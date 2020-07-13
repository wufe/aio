using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Ghoul.Presentation.Web.Middleware.HostedService {
    public class ChildProcessHandlerHostedService : IHostedService
    {
        private readonly ILogger<ChildProcessHandlerHostedService> _logger;
        private readonly IHostApplicationLifetime _appLifetime;
        private readonly ChildProcessHandlerContainer _processContainer;

        public ChildProcessHandlerHostedService(
            ILogger<ChildProcessHandlerHostedService> logger,
            IHostApplicationLifetime appLifetime,
            ChildProcessHandlerContainer processContainer)
        {
            _logger = logger;
            _appLifetime = appLifetime;
            _processContainer = processContainer;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _appLifetime.ApplicationStopping.Register(OnStopping);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private void OnStopping() {
            _logger.LogInformation("Stopping. Killing child processes.");
            _processContainer.Dispose();
            _logger.LogInformation("Done.");
        }
    }
}