using Infrastructure.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public class CoreDependenciesRegistrar
    {
        public void RegisterDependencies(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<GlobalModelValidator>();
        }
    }
}
