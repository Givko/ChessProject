using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Chess.GameEngine.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddFile(@"Logs/Chess-GameEngine-{Date}.txt");
                    logging.AddEventLog(cfg =>
                    {
                        cfg.LogName = "Chess.GameEngineLogs";
                        cfg.SourceName = "Chess.GameEngine";
                    });
                    logging.AddConsole();
                });
    }
}