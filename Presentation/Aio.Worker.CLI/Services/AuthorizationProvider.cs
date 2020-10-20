using System;
using System.Net.Http;
using System.Threading.Tasks;
using Aio.Worker.CLI.Services.Interfaces;
using Aio.Worker.CLI.Settings;
using IdentityModel.Client;
using Microsoft.Extensions.Options;

namespace Aio.Worker.CLI.Services {
    public class AuthorizationProvider : IAuthorizationProvider {
        private readonly IOptionsSnapshot<IdentitySettings> _identitySettings;

        public AuthorizationProvider(IOptionsSnapshot<IdentitySettings> identitySettings)
        {
            _identitySettings = identitySettings;
        }

        public async Task<TokenResponse> GetToken() {

            var identitySettings = _identitySettings.Value;

            var authorityServer = identitySettings.Authority;
            var clientId = identitySettings.ClientId;
            var clientSecret = identitySettings.ClientSecret;
            var scope = identitySettings.Scope;

            var httpClient = new HttpClient();
            var discovery = await httpClient.GetDiscoveryDocumentAsync(authorityServer);
            if (discovery.IsError)
            {
                throw new Exception(discovery.Error);
            }

            var tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest()
            {
                Address = discovery.TokenEndpoint,

                ClientId = clientId,
                ClientSecret = clientSecret,
                Scope = scope,

            });

            if (tokenResponse.IsError)
            {
                throw new Exception(tokenResponse.Error);
            }

            return tokenResponse;
        }
    }
}