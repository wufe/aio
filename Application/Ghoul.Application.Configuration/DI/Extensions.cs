using AutoMapper;
using Ghoul.Application.Configuration.Mapping;
using Ghoul.Application.Domain.Repository.Build;
using Ghoul.Domain.Repository.Interface.Build;
using Ghoul.Persistence.Repository.Interface;
using Ghoul.Persistence.Repository.Mongo;
using Microsoft.Extensions.DependencyInjection;

namespace Ghoul.Application.Configuration.DI {
    public static class Extensions {
        public static void AddApplicationMappings(this IServiceCollection services) {
            services
                .AddAutoMapper(cfg => {
                    cfg.AddProfile<DomainPersistenceMappingProfile>();
                    cfg.AddProfile<PersistenceApplicationMappingProfile>();
                });
        }
        public static void AddDomainRepositories(this IServiceCollection services) {
            services.AddTransient<IBuildRepository, BuildRepository>();
        }
    }
}