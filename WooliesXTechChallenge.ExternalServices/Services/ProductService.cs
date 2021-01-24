using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WooliesXTechChallenge.ExternalServices.Interfaces;
using WooliesXTechChallenge.Models.Sort;

namespace WooliesXTechChallenge.ExternalServices.Services
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ProductService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<ProductModel>> GetProductsAsync(string token)
        {
            // Get an HttpClient configured to the specification defined in StartUp.
            var client = _httpClientFactory.CreateClient("WooliesX");
            var query = new Dictionary<string, string>
            {
                ["token"] = token
            };

            var response = await client.GetStringAsync(QueryHelpers.AddQueryString(client.BaseAddress.ToString() + "/products", query));

            List<ProductModel> productList = JsonConvert.DeserializeObject<List<ProductModel>>(response);

            return productList;
        }
    }
}
