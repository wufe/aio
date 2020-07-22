using Aio.Persistence.Repository.Interface;
using Aio.Persistence.Repository.Mongo;
using Aio.Persistence.Database.Mongo;
using Microsoft.Extensions.DependencyInjection;

namespace Aio.Persistence.Configuration.DI {
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