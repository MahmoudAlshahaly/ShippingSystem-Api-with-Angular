using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Shipping.BLL.Dtos;
using Shipping.BLL.Dtos.RepresentativeDtos;
using Shipping.DAL;
using Shipping.DAL.Data;
using Shipping.DAL.Data.Models;
using Shipping.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Azure.Core.HttpHeader;


namespace Shipping.BLL.Managers
{
    public class OrderManager : IOrderManager
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IRepository<ShippingType> _shippingRepository;
        private readonly IRepository<DeliverToVillage> _deliverToVillageRepository;
        private readonly IRepository<Weight> _weightRepository;
        private readonly IRepository<City> _cityRepository;
        private readonly IRepository<SpecialPrice> _specialPriceRepository;
        private readonly IRepository<Representative> _representativeRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRepository<Governorate> _governorateRepository;
        private readonly IRepository<RepresentativeGovernate> _representativeGovernateRepository;

        public OrderManager(IOrderRepository orderRepository,
            IProductRepository productRepository,
            IRepository<ShippingType> shippingRepository,
            IRepository<DeliverToVillage> deliverToVillageRepository,
            IRepository<Weight> weightRepository,
            IRepository<City> cityRepository,
            IRepository<SpecialPrice> specialPriceRepository,
            IRepository<Representative> representativeRepo,
            UserManager<ApplicationUser> userManager,
            IRepository<Governorate> governorateRepository ,
            IRepository<RepresentativeGovernate> representativeGovernateRepository)
        {
            this._orderRepository = orderRepository;
            this._productRepository = productRepository;
            this._shippingRepository = shippingRepository;
            this._deliverToVillageRepository = deliverToVillageRepository;
            this._weightRepository = weightRepository;
            this._cityRepository = cityRepository;
            this._specialPriceRepository = specialPriceRepository;
            this._representativeRepo = representativeRepo;
            this._userManager = userManager;
            this._governorateRepository = governorateRepository;
            this._representativeGovernateRepository = representativeGovernateRepository;
        }

