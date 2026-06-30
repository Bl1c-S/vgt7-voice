namespace API.Extensions;

public static class ServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        public void AddApplicationServices()
        {
            services.AddControllers();
            services.AddOpenApi();
        }
    }
}