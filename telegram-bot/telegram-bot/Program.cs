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
            // example
            // @Pracctical_bot token : 2094560790:AAGcEq2UvuXGNCtNtdV6C-t3b6-y6W-X9AM
            // ngrok address = "https://40f6-178-69-38-10.ngrok.io";

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        
    }
}