        public async Task<AddOrderResultDto> Add(AddOrderDto orderDto)
        {

            double costDeliverToVillage = await Cost_DeliverToVillageAsync(orderDto.DeliverToVillage);

            double countWeight = CountWeight(orderDto.Products);

            double costAllProducts = Cost_AllProducts(orderDto.Products);

            double costAddititonalWeight = await Cost_AdditionalWeight(countWeight);

            double costShippingType = await Cost_ShippingType(orderDto.ShippingTypeId);

            double cityShippingPrice = (double)await GetSpecialPricesWithMerchantandCityId(orderDto.MerchantId, orderDto.CityId);
            if (cityShippingPrice == 0)
            {
                cityShippingPrice = await CityShippingPrice(orderDto.CityId);
            }

            Order order = new Order()
            {
                MerchantId = orderDto.MerchantId,
                orderType = orderDto.orderType,
                ClientName = orderDto.ClientName,
                FirstPhoneNumber = orderDto.FirstPhoneNumber,
                SecondPhoneNumber = orderDto.SecondPhoneNumber,
                Email = orderDto.Email,
                GovernorateId = orderDto.GovernorateId,
                CityId = orderDto.CityId,
                Street = orderDto.Street,
                DeliverToVillage = orderDto.DeliverToVillage,
                ShippingTypeId = orderDto.ShippingTypeId,
                PaymentType = orderDto.PaymentType,
                BranchId = orderDto.BranchId,
                orderStatus = OrderStatus.New,
                Date = DateTime.Now,
                Notes = orderDto.Notes,
                isDeleted = false,
                ProductTotalCost = costAllProducts,
                OrderShippingTotalCost = costDeliverToVillage + costAddititonalWeight + cityShippingPrice + costShippingType,
                Weight = countWeight,
                Products = orderDto.Products.Select(prod => new Product
                {
                    Name = prod.Name,
                    Quantity = prod.Quantity,
                    Price = prod.Price,
                    Weight = prod.Weight,
                    isDeleted = false,
                }).ToList(),
            };

            bool isSuccesfullOrder = _orderRepository.Add(order);
            bool isSuccesfullProduct = _productRepository.AddRange(order.Products.ToList());
            if (isSuccesfullOrder && isSuccesfullProduct)
            {
                bool isSaved = _orderRepository.SaveChanges();
                if (isSaved)
                {
                    double shippingTotalCost = costDeliverToVillage + costAddititonalWeight + cityShippingPrice + costShippingType;

                    return new AddOrderResultDto(true, costAllProducts,shippingTotalCost,countWeight);
                }
            }
            return new AddOrderResultDto(false,null,null,null);

        }
        public async Task<UpdateOrderResultDto> Update(UpdateOrderDto newOrder)
        {
            double costDeliverToVillage = await Cost_DeliverToVillageAsync(newOrder.DeliverToVillage);

            double countWeight = CountWeight(newOrder.Products);

            double costAllProducts = Cost_AllProducts(newOrder.Products);

            double costAddititonalWeight = await Cost_AdditionalWeight(countWeight);

            double costShippingType = await Cost_ShippingType(newOrder.ShippingTypeId);

            double cityShippingPrice = (double)await GetSpecialPricesWithMerchantandCityId(newOrder.MerchantId, newOrder.CityId);
            if (cityShippingPrice == 0)
            {
                cityShippingPrice = await CityShippingPrice(newOrder.CityId);
            }

            var oldOrder = _orderRepository.GetById(newOrder.Id);

            if (oldOrder != null)
            {
                oldOrder.orderType = newOrder.orderType;
                oldOrder.ClientName = newOrder.ClientName;
                oldOrder.FirstPhoneNumber = newOrder.FirstPhoneNumber;
                oldOrder.SecondPhoneNumber = newOrder.SecondPhoneNumber;
                oldOrder.Email = newOrder.Email;
                oldOrder.GovernorateId = newOrder.GovernorateId;
                oldOrder.CityId = newOrder.CityId;
                oldOrder.Street = newOrder.Street;
                oldOrder.DeliverToVillage = newOrder.DeliverToVillage;
                oldOrder.ShippingTypeId = newOrder.ShippingTypeId;
                oldOrder.PaymentType = newOrder.PaymentType;
                oldOrder.BranchId = newOrder.BranchId;
                oldOrder.orderStatus = newOrder.orderStatus;
                oldOrder.Date = DateTime.Now;
                oldOrder.Notes = newOrder.Notes;
                oldOrder.isDeleted = false;
                oldOrder.ProductTotalCost = costAllProducts;
                oldOrder.OrderShippingTotalCost = costDeliverToVillage + costAddititonalWeight + cityShippingPrice + costShippingType;
                oldOrder.Weight = countWeight;
                var newProducts = newOrder.Products.Select(prod => new Product
                {
                    OrderId = oldOrder.Id,
                    Name = prod.Name,
                    Quantity = prod.Quantity,
                    Price = prod.Price,
                    Weight = prod.Weight,
                    isDeleted = false,
                }).ToList();

                var products = _productRepository.GetProductsByOrderId(oldOrder.Id).ToList();
                var deleteFlag = _productRepository.DeleteRange(products);
                var addFlag = _productRepository.AddRange(newProducts);

                if (deleteFlag && addFlag)
                {
                    bool isSaved = _orderRepository.SaveChanges();
                    if (isSaved)
                    {
                        double shippingTotalCost = costDeliverToVillage + costAddititonalWeight + cityShippingPrice + costShippingType;

                        return new UpdateOrderResultDto(true, costAllProducts, shippingTotalCost, countWeight);
                    }
                }
            }
            return new UpdateOrderResultDto(false, null, null, null);
        }
        public bool Delete(int orderId)
        {
            var order = _orderRepository.GetById(orderId);

            if (order != null)
            {
                List<Product> products = _productRepository.GetProductsByOrderId(orderId);
                bool deleted = _productRepository.DeleteRange(products);
                if (deleted)
                {
                    bool isSuccesfull = _orderRepository.Delete(order);
                    if (isSuccesfull)
                    {
                        _orderRepository.SaveChanges();
                        return true;
                    }
                }

            }
            return false;
        }
        public UpdateOrderDto GetById(int orderId)
        {
            var order = _orderRepository.GetById(orderId);
            if (order != null)
            {
                UpdateOrderDto result = new UpdateOrderDto()
                {
                    Id = order.Id,
                    MerchantId = order.MerchantId,
                    PaymentType = order.PaymentType,
                    Email = order.Email,
                    BranchId = order.BranchId,
                    CityId = order.CityId,
                    DeliverToVillage = order.DeliverToVillage ?? false,
                    FirstPhoneNumber = order.FirstPhoneNumber,
                    SecondPhoneNumber = order.SecondPhoneNumber,
                    GovernorateId = order.GovernorateId,
                    Notes = order.Notes,
                    ShippingTypeId = order.ShippingTypeId,
                    orderType = order.orderType,
                    Street = order.Street,
                    ClientName = order.ClientName,
                    Products = order.Products.Select(prod => new ProductDto
                    {
                        Name = prod.Name,
                        Quantity = prod.Quantity,
                        Price = prod.Price,
                        Weight = prod.Weight,
                    }).ToList()
            };
                return result;
            }

            return null;
        }
        public ReadAllOrderDataDto GetAllDataById(int orderId)
        {
            var order = _orderRepository.GetById(orderId);
            
            if (order != null)
            {
                ReadAllOrderDataDto result = new ReadAllOrderDataDto()
                {
                    PaymentType = order.PaymentType,
                    Email = order.Email,
                    Branch = order.Branch!.Name,
                    City = order.City!.Name,
                    DeliverToVillage = order.DeliverToVillage ?? false,
                    FirstPhoneNumber = order.FirstPhoneNumber,
                    SecondPhoneNumber = order.SecondPhoneNumber!,
                    Governorate = order.Governorate!.Name,
                    Notes = order.Notes,
                    ShippingType = order.ShippingType!.Name,
                    orderType = order.orderType,
                    Street = order.Street,
                    ClientName = order.ClientName,
                    Products = order.Products.Select(prod => new ProductDto
                    {
                        Name = prod.Name,
                        Quantity = prod.Quantity,
                        Price = prod.Price,
                        Weight = prod.Weight,
                    }).ToList()
                };
                return result;
            }

            return null;
        }

