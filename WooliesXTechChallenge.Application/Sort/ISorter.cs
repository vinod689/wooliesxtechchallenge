using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WooliesXTechChallenge.ExternalServices.Interfaces;
using WooliesXTechChallenge.Models.Sort;

namespace WooliesXTechChallenge.Application.Sort
{
    /* Interface for Sorter */
    public interface ISorter
    {
        Task<List<SortProductModel>> SortProducts(List<SortProductModel> products);
    }

    /* Concrete implementation of base Sorter */
    public class LowToHighSorter : ISorter
    {
        public async Task<List<SortProductModel>> SortProducts(List<SortProductModel> products)
        {
            products.Sort((x, y) =>
            {
                if (x.Price < y.Price) return -1;
                if (x.Price > y.Price) return 1;
                return 0;
            });
            return await Task.FromResult(products);
        }
    }

    /* Concrete implementation of base Sorter */
    public class HighToLowSorter : ISorter
    {
        public async Task<List<SortProductModel>> SortProducts(List<SortProductModel> products)
        {
            products.Sort((x, y) =>
            {
                if (x.Price > y.Price) return -1;
                if (x.Price < y.Price) return 1;
                return 0;
            });
            return await Task.FromResult(products);
        }
    }

    /* Concrete implementation of base Sorter */
    public class AscendingSorter : ISorter
    {
        public async Task<List<SortProductModel>> SortProducts(List<SortProductModel> products)
        {
            products.Sort((a, b) => a.Name.CompareTo(b.Name));
            return await Task.FromResult(products);
        }
    }

    /* Concrete implementation of base Sorter */
    public class DescendingSorter : ISorter
    {
        public async Task<List<SortProductModel>> SortProducts(List<SortProductModel> products)
        {
            products.Sort((a, b) => b.Name.CompareTo(a.Name));
            return await Task.FromResult(products);
        }
    }

    /* Concrete implementation of base Sorter */
    public class RecommendedSorter : ISorter
    {
        private readonly IShopperHistoryService _shopperHistoryService;

        public RecommendedSorter(IShopperHistoryService shopperHistoryService)
        {
            _shopperHistoryService = shopperHistoryService;
        }
        public async Task<List<SortProductModel>> SortProducts(List<SortProductModel> products)
        {
            var shopperHistoryList = new List<ShopperHistoryModel>();
            try
            {
                shopperHistoryList = await _shopperHistoryService.GetShopperHistoryAsync(Environment.GetEnvironmentVariable("AUTHENTICATION_TOKEN"));

            }
            catch(Exception Ex)
            {
                Ex.Data.Add("shopper history", shopperHistoryList);
                throw;
            }


            //Get all the products across all users
            var productsByUsers = new List<SortProductModel>();
            shopperHistoryList.ForEach(shopperHistory =>
                {
                    if (shopperHistory.Products.Count > 0)
                    {
                        productsByUsers.AddRange(shopperHistory.Products);
                    }
                });

                //Count the popularity by iterating products
                products.ForEach(prod =>
                {
                    productsByUsers.ForEach(userProd =>
                    {
                        if (userProd.Name == prod.Name) { prod.popularity++; }
                    });
                });
            products.Sort((x, y) =>
            {
                if (x.popularity > y.popularity) return -1;
                if (x.popularity < y.popularity) return 1;
                return 0;
            });

            return await Task.FromResult(products); ;
        }
    }
}
