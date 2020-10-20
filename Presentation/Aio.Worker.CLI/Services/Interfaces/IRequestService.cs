using System.Net.Http;
using System.Threading.Tasks;

namespace Aio.Worker.CLI.Services.Interfaces {
    public interface IRequestService {
        Task<HttpResponseMessage> HttpGetAsync(string requestUri);
    }
}