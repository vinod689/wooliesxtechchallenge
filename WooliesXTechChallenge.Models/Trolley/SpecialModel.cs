using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WooliesXTechChallenge.Models.Trolley
{
    public class SpecialModel
    {
        public List<ProductQuantityModel> Quantities { get; set; }
        public decimal Total { get; set; }
    }
}
