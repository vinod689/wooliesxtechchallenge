using System;
using WooliesXTechChallenge.Models;
using WooliesXTechChallenge.ExternalServices.Interfaces;

namespace WooliesXTechChallenge.ExternalServices.Services
{
    /// <summary>
    /// Service to generate user tokens
    /// </summary>
    public class UserService : IUserService
    {
        /// <summary>
        /// Generate token for the user
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public TokenDetailsModel GenerateUserToken(string userName)
        {
            var tokenDetails = new TokenDetailsModel();

            if (userName != string.Empty)
            {
                tokenDetails.Name = userName;
                tokenDetails.Token = Environment.GetEnvironmentVariable("AUTHENTICATION_TOKEN");
            }
            else
            {
                return null;
            }
            return tokenDetails;
        }
    }
}
