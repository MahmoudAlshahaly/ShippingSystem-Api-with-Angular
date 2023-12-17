using Microsoft.EntityFrameworkCore;
using Shipping.BLL.Dtos;
using Shipping.BLL.Helpers;
using Shipping.DAL.Data.Models;
using Shipping.DAL.Params;
using Shipping.DAL.Repositories;
using Shipping.DAL.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.BLL.Managers
{
    public class BranchManager: IBranchManager
    {
        private readonly IRepository<Branch> _branchRepository;

        public BranchManager(IRepository<Branch> branchRepository)
        {
            _branchRepository = branchRepository;
        }



        public async Task<getBranchByIdDto> GetBranchByIdAsync(int id)
        {

            Branch branch = await _branchRepository.GetByCriteriaAsync(b=>b.Id == id && b.isDeleted==false);
            
            if (branch == null)
            {
                return null;
            }

            return new getBranchByIdDto
            {
               
                Name = branch.Name,
                DateTime = branch.DateTime,
                isDeleted = branch.isDeleted,
                status = branch.status,
            };
        }



        public async Task<Pagination<ShowBranchDto>> GetAllBranchesAsync(GSpecParams branchSpecParams)
        {


            var spec = new BranchSpecificaction(branchSpecParams);
            var countSpec = new BranchCountSpecification(branchSpecParams);
            var totalItems = await _branchRepository.CountAsync(countSpec);
            IReadOnlyList<Branch> branches = await _branchRepository.GetAllDataAsync(spec);
            var data = branches?.Select(b => new ShowBranchDto
            {
                Id = b.Id,
                Name = b.Name,
                DateTime = b.DateTime,
                isDeleted = b.isDeleted,
                status = b.status,
            }).ToList();

            return new Pagination<ShowBranchDto>(branchSpecParams.PageIndex, branchSpecParams.PageSize, totalItems, data);
            
        }


        public async Task<IEnumerable<UpdateBranchDto>> GetAllAsync()
        {
            var Branches = _branchRepository.GetAllAsync().Result.Where(b=>b.isDeleted==false && b.status==true);
            return Branches.Select(b => new UpdateBranchDto
            {
                Id = b.Id,
                Name = b.Name
            }).ToList();

           
        }

        public async Task<int> CreateBranchAsync(AddBranchDto branchDto)
        {
            return await _branchRepository.AddAsync(new Branch { Name = branchDto.Name });
        }


        public async Task<int> UpdateBranchAsync(UpdateBranchDto branchDto)
        {
            Branch existingBranch = await _branchRepository.GetByCriteriaAsync(b => b.Id == branchDto.Id && b.isDeleted == false);
            if (existingBranch == null)
            {
                return 0;
            }

            existingBranch.Name = branchDto.Name;

            return await _branchRepository.UpdateAsync(existingBranch);
        }

        public async Task<int> DeleteBranchAsync(int id)
        {

            Branch branch = await _branchRepository.GetByCriteriaAsync(b => b.Id == id && b.isDeleted == false, new[] { "ApplicationUsers" });
            var count = branch?.ApplicationUsers.Count();
            if (branch == null)
                return 0;

            if (count > 0)
                return -1;


            branch.isDeleted = true;
            return await _branchRepository.UpdateAsync(branch);
        }


        
        public async Task<int> ChangeStatusBranchAsync(int id)
        {

            Branch branch = await _branchRepository.GetByCriteriaAsync(b => b.Id == id && b.isDeleted == false);
            if (branch == null)
                return 0;
            if (branch.status)
            {
                branch.status = false;
            }
            else
            {
                branch.status = true;
            }
            return await _branchRepository.UpdateAsync(branch);
        }



    }
}





