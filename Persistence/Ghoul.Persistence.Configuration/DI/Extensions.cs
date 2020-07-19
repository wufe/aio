using Ghoul.Persistence.Repository.Interface;
using Ghoul.Persistence.Repository.Mongo;
using Ghoul.Persistence.Database.Mongo;
using Microsoft.Extensions.DependencyInjection;

namespace Ghoul.Persistence.Configuration.DI {
    public static class Extensions {

        public static void AddDBContext(this IServiceCollection services)
        {
            services.AddTransient<MongoDBContext>();
        }

        public static void AddGenericRepositories(this IServiceCollection services)
        {
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient(typeof(IReadRepository<>), typeof(ReadRepository<>));
        }
    }
}