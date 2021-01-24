using System;
using System.Collections.Generic;
using System.Text;

namespace WooliesXTechChallenge.Application
{
    public static class Constants
    {
        public const string SORT_METHOD_LOW = "low"; // Low to high price
        public const string SORT_METHOD_HIGH = "high"; // High to Low Price
        public const string SORT_METHOD_ASC = "ascending"; // A - Z sort on the name
        public const string SORT_METHOD_DESC = "descending"; // Z - A sort on the name
        public const string SORT_METHOD_RECOMMENDED = "recommended"; // Shopper history popularity
        public static string[] SORT_METHODS = { SORT_METHOD_LOW, SORT_METHOD_HIGH, SORT_METHOD_ASC, SORT_METHOD_DESC, SORT_METHOD_RECOMMENDED };
    }
}
