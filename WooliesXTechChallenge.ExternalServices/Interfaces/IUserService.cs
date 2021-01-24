
using WooliesXTechChallenge.Models;

namespace WooliesXTechChallenge.ExternalServices.Interfaces
{
    public interface IUserService
    {
        TokenDetailsModel GenerateUserToken(string userName);
    }
}
