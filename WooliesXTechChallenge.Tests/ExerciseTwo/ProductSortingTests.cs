using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WooliesXTechChallenge.Application.Sort;
using WooliesXTechChallenge.ExternalServices.Interfaces;
using WooliesXTechChallenge.Models.Sort;
using Xunit;

namespace WooliesXTechChallenge.Tests.ExerciseTwo
{
    public class ProductSortingTests
    {

        #region Setup

        private readonly string _token = "2306c329-4b09-4bdf-b756-c2cfc207e744";
        private readonly GetProductsHandler _getProductsHandler;

        public ProductSortingTests()
        {
            var productsServiceMock = new Mock<IProductService>();
            productsServiceMock.Setup(u => u.GetProductsAsync(_token))
                .ReturnsAsync(new List<ProductModel> { 
                    new ProductModel{Name = "Test Product A", Price = 99.99M},
                    new ProductModel{Name = "Test Product B", Price = 101.99M},
                    new ProductModel{Name = "Test Product C", Price = 10.99M},
                    new ProductModel{Name = "Test Product D", Price = 5},
                    new ProductModel{Name = "Test Product F", Price = 999999999999}
                });

            var shopperHistoryServiceMock = new Mock<IShopperHistoryService>();
            shopperHistoryServiceMock.Setup(u => u.GetShopperHistoryAsync(_token))
                .ReturnsAsync(new List<ShopperHistoryModel> {
                    new ShopperHistoryModel{CustomerId=123,Products = new List<ProductModel> { 
                    new ProductModel{Name = "Test Product A", Price = 99.99M},
                    new ProductModel{Name = "Test Product B", Price = 101.99M},
                    new ProductModel{Name = "Test Product F", Price = 999999999999}
                }},
                    new ShopperHistoryModel{CustomerId=23,Products = new List<ProductModel> {
                    new ProductModel{Name = "Test Product A", Price = 99.99M},
                    new ProductModel{Name = "Test Product B", Price = 101.99M},
                    new ProductModel{Name = "Test Product F", Price = 999999999999}
                }},
                    new ShopperHistoryModel{CustomerId=23,Products = new List<ProductModel> {
                    new ProductModel{Name = "Test Product C", Price = 10.99M},
                    new ProductModel{Name = "Test Product F", Price = 999999999999}
                }},
                    new ShopperHistoryModel{CustomerId=123,Products = new List<ProductModel> {
                    new ProductModel{Name = "Test Product A", Price = 99.99M},
                    new ProductModel{Name = "Test Product B", Price = 101.99M},
                    new ProductModel{Name = "Test Product C", Price = 10.99M},
                }}
                });
            _getProductsHandler = new GetProductsHandler(productsServiceMock.Object, shopperHistoryServiceMock.Object);
        }

        #endregion

        #region Test Cases

        [Fact(DisplayName = "Sort by sort option - Low .")]
        public async Task SortByLowAsync()
        {
            List<ProductModel> expectedList = new List<ProductModel> {
                    new ProductModel{Name = "Test Product D", Price = 5},
                    new ProductModel{Name = "Test Product C", Price = 10.99M},
                    new ProductModel{Name = "Test Product A", Price = 99.99M},
                    new ProductModel{Name = "Test Product B", Price = 101.99M},
                    new ProductModel{Name = "Test Product F", Price = 999999999999}
                };
            
            var result = await _getProductsHandler.Handle(new GetProductsQuery("Low",_token), new System.Threading.CancellationToken());

            var expectedStr = JsonConvert.SerializeObject(expectedList);
            var resultStr = JsonConvert.SerializeObject(result);
            Assert.Equal(expectedStr, resultStr);
        }

        [Fact(DisplayName = "Sort by sort option - High.")]
        public async Task SortByHighAsync()
        {
            List<ProductModel> expectedList = new List<ProductModel> {
                    new ProductModel{Name = "Test Product F", Price = 999999999999},
                    new ProductModel{Name = "Test Product B", Price = 101.99M},
                    new ProductModel{Name = "Test Product A", Price = 99.99M},
                    new ProductModel{Name = "Test Product C", Price = 10.99M},
                    new ProductModel{Name = "Test Product D", Price = 5},
                };

            var result = await _getProductsHandler.Handle(new GetProductsQuery("High", _token), new System.Threading.CancellationToken());
            var expectedStr = JsonConvert.SerializeObject(expectedList);
            var resultStr = JsonConvert.SerializeObject(result);
            Assert.Equal(expectedStr, resultStr);
        }

        [Fact(DisplayName = "Sort by sort option - Ascending.")]
        public async Task SortByAscendingAsync()
        {
            List<ProductModel> expectedList = new List<ProductModel> {
                    new ProductModel{Name = "Test Product A", Price = 99.99M},
                    new ProductModel{Name = "Test Product B", Price = 101.99M},
                    new ProductModel{Name = "Test Product C", Price = 10.99M},
                    new ProductModel{Name = "Test Product D", Price = 5},
                    new ProductModel{Name = "Test Product F", Price = 999999999999},
                };

            var result = await _getProductsHandler.Handle(new GetProductsQuery("Ascending", _token), new System.Threading.CancellationToken());
            var expectedStr = JsonConvert.SerializeObject(expectedList);
            var resultStr = JsonConvert.SerializeObject(result);
            Assert.Equal(expectedStr, resultStr);
        }

        [Fact(DisplayName = "Sort by sort option - Descending.")]
        public async Task SortByDescendingAsync()
        {
            List<ProductModel> expectedList = new List<ProductModel> {
                    new ProductModel{Name = "Test Product F", Price = 999999999999},
                    new ProductModel{Name = "Test Product D", Price = 5},
                    new ProductModel{Name = "Test Product C", Price = 10.99M},
                    new ProductModel{Name = "Test Product B", Price = 101.99M},
                    new ProductModel{Name = "Test Product A", Price = 99.99M},
                };

            var result = await _getProductsHandler.Handle(new GetProductsQuery("Descending", _token), new System.Threading.CancellationToken());
            var expectedStr = JsonConvert.SerializeObject(expectedList);
            var resultStr = JsonConvert.SerializeObject(result);
            Assert.Equal(expectedStr, resultStr);
        }

        [Fact(DisplayName = "Sort by sort option - Recommended.")]
        public async Task SortByRecommendedAsync()
        {
            List<ProductModel> expectedList = new List<ProductModel> {
                    new ProductModel{Name = "Test Product A", Price = 99.99M, popularity = 3},
                    new ProductModel{Name = "Test Product B", Price = 101.99M, popularity = 3},
                    new ProductModel{Name = "Test Product F", Price = 999999999999, popularity= 3},
                    new ProductModel{Name = "Test Product C", Price = 10.99M,popularity=2},
                    new ProductModel{Name = "Test Product D", Price = 5,popularity=0},
                };

            var result = await _getProductsHandler.Handle(new GetProductsQuery("Recommended", _token), new System.Threading.CancellationToken());
            var expectedStr = JsonConvert.SerializeObject(expectedList);
            var resultStr = JsonConvert.SerializeObject(result);
            Assert.Equal(expectedStr, resultStr);
        }

        #endregion


    }
}
