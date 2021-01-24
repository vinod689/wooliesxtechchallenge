using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WooliesXTechChallenge.Application.Sort
{
    public interface ISorterFactory
    {
        ISorter GetSorter(string sortOption);
    }

    public class SorterFactory: ISorterFactory
    {
        private readonly IServiceProvider _serviceProvider;
        public SorterFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public ISorter GetSorter(string sortOption)
        {
            switch(sortOption)
            {
                case Constants.SORT_METHOD_LOW:
                    return (ISorter)_serviceProvider.GetService(typeof(LowToHighSorter));
                case Constants.SORT_METHOD_HIGH:
                    return (ISorter)_serviceProvider.GetService(typeof(HighToLowSorter));
                case Constants.SORT_METHOD_ASC:
                    return (ISorter)_serviceProvider.GetService(typeof(AscendingSorter));
                case Constants.SORT_METHOD_DESC:
                    return (ISorter)_serviceProvider.GetService(typeof(DescendingSorter));
                case Constants.SORT_METHOD_RECOMMENDED:
                    return (ISorter)_serviceProvider.GetService(typeof(RecommendedSorter));
                default:
                    throw new ArgumentException("invalid sort option received");
            }

        }
    }
}
