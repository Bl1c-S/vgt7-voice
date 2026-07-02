using Application.Models.AiModel;
using Infrastructure.AI;
using Infrastructure.Logger;
using Infrastructure.Options;
using Serilog.Core;

namespace API.Extensions;

public static class WebApplicationBuilderExtensions
{
    extension(WebApplicationBuilder builder)
    {
        public void AddApplicationServices()
        {
            var services = builder.Services;
            var cfg = builder.Configuration;
            
            services.AddControllers();
            services.AddOpenApi();
            
            services.ConfigureLogger(cfg);
            services.ConfigureAiServices(cfg);
        }
    }

    extension(IServiceCollection services)
    {
        private void ConfigureAiServices(ConfigurationManager cfg)
        {
            services.Configure<AiOptions>(cfg.GetSection("AI"));
            services.AddSingleton<AiManagerFactory>();
            services.AddSingleton<GoogleAiManager>(provider => 
            {
                var factory = provider.GetRequiredService<AiManagerFactory>();
                return (GoogleAiManager)factory.Create(AiModelTypes.Gemini20Flash);
            });
        }
        
        private void ConfigureLogger(ConfigurationManager cfg)
        {
            services.Configure<ConnectionOptions>(cfg.GetSection("PSQL"));
            services.AddSingleton<Vgt7LoggerBuilder>();
            services.AddSingleton<Logger>(provider =>
            {
                var builder = provider.GetRequiredService<Vgt7LoggerBuilder>();
                return builder.Build();
            });
        }
    }
}