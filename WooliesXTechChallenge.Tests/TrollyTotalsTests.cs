using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using WooliesXTechChallenge.Application.Trolley;
using WooliesXTechChallenge.ExternalServices.Interfaces;
using WooliesXTechChallenge.ExternalServices.Services;
using WooliesXTechChallenge.Models.Trolley;
using Xunit;

namespace WooliesXTechChallenge.Tests
{
    public class TrollyTotalsTests
    {
        #region Setup

        private readonly string _token = "2306c329-4b09-4bdf-b756-c2cfc207e744";

        public TrollyTotalsTests()
        {
        }

        #endregion

        #region Test Cases

        [Fact(DisplayName = "get the trolley total for sample 1.")]
        public async Task getTrolleyTotalSample1()
        {
            decimal expected = 30;
            var trolleyService = new TrolleyService();
            TrolleyModel trolleyModel = JsonConvert.DeserializeObject<TrolleyModel>(File.ReadAllText(@"Mock\\Trolley\\TrolleyRequest-Sample1.json"));
            var result = await trolleyService.GetTrollyTotal(trolleyModel,_token);
            Assert.Equal(expected, result);
        }

        [Fact(DisplayName = "get the trolley total for sample 2.")]
        public async Task getTrolleyTotalSample2()
        {
            decimal expected = 80;
            TrolleyModel trolleyModel = JsonConvert.DeserializeObject<TrolleyModel>(File.ReadAllText(@"Mock\\Trolley\\TrolleyRequest-Sample2.json"));
            var trolleyService = new TrolleyService();
            var result = await trolleyService.GetTrollyTotal(trolleyModel, _token);
            Assert.Equal(expected, result);
        }

        [Fact(DisplayName = "get the trolley total for sample 3.")]
        public async Task getTrolleyTotalSample3()
        {
            decimal expected = 70;
            TrolleyModel trolleyModel = JsonConvert.DeserializeObject<TrolleyModel>(File.ReadAllText(@"Mock\\Trolley\\TrolleyRequest-Sample3.json"));
            var trolleyService = new TrolleyService();
            var result = await trolleyService.GetTrollyTotal(trolleyModel, _token);
            Assert.Equal(expected, result);
        }

        [Fact(DisplayName = "get the trolley total for sample 3.")]
        public async Task getTrolleyTotalSample4()
        {
            decimal expected = 6.296458940268143M;
            TrolleyModel trolleyModel = JsonConvert.DeserializeObject<TrolleyModel>(File.ReadAllText(@"Mock\\Trolley\\TrolleyRequest-Sample4.json"));
            var trolleyService = new TrolleyService();
            var result = await trolleyService.GetTrollyTotal(trolleyModel, _token);
            Assert.Equal(expected, result);
        }

        #endregion
    }
}