        //order reports
        public IEnumerable<ReadOrderReportsDto> GetAll(int pageNumer, int pageSize)
        {
            return _orderRepository.GetAll(pageNumer,pageSize).Select(s => new ReadOrderReportsDto
            {
                Merchant = s.Merchant!.Name,
                orderStatus = s.orderStatus,
                FirstPhoneNumber = s.FirstPhoneNumber,
                OrderShippingTotalCost = s.OrderShippingTotalCost,
                ProductTotalCost = s.ProductTotalCost,
                Date = s.Date,
                ClientName = s.ClientName,
                Governorate = s.Governorate!.Name,
                City = s.City!.Name,
            });
        }
        public int CountAll()
        {
            return _orderRepository.CountAll();
        }
        public IEnumerable<ReadOrderReportsDto> SearchByDateAndStatus(int pageNumer, int pageSize, DateTime fromDate, DateTime toDate, OrderStatus status)
        {
            return _orderRepository.SearchByDateAndStatus(pageNumer, pageSize,fromDate,toDate,status).Select(s => new ReadOrderReportsDto
            {
                Merchant = s.Merchant!.Name,
                orderStatus = s.orderStatus,
                FirstPhoneNumber = s.FirstPhoneNumber,
                OrderShippingTotalCost = s.OrderShippingTotalCost,
                ProductTotalCost = s.ProductTotalCost,
                Date = s.Date,
                ClientName = s.ClientName,
                Governorate = s.Governorate!.Name,
                City = s.City!.Name,
            });
        }
        public int CountOrdersByDateAndStatus(DateTime fromDate, DateTime toDate, OrderStatus status)
        {
            return _orderRepository.CountOrdersByDateAndStatus(fromDate,toDate,status);
        }


