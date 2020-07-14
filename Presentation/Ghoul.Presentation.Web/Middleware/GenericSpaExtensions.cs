using Ghoul.Presentation.Web.Middleware.HostedService;
using Microsoft.AspNetCore.SpaServices;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Diagnostics;

namespace Ghoul.Web.Middleware
{
    public static class GenericSpaExtensions {

        public static void AddYarn(this IServiceCollection services) {
            services.AddSingleton<ChildProcessHandlerContainer>();
            services.AddHostedService<ChildProcessHandlerHostedService>();
        }

        public static void UseYarn(this ISpaBuilder spaBuilder, string directory, string script = "dev") {
            var processInfo = new ProcessStartInfo() {
                UseShellExecute = false,
                FileName = "node",
                Arguments = $"config/phandler.js yarn {script}",
                WorkingDirectory = directory
            };
            var process = Process.Start(processInfo);
            var processContainer = spaBuilder.ApplicationBuilder.ApplicationServices.GetService<ChildProcessHandlerContainer>();
            processContainer.RegisterChildProcess(process);
        }
    }
}