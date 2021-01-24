using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WooliesXTechChallenge.Application.Sort;

namespace WooliesXTechChallenge.API.Controllers
{
#pragma warning disable CS1591
    [ApiController]
    [Route("api/[controller]")]
    public class SortController : ControllerBase
    {
        protected readonly ILogger Logger;
        private readonly IMediator _mediator;

        public SortController(IMediator mediator, ILogger<TokenController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            Logger = logger;
        }

        // GET
        // api/v1/ExerciseTwo/sort/

        /// <summary>
        /// validates user
        /// </summary>
        /// <returns>A response with products sorted based on provided sort option</returns>
        [HttpGet("sort")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetProducts([FromQuery] string sortOption)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(GetProducts));
            var productList = await _mediator.Send(new GetProductsQuery(sortOption, Environment.GetEnvironmentVariable("AUTHENTICATION_TOKEN")));
            return Ok(productList);
        }
    }
}
