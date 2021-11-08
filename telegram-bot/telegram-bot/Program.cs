using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace telegram_bot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // @Pracctical_bot token : 2094560790:AAGcEq2UvuXGNCtNtdV6C-t3b6-y6W-X9AM
            string ngrokAddress = "https://2c38-178-69-38-10.ngrok.io"; 
            string BotToken = "2094560790:AAGcEq2UvuXGNCtNtdV6C-t3b6-y6W-X9AM";

            deleteWebHook(BotToken);
            setWebHook(ngrokAddress, BotToken);

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        // funs for dev
        public async static void setWebHook(string ngrokAddress, string BotToken)
        {
            var client = new HttpClient();
            var uri = new Uri("https://api.telegram.org/bot" + BotToken + "/setWebhook?url=" + ngrokAddress + "/api/BotController");

            await client.GetAsync(uri);
        }

        public async static void deleteWebHook(string BotToken)
        {
            var client = new HttpClient();
            var uri = new Uri("https://api.telegram.org/bot" + BotToken + "/deleteWebhook");

            await client.GetAsync(uri);
        }
    }
}
