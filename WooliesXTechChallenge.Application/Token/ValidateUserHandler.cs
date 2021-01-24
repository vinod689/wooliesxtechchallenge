using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WooliesXTechChallenge.Models;
using WooliesXTechChallenge.ExternalServices.Interfaces;

namespace WooliesXTechChallenge.Application.Token
{
    public class ValidateUserHandler : IRequestHandler<ValidateUserQuery, TokenDetailsModel>
    {
        private readonly IUserService _userService;
        public ValidateUserHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<TokenDetailsModel> Handle(ValidateUserQuery request, CancellationToken cancellationToken)
        {
            string user = "Vinod Patwari";

            return await Task.Run(() =>
            {
                //Generate user token
                var token = _userService.GenerateUserToken(user);
                return token;
            });

        }
    }
}
