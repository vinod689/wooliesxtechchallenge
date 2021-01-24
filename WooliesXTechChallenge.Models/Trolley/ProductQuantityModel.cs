using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WooliesXTechChallenge.Models.Trolley
{
    public class ProductQuantityModel
    {
        public ProductQuantityModel()
        {
        }
        public ProductQuantityModel(string name, int quantity)
        {
            Name = name;
            Quantity = quantity;
        }
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}
