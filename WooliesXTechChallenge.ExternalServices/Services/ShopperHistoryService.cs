using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WooliesXTechChallenge.ExternalServices.Interfaces;
using WooliesXTechChallenge.Models.Sort;

namespace WooliesXTechChallenge.ExternalServices.Services
{
    /// <summary>
    /// shopper order history service
    /// </summary>
    public class ShopperHistoryService : IShopperHistoryService
    {

        private readonly IHttpClientFactory _httpClientFactory;
        public ShopperHistoryService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// Get shopper History
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<ShopperHistoryModel>> GetShopperHistoryAsync(string token)
        {
            // Get an HttpClient configured to the specification defined in StartUp.
            var client = _httpClientFactory.CreateClient("WooliesX");
            var query = new Dictionary<string, string>
            {
                ["token"] = token
            };

            var response = await client.GetStringAsync(QueryHelpers.AddQueryString(client.BaseAddress.ToString() + "/shopperHistory", query));

            List<ShopperHistoryModel> productList = JsonConvert.DeserializeObject<List<ShopperHistoryModel>>(response);

            return productList;
        }
    }

}
