using System.Collections.Generic;
using System.Threading.Tasks;
using WooliesXTechChallenge.Models.Sort;

namespace WooliesXTechChallenge.ExternalServices.Interfaces
{
    public interface IShopperHistoryService
    {
        Task<List<ShopperHistoryModel>> GetShopperHistoryAsync(string token);
    }
}
