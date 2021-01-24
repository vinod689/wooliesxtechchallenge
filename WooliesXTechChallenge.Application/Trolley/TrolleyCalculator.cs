using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WooliesXTechChallenge.Models.Trolley;

namespace WooliesXTechChallenge.Application.Trolley
{
    public static class TrolleyCalculator
    {
        private static List<decimal> priceCombinations = new List<decimal>();

        public static decimal CalculateTrolleyTotal(TrolleyModel TrolleyModel)
        {
            decimal regualrPrice = 0;

            TrolleyModel.Quantities.ForEach(q =>
            {
                var product = TrolleyModel.Products.Find(x => x.Name == q.Name);

                if (product == null)
                {
                    var ex = new Exception(message: "incomplete content");
                    ex.Data.Add("product: ", product);
                    throw ex;
                } else {
                    regualrPrice += product.Price * q.Quantity;
                }
            });
            priceCombinations.Add(regualrPrice);


            TrolleyModel.Specials.ForEach(sp =>
            {
                if (sp.Total < regualrPrice)
                {
                    int returnValue = CheckQuantityLimit(sp.Quantities, TrolleyModel.Quantities);

                    if (returnValue == 0)
                    {
                        priceCombinations.Add(sp.Total);
                    }
                    else if (returnValue < 0)
                    {
                        CombineSpecialsAndRegularPrices(sp, TrolleyModel);
                    }
                    else if (returnValue > 0)
                    {
                        return;
                    }
                }
            });

            return priceCombinations.Min();
        }
        private static void CombineSpecialsAndRegularPrices(SpecialModel special, TrolleyModel TrolleyModel)
        {
            AddRemainingRegularPrice(special, TrolleyModel);

            var combinedSpecials = special;

            //combine with other specials
            TrolleyModel.Specials.ForEach(splQuantity =>
            {
                List<ProductQuantityModel> merged = combinedSpecials.Quantities.Concat(splQuantity.Quantities)
                                               .GroupBy(so => so.Name)
                                               .Select(g => new ProductQuantityModel(g.Key, g.Sum(so => so.Quantity))).ToList();

                var addedSpecial = new SpecialModel();
                addedSpecial.Total = combinedSpecials.Total + splQuantity.Total;
                addedSpecial.Quantities = merged;

                if (addedSpecial.Total < priceCombinations.Min())
                {
                    int returnValue = CheckQuantityLimit(merged, TrolleyModel.Quantities);

                    if (returnValue == 0)
                    {
                        priceCombinations.Add(addedSpecial.Total);
                    }
                    else if (returnValue < 0)
                    {
                        AddRemainingRegularPrice(addedSpecial, TrolleyModel);
                        combinedSpecials = addedSpecial;
                    }
                }


                List<ProductQuantityModel> mergedSeqeunce = special.Quantities.Concat(splQuantity.Quantities)
                                               .GroupBy(so => so.Name)
                                               .Select(g => new ProductQuantityModel(g.Key, g.Sum(so => so.Quantity))).ToList();

                var mergeSequenceSpecial = new SpecialModel();
                mergeSequenceSpecial.Total = special.Total + splQuantity.Total;
                mergeSequenceSpecial.Quantities = mergedSeqeunce;

                if (mergeSequenceSpecial.Total < priceCombinations.Min())
                {
                    int returnValue = CheckQuantityLimit(mergedSeqeunce, TrolleyModel.Quantities);

                    if (returnValue == 0)
                    {
                        priceCombinations.Add(mergeSequenceSpecial.Total);
                    }
                    else if (returnValue < 0)
                    {
                        AddRemainingRegularPrice(mergeSequenceSpecial, TrolleyModel);
                    }
                }
            });
        }

        /// <summary>
        /// Check quantity limits
        /// </summary>
        /// <param name="specialProductQuantityList"></param>
        /// <param name="requiredQuantityList"></param>
        /// <returns></returns>
        private static int CheckQuantityLimit(List<ProductQuantityModel> specialProductQuantityList, List<ProductQuantityModel> requiredQuantityList)
        {
            int limit = 0;
            foreach (var splQuantity in specialProductQuantityList)
            {
                var requiredQuantity = requiredQuantityList.Find(x => x.Name == splQuantity.Name);

                if(requiredQuantity == null)
                {
                    var ex = new Exception(message: "incomplete content");
                    ex.Data.Add("requiredQuantityList: ", requiredQuantityList);
                    throw ex;
                } else if (splQuantity.Quantity > requiredQuantity.Quantity)
                {
                    limit = 1;
                    break;
                }
                else if (splQuantity.Quantity == requiredQuantity.Quantity)
                {
                    limit = 0 + limit;
                }
                else if (splQuantity.Quantity < requiredQuantity.Quantity)
                {
                    limit = -1;
                }
            }

            return limit;
        }

        private static void AddRemainingRegularPrice(SpecialModel special, TrolleyModel TrolleyModel)
        {
            decimal remainingRegularPrice = 0;
            special.Quantities.ForEach(splQuantity =>
            {
                var reqQuntity = TrolleyModel.Quantities.Find(q => q.Name == splQuantity.Name);

                if (reqQuntity == null)
                {
                    var ex = new Exception(message: "incomplete content");
                    ex.Data.Add("remaining reqQuntity: ", reqQuntity);
                    throw ex;
                } else if (reqQuntity.Quantity - splQuantity.Quantity > 0)
                {
                    var productPrice = TrolleyModel.Products.Find(p => p.Name == splQuantity.Name);

                    if (productPrice == null)
                    {
                        var ex = new Exception(message: "incomplete content");
                        ex.Data.Add("productPrice: ", productPrice);
                        throw ex;
                    }

                    remainingRegularPrice += productPrice.Price * (reqQuntity.Quantity - splQuantity.Quantity);
                }

            });
            if (remainingRegularPrice + special.Total < priceCombinations.Min())
            {
                priceCombinations.Add(remainingRegularPrice + special.Total);
            }
        }
    }
}
