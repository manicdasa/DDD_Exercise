using GhostWriter.Application.Common.Models;
using System.Threading.Tasks;

namespace GhostWriter.Application.Common.Interfaces
{
    public interface IPayoutService
    {
        Task<OutputModel> CreatePayoutInEuros(string payerID, decimal amount);
        Task<OutputModel> GetAuthorsPaypalCredentials(string code);
    }
}
