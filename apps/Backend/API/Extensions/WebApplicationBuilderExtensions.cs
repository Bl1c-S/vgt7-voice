using API.Conventions;
using API.Options;
using API.Services.Auth;
using Application.Services.AI;
using Infrastructure.Data;
using Infrastructure.Logger;
using Infrastructure.Model;
using Infrastructure.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace API.Extensions;

public static class WebApplicationBuilderExtensions
{
    extension(WebApplicationBuilder builder)
    {
        public void AddApplicationServices()
        {
            var services = builder.Services;
            var cfg = builder.Configuration;
            
            var connectionOptions = GetOptions<ConnectionOptions>("Connection", cfg);
            var aiOptions = GetOptions<AiOptions>("AI", cfg);
            var authOptions = GetOptions<AuthOptions>("Auth", cfg);

            builder.ConfigureLogger(connectionOptions);
            services.ConfigureOptions(cfg);

            services.ConfigureEntityFramework(cfg, connectionOptions);
            services.ConfigureAuthentication(cfg, authOptions);
            services.ConfigureAiServices(cfg, aiOptions);

            services.AddControllers(options => { options.Conventions.Add(new ApiPrefixConvention("api")); });
            services.AddOpenApi();
        }

        private static TOptions GetOptions<TOptions>(string sectionName, ConfigurationManager cfg)
            where TOptions : class
        {
            var section = cfg.GetSection(sectionName);
            var options = section.Get<TOptions>()!;
            return options;
        }

        private void ConfigureLogger(ConnectionOptions options)
        {
            builder.Host.UseSerilog((context, services, configuration) =>
            {
                var loggerBuilder = new Vgt7LoggerBuilder(options.Psql);
                var logger = loggerBuilder.Build();

                configuration.ReadFrom.Services(services)
                    .WriteTo.Logger(logger);
            });
        }
    }

    extension(IServiceCollection services)
    {
        private void ConfigureEntityFramework(ConfigurationManager cfg, ConnectionOptions options)
        {
            services.AddDbContext<Vgt7UserDbContext>(op =>
                op.UseNpgsql(options.Psql));

            services.AddIdentityCore<Vgt7User>().AddEntityFrameworkStores<Vgt7UserDbContext>();
        }

        private void ConfigureAuthentication(ConfigurationManager cfg, AuthOptions options)
        {
            services.AddScoped<TokenService>();
            services.AddScoped<AuthService>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(op =>
                {
                    op.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,

                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = options.GetSymmetricSecurityKey()
                    };
                });
        }

        private void ConfigureAiServices(ConfigurationManager cfg, AiOptions options)
        {
            services.AddSingleton<AiManagerFactory>();
            services.AddSingleton<GoogleAiManager>(provider =>
            {
                var factory = provider.GetRequiredService<AiManagerFactory>();
                return (GoogleAiManager)factory.Create(options.DefaultGoogleAiModel);
            });
            services.AddSingleton<OpenAiManager>(provider =>
            {
                var factory = provider.GetRequiredService<AiManagerFactory>();
                return (OpenAiManager)factory.Create(options.DefaultOpenAiModel);
            });
        }

        private void ConfigureOptions(ConfigurationManager cfg)
        {
            services.AddOptions<AiOptions>()
                .Bind(cfg.GetSection("AI")).ValidateOnStart();
            services.AddOptions<AuthOptions>()
                .Bind(cfg.GetSection("Auth")).ValidateOnStart();
            services.AddOptions<ConnectionOptions>()
                .Bind(cfg.GetSection("Connection")).ValidateOnStart();
        }
    }
}