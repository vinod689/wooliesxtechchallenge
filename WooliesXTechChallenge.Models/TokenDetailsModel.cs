using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WooliesXTechChallenge.Models
{
    public class TokenDetailsModel
    {
        public TokenDetailsModel()
        {

        }
        public TokenDetailsModel(string token,string name)
        {
            Token = token;
            Name = name;
        }
        public string Token { get; set; }

        public string Name { get; set; }
    }
}