        //Employee
        public List<int> CountOrdersForEmployeeByStatus()
        {
            var listOrderStatus = _orderRepository.CountOrdersForEmployeeByStatus();
            int[] countOrdres = new int[11];//size of enum

            var statusGroups = listOrderStatus.GroupBy(i => i);

            foreach (var group in statusGroups)
            {
                countOrdres[group.Key] = group.Count();
            }
            return countOrdres.ToList();
        }
        public int GetCountOrdersForEmployee(int statusId, string searchText)
        {
            if (statusId > 10 || statusId < 0)
            {
                return 0;
            }
            return _orderRepository.GetCountOrdersForEmployee(statusId,searchText);
        }
        public IEnumerable<ReadOrderDto> GetOrdersForEmployee(string searchText,int statusId, int pageNumer, int pageSize)
        {
            if (statusId > 10 || statusId < 0)
            {
                return null!;
            }
            return _orderRepository.GetOrdersForEmployee(searchText,statusId, pageNumer, pageSize).Select(o => new ReadOrderDto
            {
                Id=o.Id,
                ClientName = o.ClientName,
                Date = o.Date,
                Governorate = o.Governorate!.Name,
                City = o.City!.Name,
                Cost = o.ProductTotalCost + o.OrderShippingTotalCost

            });
        }
        public bool SelectRepresentative(int OrderId, string representativeId)
        {
            var order = _orderRepository.GetById(OrderId);
            if (order == null)
            {
                return false;
            }
            else
            {
                order.RepresentativeId = representativeId;
                order.orderStatus = OrderStatus.RepresentitiveDelivered;
                _orderRepository.SaveChanges();
                return true;
            }
        }
        public async Task<List<DropdownListRepresentativeDto>> DropdownListRepresentativeAsync(int orderId)
        {
            var governorateId = _orderRepository.GetById(orderId)?.GovernorateId;

            var representativeGovernate = await _representativeGovernateRepository.GetAllAsync();
            var governorates = await _governorateRepository.GetAllAsync();
            List<DropdownListRepresentativeDto> representatives = (from a in _userManager.Users
                                                          join b in representativeGovernate  on a.Id equals b.RepresentativeId
                                                          join c in governorates on b.GovernorateId equals governorateId
                                                          select new DropdownListRepresentativeDto { Name = a.Name, Id = a.Id }).Distinct().ToList();


            return representatives;


        }
        
        //Merchant
        public List<int> CountOrdersForMerchantByStatus(string merchantId)
        {
            var listOrderStatus = _orderRepository.CountOrdersForMerchantByStatus(merchantId);
            int[] countOrdres = new int[11];//size of enum

            var statusGroups = listOrderStatus.GroupBy(i => i);

            foreach (var group in statusGroups)
            {
                countOrdres[group.Key] = group.Count();
            }
            return countOrdres.ToList();
        }
        public int GetCountOrdersForMerchant(string merchantId, int statusId, string searchText)
        {
            if (statusId > 10 || statusId < 0)
            {
                return 0;
            }
            return _orderRepository.GetCountOrdersForMerchant(merchantId, statusId,searchText);
        }
        public IEnumerable<ReadOrderDto> GetOrdersForMerchant(string searchText, string merchantId, int statusId, int pageNumer, int pageSize)
        {
            if (statusId > 10 || statusId < 0)
            {
                return null!;
            }
            return _orderRepository.GetOrdersForMerchant(searchText,merchantId, statusId, pageNumer, pageSize).Select(o => new ReadOrderDto
            {
                Id= o.Id,
                ClientName = o.ClientName,
                Date = o.Date,
                Governorate = o.Governorate!.Name,
                City = o.City!.Name,
                Cost = o.ProductTotalCost + o.OrderShippingTotalCost

            });
        }

