using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth
{
    public static class Extension
    {
        public static void AddJwt(IServiceCollection services,IConfiguration configuration)
        {
            var option =  new JWTOptions();
            var section = configuration.GetSection("Jwt");
            section.Bind(option);
            services.Configure<JWTOptions>(configuration.GetSection("jwt"));
            services.AddSingleton<IJWTHandller,JWTHandler>();
            services.AddAuthentication().
                AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters()
                    { 
                        ValidateAudience = false,
                        ValidateIssuer = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(s: option.SecreteKey))
                    };



                });
        }
    }
}
