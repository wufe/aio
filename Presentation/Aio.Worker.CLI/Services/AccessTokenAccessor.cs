using Aio.Worker.CLI.Services.Interfaces;
using IdentityModel.Client;

namespace Aio.Worker.CLI.Services {
    public class AccessTokenAccessor : IAccessTokenAccessor {
        public TokenResponse Token { get; set; } = null;
    }
}