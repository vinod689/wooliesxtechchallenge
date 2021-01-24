using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WooliesXTechChallenge.Application.Validators
{
    public static class SortRequestValidator
    {
        public static void validateAndThrow(string sortOption)
        {
            if(sortOption == null)
            {
                var ex = new Exception(message: "sortOption is null");
                ex.Data.Add("sortOption", sortOption);
                throw ex;
            } else if (Array.IndexOf(Constants.SORT_METHODS, sortOption.ToLower()) < 0)
            {
                var ex = new Exception(message: "Invalid sort option supplied");
                ex.Data.Add("sortOption", sortOption);
                throw ex;
            }
        }
    }
}
