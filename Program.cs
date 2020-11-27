using System;
using System.IO;
using System.Threading.Tasks;
using DotNetCoreBetterConsoleApp.Classes;
using DotNetCoreBetterConsoleApp.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace DotNetCoreBetterConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            BuildConfig(builder);

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Build())
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddTransient<IJsonHandler, JsonHandler>();
                    services.AddTransient<IFilesHandler, FilesHandler>();
                })
                .UseSerilog()
                .Build();

            var filesHandlerSvc = ActivatorUtilities.CreateInstance<FilesHandler>(host.Services);
            var jsonHandlerSvc = ActivatorUtilities.CreateInstance<JsonHandler>(host.Services);

            string jsonFilePath = filesHandlerSvc.GetFilePath();

            string fileContent = await filesHandlerSvc.GetFileContentByPath(jsonFilePath);

            jsonHandlerSvc.CreateDirectoriesBasedOnJsonContent(fileContent, jsonFilePath);
        }

        static void BuildConfig(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.json.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .AddEnvironmentVariables();
        }
    }
}
