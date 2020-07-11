using AutoMapper;
using Ghoul.Application.Configuration.Mapping;
using Ghoul.Persistence.Repository.Interface;
using Ghoul.Persistence.Repository.Mongo;
using Microsoft.Extensions.DependencyInjection;

namespace Ghoul.Application.Configuration.DI {
    public static class Extensions {
        public static void AddDomainPersistenceMappings(this IServiceCollection services) {
            services
                .AddAutoMapper(cfg => {
                    cfg.AddProfile<DomainPersistenceMappingProfile>();
                    cfg.AddProfile<PersistenceApplicationMappingProfile>();
                });
        }
    }
}