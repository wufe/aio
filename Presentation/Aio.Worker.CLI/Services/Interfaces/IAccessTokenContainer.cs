using IdentityModel.Client;

namespace Aio.Worker.CLI.Services.Interfaces {
    public interface IAccessTokenAccessor {
        public TokenResponse Token { get; set; }
    }
}