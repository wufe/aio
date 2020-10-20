using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Aio.Worker.CLI.Configuration;
using Aio.Worker.CLI.Services;
using Aio.Worker.CLI.Services.Interfaces;
using Autofac;
using IdentityModel.Client;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Aio.Worker.CLI
{
    class Program
    {
        static async Task Main(string[] args)
        {

            Log.Logger = new LoggerConfiguration()
                .WriteTo
                .Console(Serilog.Events.LogEventLevel.Debug)
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .CreateLogger();

            Startup.Build()
                .Start();
        }
    }
}
