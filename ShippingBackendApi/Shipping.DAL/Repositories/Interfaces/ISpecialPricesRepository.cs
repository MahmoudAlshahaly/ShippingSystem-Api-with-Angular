using Shipping.DAL.Data.Models;

namespace Shipping.DAL.Repositories
{


    public interface ISpecialPricesRepository
    {
        Task<List<SpecialPrice>> GetSpecialPricesByMerchantId(string Id);
        Task<int> AddRangeAsync(List<SpecialPrice> specialPrices);
        Task<int> RemoveRangeAsync(List<SpecialPrice> specialPrices);

    }
}
