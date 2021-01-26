using MediatR;
using System.Collections.Generic;
using WooliesXTechChallenge.Models.Sort;

namespace WooliesXTechChallenge.Application.Sort
{
    public class GetProductsQuery : IRequest<List<SortProductModel>>
    {
        public GetProductsQuery(string sortOption,string token)
        {
            SortOption = sortOption;
            Token = token;
        }

        public string SortOption { get; }
        public string Token { get; }
    }
}
