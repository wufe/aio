using System.IO;
using Aio.Worker.CLI.Settings;
using Microsoft.Extensions.Configuration;

namespace Aio.Worker.CLI.Configuration {
    public class ApplicationConfiguration {
        private readonly IConfigurationRoot _root;

        private ApplicationConfiguration(IConfigurationRoot root) {
            _root = root;
        }

        public IConfigurationRoot Root => _root;

        public static ApplicationConfiguration Build(ApplicationEnvironment environment) {
            var configurationRoot = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment.Name}.json", optional: false, reloadOnChange: true)
                .Build();

            return new ApplicationConfiguration(configurationRoot);
        }
    }
}