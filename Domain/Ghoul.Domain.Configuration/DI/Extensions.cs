using Ghoul.Domain.Service;
using Ghoul.Domain.Service.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Ghoul.Domain.Configuration.DI {

    public static class Extensions {
        public static void AddDomainServices(this IServiceCollection services) {
            services.AddTransient<IBuildDomainService, BuildDomainService>();
        }
    }
}