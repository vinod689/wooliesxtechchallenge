using System.Collections.Generic;
using System.Threading.Tasks;
using WooliesXTechChallenge.Models.Sort;

namespace WooliesXTechChallenge.ExternalServices.Interfaces
{
    public interface IProductService
    {
        Task<List<SortProductModel>> GetProductsAsync(string token);
    }
}
