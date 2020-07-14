using AutoMapper;
using Ghoul.Application.Configuration.Mapping;
using Ghoul.Persistence.Repository.Interface;
using Ghoul.Persistence.Repository.Mongo;
using Microsoft.Extensions.DependencyInjection;

namespace Ghoul.Application.Configuration.DI {
    public static class Extensions {
        public static void AddApplicationMappings(this IServiceCollection services) {
            services
                .AddAutoMapper(cfg => {
                    // Da domain a persistence
                    cfg.AddProfile<DomainPersistenceMappingProfile>();
                    // Da persistence a application
                    cfg.AddProfile<PersistenceApplicationMappingProfile>();
                });
        }
    }
}