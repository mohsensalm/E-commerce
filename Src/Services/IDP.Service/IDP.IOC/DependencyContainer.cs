using Asp.Versioning;
using IDP.Application.Handlers.Comand.User;
using IDP.Domain.IRepository.Command;
using IDP.Infra.Data;
using IDP.Infra.Repository.Command;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IDP.IOC
{
    public static class DependencyContainer
    {
        public static void RegisterService(this WebApplicationBuilder builder)
        {
            builder.Services.AddMediatR(typeof(UserHandler).GetTypeInfo().Assembly);
            builder.Services.AddScoped<IOTPRepository, OTPRedisRepository>();
            builder.Services.AddScoped<ShopComandDBContext>();
            builder.Services.AddScoped<ShopQueryDBContext>();

            //builder.Services.AddSingleton<ShopDBContext>();
            builder.Services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1);
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new UrlSegmentApiVersionReader(),
                    new HeaderApiVersionReader("X-Api-Version"));
            })
                         .AddMvc() // This is needed for controllers
                          .AddApiExplorer(options =>
                          {
                              options.GroupNameFormat = "'v'V";
                              options.SubstituteApiVersionInUrl = true;
                          });
            builder.Services.AddCap(options =>
            {
                //options.UseEntityFramework<ShopDBContext>();
                options.UseDashboard(path => path.PathMatch = "/cap");
                options.UseRabbitMQ(options =>
                {
                    options.ConnectionFactoryOptions = options =>
                    {
                        options.Ssl.Enabled = false;
                        options.HostName = "localhost";
                        options.UserName = "guest";
                        options.Password = "guest";
                        options.Port = 5672;
                    };
                });
                options.FailedRetryCount = 10;
                options.FailedRetryInterval = 5;//second
            });

            Auth.Extension.AddJwt(builder.Services, builder.Configuration);

            builder.Services.AddMassTransit(busconfig =>
            {
                busconfig.AddEntityFrameworkOutbox<ShopComandDBContext>(o =>
                {
                    o.QueryDelay = TimeSpan.FromSeconds(30);
                    o.UseSqlServer().UseBusOutbox();
                });

                busconfig.SetKebabCaseEndpointNameFormatter();
                busconfig.UsingRabbitMq(  );

            } );

        }
    }
}
