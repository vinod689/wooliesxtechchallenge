using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WooliesXTechChallenge.Application.Sort;
using WooliesXTechChallenge.ExternalServices.Interfaces;
using WooliesXTechChallenge.ExternalServices.Services;
using WooliesXTechChallenge.Models.Sort;
using Xunit;

namespace WooliesXTechChallenge.Tests
{
    public class ProductSortingTests
    {

        #region Setup

        private readonly string _token = "2306c329-4b09-4bdf-b756-c2cfc207e744";
        private readonly List<SortProductModel> _productModelMock;
        

        public ProductSortingTests()
        {
            _productModelMock = new List<SortProductModel> { 
                    new SortProductModel{Name = "Test Product A", Price = 99.99M},
                    new SortProductModel{Name = "Test Product B", Price = 101.99M},
                    new SortProductModel{Name = "Test Product C", Price = 10.99M},
                    new SortProductModel{Name = "Test Product D", Price = 5},
                    new SortProductModel{Name = "Test Product F", Price = 999999999999}
                };

        }

        #endregion

        #region Test Cases

        [Fact(DisplayName = "Sort by sort option - Low .")]
        public async Task SortByLowAsync()
        {
            List<SortProductModel> expectedList = new List<SortProductModel> {
                    new SortProductModel{Name = "Test Product D", Price = 5},
                    new SortProductModel{Name = "Test Product C", Price = 10.99M},
                    new SortProductModel{Name = "Test Product A", Price = 99.99M},
                    new SortProductModel{Name = "Test Product B", Price = 101.99M},
                    new SortProductModel{Name = "Test Product F", Price = 999999999999}
                };

            var lowToHighSorter = new LowToHighSorter();
            await lowToHighSorter.SortProducts(_productModelMock);

            var expectedStr = JsonConvert.SerializeObject(expectedList);
            var resultStr = JsonConvert.SerializeObject(_productModelMock);
            Assert.Equal(expectedStr, resultStr);
        }

        [Fact(DisplayName = "Sort by sort option - High.")]
        public async Task SortByHighAsync()
        {
            List<SortProductModel> expectedList = new List<SortProductModel> {
                    new SortProductModel{Name = "Test Product F", Price = 999999999999},
                    new SortProductModel{Name = "Test Product B", Price = 101.99M},
                    new SortProductModel{Name = "Test Product A", Price = 99.99M},
                    new SortProductModel{Name = "Test Product C", Price = 10.99M},
                    new SortProductModel{Name = "Test Product D", Price = 5},
                };

            var highToLowSorter = new HighToLowSorter();
            await highToLowSorter.SortProducts(_productModelMock);

            var expectedStr = JsonConvert.SerializeObject(expectedList);
            var resultStr = JsonConvert.SerializeObject(_productModelMock);
            Assert.Equal(expectedStr, resultStr);
        }

        [Fact(DisplayName = "Sort by sort option - Ascending.")]
        public async Task SortByAscendingAsync()
        {
            List<SortProductModel> expectedList = new List<SortProductModel> {
                    new SortProductModel{Name = "Test Product A", Price = 99.99M},
                    new SortProductModel{Name = "Test Product B", Price = 101.99M},
                    new SortProductModel{Name = "Test Product C", Price = 10.99M},
                    new SortProductModel{Name = "Test Product D", Price = 5},
                    new SortProductModel{Name = "Test Product F", Price = 999999999999},
                };

            var ascendingSorter = new AscendingSorter();
            await ascendingSorter.SortProducts(_productModelMock);

            var expectedStr = JsonConvert.SerializeObject(expectedList);
            var resultStr = JsonConvert.SerializeObject(_productModelMock);
            Assert.Equal(expectedStr, resultStr);
        }

        [Fact(DisplayName = "Sort by sort option - Descending.")]
        public async Task SortByDescendingAsync()
        {
            List<SortProductModel> expectedList = new List<SortProductModel> {
                    new SortProductModel{Name = "Test Product F", Price = 999999999999},
                    new SortProductModel{Name = "Test Product D", Price = 5},
                    new SortProductModel{Name = "Test Product C", Price = 10.99M},
                    new SortProductModel{Name = "Test Product B", Price = 101.99M},
                    new SortProductModel{Name = "Test Product A", Price = 99.99M},
                };


            var descendingSorter = new DescendingSorter();
            await descendingSorter.SortProducts(_productModelMock);

            var expectedStr = JsonConvert.SerializeObject(expectedList);
            var resultStr = JsonConvert.SerializeObject(_productModelMock);
            Assert.Equal(expectedStr, resultStr);
        }

        #endregion


    }
}