        //Employee and Merchant
        public bool ChangeStatus(int OrderId, OrderStatus status)
        {
            var order = _orderRepository.GetById(OrderId);
            if (order == null)
            {
                return false;
            }
            else
            {
                order.orderStatus = status;
                _orderRepository.SaveChanges();
                return true;
            }
        }

        
        //Representative
        public List<int> CountOrdersForRepresentativeByStatus(string representativeId)
        {
            var listOrderStatus = _orderRepository.CountOrdersForRepresentativeByStatus(representativeId);
            //size of enum
            int[] countOrdres = new int[11];

            var statusGroups = listOrderStatus.GroupBy(i => i);

            foreach (var group in statusGroups)
            {
                countOrdres[group.Key] = group.Count();
            }

            int[] representativeStatus = new int[8];

            Array.Copy(countOrdres, 2, representativeStatus, 0, 8);

            return representativeStatus.ToList();
        }
        public int GetCountOrdersForRepresentative(string representativeId, int statusId, string searchText)
        {
            return _orderRepository.GetCountOrdersForRepresentative(representativeId, statusId, searchText);
        }
        public IEnumerable<ReadOrderDto> GetOrdersForRepresentative(string representativeId, int statusId, int pageNumer, int pageSize, string searchText)
        {
            return _orderRepository.GetOrdersForRepresentative(representativeId,statusId, pageNumer, pageSize, searchText).Select(o => new ReadOrderDto
            {
                Id = o.Id,
                ClientName = o.ClientName,
                Date = o.Date,
                Governorate = o.Governorate!.Name,
                City = o.City!.Name,
                Cost = o.ProductTotalCost + o.OrderShippingTotalCost

            });
        }
        public bool ChangeStatusAndReasonRefusal(int OrderId, OrderStatus status, int? reasonRefusal)
        {
            var order = _orderRepository.GetById(OrderId);
            if (order == null)
            {
                return false;
            }
            else
            {
                order.orderStatus = status;
                if (reasonRefusal != -1) { order.ReasonsRefusalTypeId = reasonRefusal; }
                else { order.ReasonsRefusalTypeId = null; }
                _orderRepository.SaveChanges();
                return true;
            }
        }




        private async Task<double> CityShippingPrice(int cityId)
        {
            var result = await _cityRepository.GetByIdAsync(cityId);
            if (result != null)
            {
                return result.Price;
            }
            return 0;
        }

        private async Task<double> Cost_DeliverToVillageAsync(bool isDeliverToVillage)
        {
            var result = await _deliverToVillageRepository.GetAllAsync();

            if (isDeliverToVillage && result != null)
            {
                return result.Select(s => s.AdditionalCost).FirstOrDefault();
            }
            return 0;
        }

        private async Task<double> Cost_ShippingType(int shippingTypeId)
        {
            var result = await _shippingRepository.GetByIdAsync(shippingTypeId);
            if (result != null)
            {
                return result.Cost;
            }
            return 0;
        }

        private async Task<double> Cost_AdditionalWeight(double totalWeight)
        {
            double cost = 0;
            double defaultWeight = 0;
            double additionalPrice = 0;
            var result = await _weightRepository.GetAllAsync();
            if (result != null)
            {
                defaultWeight = result.Select(w => w.DefaultWeight).FirstOrDefault();

                if (totalWeight > defaultWeight)
                {

                    additionalPrice = result.Select(w => w.AdditionalPrice).FirstOrDefault();

                    totalWeight = totalWeight - defaultWeight;

                    cost = totalWeight * additionalPrice;
                }
            }

            return cost;
        }

        private double CountWeight(List<ProductDto> products)
        {
            double weight = 0;
            foreach (var item in products)
            {
                weight += item.Weight * item.Quantity;
            }
            return weight;
        }

        private double Cost_AllProducts(List<ProductDto> products)
        {
            double price = 0;
            foreach (var item in products)
            {
                price += item.Price * item.Quantity;
            }
            return price;
        }

        private async Task<decimal> GetSpecialPricesWithMerchantandCityId(string merchantId, int cityId)
        {
            decimal totalPrice = 0;
            var result = await _specialPriceRepository.GetAllAsync();
            if (result != null)
            {
                var specialPrices = result.Where(sp => sp.CityId == cityId & sp.MerchentId == merchantId).FirstOrDefault();
                if (specialPrices != null)
                {
                    totalPrice = specialPrices.Price;
                }
            }
            return totalPrice;

        }
    }
}
