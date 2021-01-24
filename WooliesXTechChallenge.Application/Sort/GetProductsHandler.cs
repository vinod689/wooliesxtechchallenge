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
    public class GetProductsHandler : IRequestHandler<GetProductsQuery, List<ProductModel>>
    {
        private readonly IProductService _productService;
        private readonly SorterFactory _sorterFactory;
        public GetProductsHandler(IProductService productService,SorterFactory sorterFactory)
        {
            _productService = productService;
            _sorterFactory = sorterFactory;
        }

        public async Task<List<ProductModel>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = new List<ProductModel>();

            //Todo Replace with fluent
            //Validate request object
            SortRequestValidator.validateAndThrow(request.SortOption);

            try
            {
                //Get products by calling WooliesX API
                products = await _productService.GetProductsAsync(request.Token);

                //Get sort strategy context  
                var iSorter = _sorterFactory.GetSorter(request.SortOption.ToLower());
                //Apply the strategy based on request sort option
                await iSorter.GetSortedProducts(products);
            }
            catch
            {
                var ex1 = new Exception(message: "unable to retreive products");
                ex1.Data.Add("products", products);
                throw ex1;
            }
            


            return products;
        }
    }
}
