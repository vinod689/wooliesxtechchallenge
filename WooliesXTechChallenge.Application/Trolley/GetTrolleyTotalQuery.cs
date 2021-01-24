using MediatR;
using WooliesXTechChallenge.Models.Trolley;

namespace WooliesXTechChallenge.Application.Trolley
{
    public class GetTrolleyTotalQuery : IRequest<decimal>
    {
        public GetTrolleyTotalQuery(TrolleyModel trolleyModel,string token)
        {
            TrolleyModel = trolleyModel;
            Token = token;
        }

        public TrolleyModel TrolleyModel { get; }
        public string Token { get; }
    }
}
