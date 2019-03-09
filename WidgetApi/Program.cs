using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace WidgetApi
{
    public class Program
    {        

        public static void Main(string[] args)
        {
            //Create the logger
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .MinimumLevel.Debug()
                .WriteTo.ColoredConsole(
                    Serilog.Events.LogEventLevel.Verbose,
                    "{NewLine}{Timestamp:HH:mm:ss} [{Level}] ({CorrelationToken}) {Message}{NewLine}{Exception}")
                .WriteTo.File(
                    @"C:\AppLog\logs.txt",
                    fileSizeLimitBytes: 1_000_000,
                    rollOnFileSizeLimit: true,
                    shared: true,
                    flushToDiskInterval: TimeSpan.FromSeconds(1))
                .WriteTo.Seq("http://localhost:5341")
                .CreateLogger();

            try
            {
                Log.Debug("App Started");
                CreateWebHostBuilder(args).Build().Run();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseSerilog()   //Set serilog as the loggin provider
                
                .UseStartup<Startup>();
    }
}
