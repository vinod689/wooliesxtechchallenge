using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WooliesXTechChallenge.Models.Trolley
{
    public class TrolleyModel
    {
        public List<ProductModel> Products { get; set; }
        public List<SpecialModel> Specials { get; set; }
        public List<ProductQuantityModel> Quantities { get; set; }
    }
}
