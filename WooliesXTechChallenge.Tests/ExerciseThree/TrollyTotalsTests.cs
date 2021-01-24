using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using WooliesXTechChallenge.Application.Trolley;
using WooliesXTechChallenge.ExternalServices.Interfaces;
using WooliesXTechChallenge.Models.Trolley;
using Xunit;

namespace WooliesXTechChallenge.Tests.ExerciseThree
{
    public class TrollyTotalsTests
    {
        #region Setup

        private readonly string _token = "2306c329-4b09-4bdf-b756-c2cfc207e744";
        private readonly GetTrolleyTotalHandler _getTrolleyTotalHandler;


        public TrollyTotalsTests()
        {
            _getTrolleyTotalHandler = new GetTrolleyTotalHandler();
        }

        #endregion

        #region Test Cases

        [Fact(DisplayName = "get the trolley total for sample 1.")]
        public async Task getTrolleyTotalSample1()
        {
            var expected = 80;
            TrolleyModel trolleyModel = JsonConvert.DeserializeObject<TrolleyModel>(File.ReadAllText(@"Mock\\Trolley\\TrolleyResult-80.json"));
            var result = await _getTrolleyTotalHandler.Handle(new GetTrolleyTotalQuery(trolleyModel,_token), new System.Threading.CancellationToken());
            Assert.Equal(expected, result);
        }

        [Fact(DisplayName = "get the trolley total for sample 2.")]
        public async Task getTrolleyTotalSample2()
        {
            var expected = 30;
            TrolleyModel trolleyModel = JsonConvert.DeserializeObject<TrolleyModel>(File.ReadAllText(@"Mock\\Trolley\\TrolleyResult-30.json"));
            var result = await _getTrolleyTotalHandler.Handle(new GetTrolleyTotalQuery(trolleyModel, _token), new System.Threading.CancellationToken());
            Assert.Equal(expected, result);
        }

        [Fact(DisplayName = "get the trolley total for missing info.")]
        public async Task getTrolleyTotalForMissingInfo()
        {
            var expected = 30;
            TrolleyModel trolleyModel = JsonConvert.DeserializeObject<TrolleyModel>(File.ReadAllText(@"Mock\\Trolley\\missing-info.json"));
            var result = await _getTrolleyTotalHandler.Handle(new GetTrolleyTotalQuery(trolleyModel, _token), new System.Threading.CancellationToken());
            Assert.Equal(expected, result);
        }

        #endregion
    }
}
