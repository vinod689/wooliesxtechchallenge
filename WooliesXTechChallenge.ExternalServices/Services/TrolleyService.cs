using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WooliesXTechChallenge.ExternalServices.Interfaces;
using WooliesXTechChallenge.Models.Trolley;

namespace WooliesXTechChallenge.ExternalServices.Services
{
    public class TrolleyService: ITrolleyService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TrolleyService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<decimal> GetTrollyTotal(TrolleyModel TrolleyModel, string token)
        {
            // Get an HttpClient configured to the specification defined in StartUp.
            var client = _httpClientFactory.CreateClient("WooliesX");
            var query = new Dictionary<string, string>
            {
                ["token"] = token
            };

            var json = JsonConvert.SerializeObject(TrolleyModel);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(QueryHelpers.AddQueryString(client.BaseAddress.ToString() + "/trolleyCalculator", query), data);

            decimal trolleyTotal = Convert.ToDecimal(response.Content.ReadAsStringAsync().Result);

            return trolleyTotal;
        }
    }
}
