using Asp.Versioning;
using Consul;
using IDP.Application.Handlers.Comand.User;
using IDP.Domain.IRepository.Command;
using IDP.Infra.Data;
using IDP.Infra.Repository.Command;
using MediatR;
using System.Reflection;

namespace IDP.Services
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddStackExchangeRedisCache(options =>
            {

                options.Configuration = builder.Configuration.GetValue<string>("CachSetings:Redissetings");


            });
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddMediatR(typeof(UserHandler).GetTypeInfo().Assembly);
            builder.Services.AddScoped<IOTPRepository, OTPRedisRepository>();
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
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
