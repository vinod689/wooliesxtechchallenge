using System.Threading.Tasks;
using WooliesXTechChallenge.Models.Trolley;

namespace WooliesXTechChallenge.ExternalServices.Interfaces
{
    public interface ITrolleyService
    {
        Task<decimal> GetTrollyTotal(TrolleyModel TrolleyModel,string token);
    }
}
