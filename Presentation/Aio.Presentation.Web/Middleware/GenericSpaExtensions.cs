using Aio.Presentation.Web.Middleware.HostedService;
using Microsoft.AspNetCore.SpaServices;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Diagnostics;

namespace Aio.Web.Middleware
{
    public static class GenericSpaExtensions {

        public static void AddYarn(this IServiceCollection services) {
            services.AddSingleton<ChildProcessHandlerContainer>();
            services.AddHostedService<ChildProcessHandlerHostedService>();
        }

        public static void UseYarn(this ISpaBuilder spaBuilder, string directory, string command = "dev") {
            var pid = Process.GetCurrentProcess().Id;
            var processInfo = new ProcessStartInfo() {
                UseShellExecute = false,
                FileName = "node",
                Arguments = $"config/phandler.js {pid} yarn {command}",
                WorkingDirectory = directory
            };
            var process = Process.Start(processInfo);
            var processContainer = spaBuilder.ApplicationBuilder.ApplicationServices.GetService<ChildProcessHandlerContainer>();
            processContainer.RegisterChildProcess(process);
        }
    }
}