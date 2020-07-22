using AutoMapper;
using Aio.Application.Configuration.Mapping;
using Aio.Application.Domain.Repository.Build;
using Aio.Domain.Repository.Interface.Build;
using Aio.Persistence.Repository.Interface;
using Aio.Persistence.Repository.Mongo;
using Microsoft.Extensions.DependencyInjection;

namespace Aio.Application.Configuration.DI {
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