using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Authentication
{
    public static class JwtAuthExtension
    {
        public static void AddJwtAuthentication(this IServiceCollection services, IConfigurationBuilder builder)
        {
            IConfigurationRoot _config = builder.Build();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            // Adding Jwt Bearer
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = _config["JWT:ValidAudience"],
                    ValidIssuer = _config["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]))
                };
            });
        }
    }
}