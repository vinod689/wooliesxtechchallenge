using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WooliesXTechChallenge.Application.Validators;
using WooliesXTechChallenge.ExternalServices.Interfaces;
using WooliesXTechChallenge.Models.Sort;

namespace WooliesXTechChallenge.Application.Sort
{
    public class GetProductsHandler : IRequestHandler<GetProductsQuery, List<SortProductModel>>
    {
        private readonly IProductService _productService;
        private readonly SorterFactory _sorterFactory;
        private readonly SortRequestValidator _validator;
        public GetProductsHandler(IProductService productService,SorterFactory sorterFactory)
        {
            _productService = productService;
            _sorterFactory = sorterFactory;
            _validator = new SortRequestValidator();
        }

        public async Task<List<SortProductModel>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = new List<SortProductModel>();

            //Validate request object
            var validationResult = _validator.Validate(request.SortOption);

            if (!validationResult.IsValid)
            {
                var ex = new Exception(message: "Invalid sort option supplied");
                ex.Data.Add("sortOption", request.SortOption);
                throw ex;
            }

            try
            {
                //Get products by calling WooliesX API
                products = await _productService.GetProductsAsync(request.Token);

                //Get sorter based on the given sort option
                var iSorter = _sorterFactory.GetSorter(request.SortOption.ToLower());
                await iSorter.SortProducts(products);
            }
            catch(Exception Ex)
            {
                Ex.Data.Add("products", products);
                throw;
            }

            return products;
        }
    }
}
