using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WooliesXTechChallenge.Application.Validators;
using WooliesXTechChallenge.ExternalServices.Interfaces;

namespace WooliesXTechChallenge.Application.Trolley
{
    public class GetTrolleyTotalHandler: IRequestHandler<GetTrolleyTotalQuery, decimal>
    {
        private readonly ITrolleyService _trolleyService;

        public GetTrolleyTotalHandler()
        {
        }

        public GetTrolleyTotalHandler(ITrolleyService trolleyService)
        {
            _trolleyService = trolleyService;
        }

        public async Task<decimal> Handle(GetTrolleyTotalQuery request, CancellationToken cancellationToken)
        {
            decimal trolleyTotal = 0;
            trolleyTotal = await _trolleyService.GetTrollyTotal(request.TrolleyModel, request.Token);
            return trolleyTotal;
        }
    }
}
