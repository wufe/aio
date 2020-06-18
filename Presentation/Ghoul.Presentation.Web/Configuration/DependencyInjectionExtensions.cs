using Ghoul.Application.Model.Interface;
using Ghoul.Application.Service;
using Ghoul.Application.Service.Interface;
using Ghoul.Infrastructure.Configuration;
using Ghoul.Presentation.Model.Build;
using Ghould.Database.Mongo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Ghoul.Web.Configuration {
    public static class DependencyInjectionExtensions {
        
        public static void AddDatabaseSettings(this IServiceCollection services, IConfiguration configuration) {
            services
                .Configure<DatabaseSettings>(configuration.GetSection(DatabaseSettings.FIELD_NAME));
            
            services
                .AddSingleton<IDatabaseSettings>(serviceProvider =>
                    serviceProvider.GetRequiredService<IOptions<DatabaseSettings>>().Value);
        }

        public static void AddPresentationModels(this IServiceCollection services)
        {
            services.AddScoped<ICreateBuildModel, CreateBuildInputModel>();
        }

        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IBuildApplicationService, BuildApplicationService>();
        }

    }
}