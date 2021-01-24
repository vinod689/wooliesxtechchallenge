using System.Collections.Generic;
using System.Threading.Tasks;
using WooliesXTechChallenge.Models.Sort;

namespace WooliesXTechChallenge.ExternalServices.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductModel>> GetProductsAsync(string token);
    }
}
