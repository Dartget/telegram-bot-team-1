using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TelegramBot.Services;
using TelegramBot.WebHookSetup;

namespace TelegramBot
{
    public class Startup
    {
        
       
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            BotConfig = Configuration.GetSection("BotConfiguration").Get<BotConfiguration>();
        }

        public IConfiguration Configuration { get; }
        public BotConfiguration BotConfig { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHostedService<ConfigureWebhook>();

            //services.AddHttpClient("telWebHook")
            //        .AddTypedClient<IWebHookClient>(client
            //            => new WebHookClient(BotConfig, client));

            services.AddHttpClient();

            services.AddScoped<IWebHookClient, WebHookClient>();

            services.AddScoped<HandleUpdateService>();

			      services.AddSingleton(BotConfig);
			      services.AddSingleton<ISetupClient, SetupClient>();

            services
                .AddControllers()
                .AddNewtonsoftJson();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                //var token = BotConfig.BotToken;
                //endpoints.MapControllerRoute(name: "telWebHook",
                //                             pattern: $"bot/{token}",
                //                             new { controller = "BotController", action = "Post" });
                endpoints.MapControllers();
            });
        }
    }
}
