using AutoMapper;
using Ghoul.Presentation.Web.Configuration.Mapping;
using Microsoft.Extensions.DependencyInjection;

namespace Ghoul.Presentation.Web.Configuration.DI {
    public static class Extensions {
        public static void AddPresentationMappings(this IServiceCollection services) {
            services
                .AddAutoMapper(cfg => {
                    cfg.AddProfile<PresentationApplicationMappingProfile>();
                });
        }
    }
}