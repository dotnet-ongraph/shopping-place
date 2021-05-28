using Microsoft.Extensions.Configuration;

namespace Core.Utilities
{
    public class ConfigurationService
    {
        private static IConfiguration _configuration = null;

        public static IConfiguration Configuration
        {
            get
            {
                if (_configuration == null)
                {
                    var builder = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json");
                    _configuration = builder.Build();
                }
                return _configuration;

            }
        }
    }
}
