using System;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using NLog.Web;

namespace TheChallenge.Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            try
            {
                logger?.Info(() => "Initializing and starting application.");
                BuildWebHost(args).Run();

            }
            catch (Exception e)
            {
                logger?.Error(e, "Application startup failed");
                throw;
            }
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            var config = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("hosting.json", optional: true)
                                .AddJsonFile("appsettings.json", optional: true)
                                .AddUserSecrets<Startup>()
                                .Build();

            return WebHost.CreateDefaultBuilder(args)
                          .UseStartup<Startup>()
                          .UseConfiguration(config)
                          .UseNLog()
                          .Build();
        }
    }
}
