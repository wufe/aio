using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace Extensions {

    public static class HttpClientExtensions {
        public static async Task<HttpResponseMessage> GetAsync(this HttpClient httpClient, string requestUri, TokenResponse tokenResponse) {
            httpClient.SetBearerToken(tokenResponse.AccessToken);
            return await httpClient.GetAsync(requestUri);
        }
    }

}