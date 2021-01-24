using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WooliesXTechChallenge.Application.Trolley;
using WooliesXTechChallenge.Models.Trolley;

namespace WooliesXTechChallenge.API.Controllers
{
#pragma warning disable CS1591
    [ApiController]
    [Route("api/[controller]")]
    public class TrolleyController : ControllerBase
    {
        protected readonly ILogger Logger;
        private readonly IMediator _mediator;

        public TrolleyController(IMediator mediator, ILogger<TokenController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            Logger = logger;
        }
        // POST
        // api/v1/ExerciseThree/trolleyTotal/

        /// <summary>
        /// gets the lowest possible total based on provided lists of prices, specials and quantities
        /// </summary>
        /// <param name="TrolleyModel"></param>
        /// <response code="200">Returns the name and token</response>
        /// <response code="400">For bad request</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpPost("trolleyTotal")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> TrolleyTotal([FromBody] TrolleyModel TrolleyModel)
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(TrolleyTotal));

            var trolleyTotal = await _mediator.Send(new GetTrolleyTotalQuery(TrolleyModel, Environment.GetEnvironmentVariable("AUTHENTICATION_TOKEN")));
            return Ok(trolleyTotal);

        }
    }
}
