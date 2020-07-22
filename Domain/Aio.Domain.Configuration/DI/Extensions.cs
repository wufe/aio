using Aio.Domain.Service;
using Aio.Domain.Service.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Aio.Domain.Configuration.DI {

    public static class Extensions {
        public static void AddDomainServices(this IServiceCollection services) {
            services.AddTransient<IBuildDomainService, BuildDomainService>();
        }
    }
}