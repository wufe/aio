using Microsoft.AspNetCore.SpaServices;
using System.Diagnostics;

namespace Ghoul.Web.Middleware
{
    public static class GenericSpaExtensions {
        public static void UseYarn(this ISpaBuilder spaBuilder, string directory, string script = "dev") {
            var processInfo = new ProcessStartInfo() {
                UseShellExecute = false,
                FileName = "yarn",
                Arguments = script,
                WorkingDirectory = directory
            };
            var process = Process.Start(processInfo);
        }
    }
}