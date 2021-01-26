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
            Name = name;
            Token = token;
        }

        public string Name { get; set; }
        public string Token { get; set; }

    }
}
