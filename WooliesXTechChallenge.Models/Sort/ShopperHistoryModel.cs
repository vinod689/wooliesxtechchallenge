using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WooliesXTechChallenge.Models.Sort
{
    public class ShopperHistoryModel
    {
        public int CustomerId { get; set; }
        public List<ProductModel> Products { get; set; }
    }
}
