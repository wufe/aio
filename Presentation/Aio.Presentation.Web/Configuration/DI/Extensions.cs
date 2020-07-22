using AutoMapper;
using Aio.Presentation.Web.Configuration.Mapping;
using Microsoft.Extensions.DependencyInjection;

namespace Aio.Presentation.Web.Configuration.DI {
    public static class Extensions {
        public static void AddPresentationMappings(this IServiceCollection services) {
            services
                .AddAutoMapper(cfg => {
                    cfg.AddProfile<PresentationApplicationMappingProfile>();
                });
        }
    }
}