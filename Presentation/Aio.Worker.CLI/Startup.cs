using System;
using Aio.Worker.CLI.Configuration;
using Aio.Worker.CLI.Services;
using Aio.Worker.CLI.Services.Interfaces;
using Aio.Worker.CLI.Settings;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Aio.Worker.CLI {
    public class Startup {
        private ApplicationEnvironment _environment;
        private ContainerBuilder _builder;
        private IConfigurationRoot _configuration;
        private IContainer _container;

        private Startup() {}

        public static Startup Build() => new Startup();

        public void Start()
        {
            RetrieveEnvironment();
            RetrieveConfiguration();
            StartDependencyInjectionContainer();
            RegisterServices();
            BuilderDependencyInjectionContainer();

            using (var scope = _container.BeginLifetimeScope())
            {
                var machineName = Environment.MachineName;
                Console.WriteLine(machineName);
                var authorizationProvider = scope.Resolve<IAuthorizationProvider>();
                var token = authorizationProvider.GetToken();
            }
        }

        private void RetrieveEnvironment() {
            _environment = new ApplicationEnvironment(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
            Log.Debug($"Using environment {_environment.Name}.");
        }

        private void RetrieveConfiguration() {
            _configuration = ApplicationConfiguration
                .Build(_environment)
                .Root;
        }

        private void StartDependencyInjectionContainer() {
            _builder = new ContainerBuilder();
        }

        private void RegisterServices() {

            #region Default .NET Core DI Container
            var services = new ServiceCollection();
            services.Configure<IdentitySettings>(_configuration.GetSection(IdentitySettings.SECTION_NAME));
            _builder.Populate(services);
            #endregion

            _builder.RegisterInstance(_environment)
                .As<ApplicationEnvironment>();

            _builder.RegisterInstance(_configuration)
                .As<IConfigurationRoot>();

            _builder.RegisterType<AuthorizationProvider>().As<IAuthorizationProvider>();
            _builder.RegisterType<AccessTokenAccessor>().As<IAccessTokenAccessor>()
                .SingleInstance();
            _builder.RegisterType<RequestService>().As<IRequestService>();
        }

        private void BuilderDependencyInjectionContainer() {
            _container = _builder.Build();
        }
    }
}