using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WooliesXTechChallenge.Application.Token;

namespace WooliesXTechChallenge.API.Controllers
{
#pragma warning disable CS1591
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : ControllerBase
    {
        protected readonly ILogger Logger;
        private readonly IMediator _mediator;

        public TokenController(IMediator mediator, ILogger<TokenController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            Logger = logger;
        }

        // GET
        // api/v1/ExerciseOne/user/

        /// <summary>
        /// Generate user token
        /// </summary>
        /// <returns>A response with name and token</returns>
        /// <response code="200">Returns name and token</response>
        /// <response code="400">For bad request</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("user")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GenerateUserToken()
        {
            Logger?.LogDebug("'{0}' has been invoked", nameof(GenerateUserToken));

            var token = await _mediator.Send(new ValidateUserQuery());
            return Ok(token);
        }
    }
}
