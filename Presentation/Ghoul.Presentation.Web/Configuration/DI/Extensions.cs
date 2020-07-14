using AutoMapper;
using Ghoul.Presentation.Web.Configuration.Mapping;
using Microsoft.Extensions.DependencyInjection;

namespace Ghoul.Presentation.Web.Configuration.DI {
    public static class Extensions {
        public static void AddPresentationMappings(this IServiceCollection services) {

            // Usa "AddAutomapper" (extension method fornito da automapper stesso)
            // per iniettarsi nel container di dependency injection di ASPNET core.

            // Le ISTRUZIONI fornite ad automapper, sono quelle di prendere in considerazione
            // le regole nel file PresentationApplicationMappingProfile.
            //
            // Per "Profile", si intende proprio un set di istruzioni per automapper,
            // in modo che capisca come convertire un oggetto in un altro.
            services
                .AddAutoMapper(cfg => {
                    cfg.AddProfile<PresentationApplicationMappingProfile>();
                });
        }
    }
}