using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WooliesXTechChallenge.Models.Sort
{
    public class ProductModel
    {
        public ProductModel()
        {

        }

        public ProductModel(string name,decimal price,decimal quantity)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public int popularity;

        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
    }
}
