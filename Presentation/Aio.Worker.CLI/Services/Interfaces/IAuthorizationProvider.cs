using System.Threading.Tasks;
using IdentityModel.Client;

namespace Aio.Worker.CLI.Services.Interfaces {
    public interface IAuthorizationProvider {
        Task<TokenResponse> GetToken();
    }
}