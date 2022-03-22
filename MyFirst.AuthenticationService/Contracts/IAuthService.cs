using MyFirst.AuthenticationService.Models;
using System.Threading.Tasks;

namespace MyFirst.AuthenticationService.Contracts
{
    public interface IAuthService
    {
        Task<AuthenticationModel> GetTokenAsync(CredentialModel credentialModel);
    }
}
