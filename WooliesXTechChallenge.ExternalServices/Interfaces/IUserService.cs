
using System.Threading.Tasks;
using WooliesXTechChallenge.Models;

namespace WooliesXTechChallenge.ExternalServices.Interfaces
{
    public interface IUserService
    {
        Task<TokenDetailsModel> GenerateUserToken(string userName);
    }
}
