using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using WooliesXTechChallenge.Models;

namespace WooliesXTechChallenge.Application.Token
{
    public class ValidateUserQuery : IRequest<TokenDetailsModel>
    {
        public ValidateUserQuery()
        {
        }
    }

}
