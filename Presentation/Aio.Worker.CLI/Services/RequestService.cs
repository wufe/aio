using System.Net.Http;
using System.Threading.Tasks;
using Aio.Worker.CLI.Services.Interfaces;

namespace Aio.Worker.CLI.Services {
    public class RequestService : IRequestService {
        public async Task<HttpResponseMessage> HttpGetAsync(string requestUri) {
            return null;
        }
    }
}