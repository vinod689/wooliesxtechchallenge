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
        private List<decimal> priceCombinations = new List<decimal>();

        public async Task<decimal> GetTrollyTotal(TrolleyModel TrolleyModel, string token)
        {
            decimal trolleyTotal = CalculateTrolleyTotal(TrolleyModel);

            return await Task.FromResult(trolleyTotal);
        }

        private decimal CalculateTrolleyTotal(TrolleyModel trolleyModel)
        {
            //STEP 1 
            //Get the list of products and quantities into respective dictionaries
            var productDictionary = trolleyModel.Products.ToDictionary(x => x.Name, x => x.Price);
            var quantityDictionary = trolleyModel.Quantities.ToDictionary(y => y.Name, y => y.Quantity);

            //if there are no products/quantities exists just return 0
            if (productDictionary.Count == 0 || quantityDictionary.Count == 0
                                            || productDictionary.Sum(x => x.Value) <= 0
                                            || quantityDictionary.Sum(x => x.Value) <= 0)
            {
                return 0;
            }

            //STEP 2
            //if product & quantity names doesn't match throw exception
            var productQualityDiff = productDictionary.Keys.Except(quantityDictionary.Keys).Concat(quantityDictionary.Keys.Except(productDictionary.Keys));
            if (productQualityDiff.Any())
            {
                var ex = new Exception(message: "incomplete content");
                ex.Data.Add("product quality differences: ", productQualityDiff);
                throw ex;
            }


            //STEP 3
            //get the maximum price and keep it aside
            var maxPrice = productDictionary.ToDictionary(orig => orig.Key, orig => orig.Value * quantityDictionary[orig.Key]).Sum(x => x.Value);


            //check if specials exists, if not just return regular price
            if (trolleyModel.Specials.Count == 0)
            {
                return maxPrice;
            }

            //STEP 4
            //Now get the list of quantities as dictionaries along with their totals and unit price
            var listOfSpecials = new List<(Dictionary<string, int> Quantity, decimal Total)>();
            trolleyModel.Specials.ForEach(special =>
            {
                //Check if the special quantity exceeds required quantity, if so just ignore this special
                if (special.Quantities.ToDictionary(p => p.Name, p => quantityDictionary[p.Name] - p.Quantity)
                                                                .ToDictionary(orig => orig.Key, orig => orig.Value).Any(x => x.Value < 0) || special.Quantities.Sum(x => x.Quantity) <= 0)
                {
                    return;
                }

                listOfSpecials.Add((special.Quantities.ToDictionary(x => x.Name, x => x.Quantity), special.Total));
            });

            //there are no special that can offer better unit price than maxPrice, so just return maxPrice
            if (listOfSpecials.Count == 0)
            {
                return maxPrice;
            }
            //Add the max price to the price combinations
            priceCombinations.Add(maxPrice);

            //STEP 5
            //Validate specials against the products
            listOfSpecials.ForEach(special =>
            {
                if (productDictionary.Keys.Except(special.Quantity.Keys).Concat(special.Quantity.Keys.Except(productDictionary.Keys)).Any())
                {
                    priceCombinations.Add(0);
                    return;
                }
            });


            //STEP 6
            //Get the lowest special quantity based on the following criteria
            //a. that offers best unit price
            //b. that has least quantity so that 
            //var lowestSpecialQuantity = listOfSpecials.OrderBy(x => x.UnitPrice).ThenBy(y => y.Quantity.Values.Sum()).FirstOrDefault();

            listOfSpecials.ForEach(special =>
            {
                //Get all the price combinations using lowest special quantity
                AddPriceCombinations(special, listOfSpecials, quantityDictionary, productDictionary);
            });

            //FINAL STEP
            //Finally return the least possible price from the list
            return priceCombinations.Min();
        }

        /// <summary>
        /// method that iterates specials add generate least possible price combinations
        /// </summary>
        /// <param name="lowestSpecial"></param>
        /// <param name="specials"></param>
        /// <param name="quantityDictionary"></param>
        /// <param name="productDictionary"></param>
        private void AddPriceCombinations((Dictionary<string, int> Quantity, decimal Total) lowestSpecial,
                                        List<(Dictionary<string, int> Quantity, decimal Total)> specials,
                                        Dictionary<string, int> quantityDictionary,
                                        Dictionary<string, decimal> productDictionary)
        {
            var newSpecialList = new List<(Dictionary<string, int> Quantity, decimal Total)>();
            specials.ForEach(special =>
            {
                var addedSpecial = (Quantity: lowestSpecial.Quantity.ToDictionary(orig => orig.Key, orig => orig.Value + special.Quantity[orig.Key]), Total: lowestSpecial.Total + special.Total);

                if (addedSpecial.Quantity.Where(entry => entry.Value > quantityDictionary[entry.Key]).ToDictionary(entry => entry.Key, entry => entry.Value).Any())
                { //if adding this would exceed the required quantity then get the remaining with regular price

                    var remainingRegularPrice = special.Quantity.ToDictionary(p => p.Key, p => quantityDictionary[p.Key] - p.Value)
                                                                .ToDictionary(orig => orig.Key, orig => orig.Value * productDictionary[orig.Key]).Sum(x => x.Value);
                    priceCombinations.Add(special.Total + remainingRegularPrice);
                }
                else if (addedSpecial.Quantity.Where(entry => entry.Value < quantityDictionary[entry.Key]).ToDictionary(entry => entry.Key, entry => entry.Value).Any())
                {
                    newSpecialList.Add(addedSpecial);
                }
                else
                {//matching with required quantity for all the items so just add it to price combinations
                    priceCombinations.Add(addedSpecial.Total);
                }
            });

            //If  there are no more special quantities to iterate, break the recursion
            if (newSpecialList.Count == 0)
            {
                return;
            }

            //recursive function to iterate the special quantities
            AddPriceCombinations(lowestSpecial, newSpecialList, quantityDictionary, productDictionary);
        }
    }
}
