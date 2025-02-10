using Asp.Versioning;
using Consul;
using IDP.Application.Handlers.Comand.User;
using IDP.Domain.IRepository.Command;
using IDP.Infra.Data;
using IDP.Infra.Repository.Command;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Reflection;

namespace IDP.Services
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var builder = WebApplication.CreateBuilder(args);

                // Add services to the container.
                builder.Services.AddRazorPages();

                // Configure OAuth 2.0 with Google
                builder.Services.AddAuthentication(options =>
                {
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                })
                .AddCookie()
                .AddOpenIdConnect(options =>
                {
                    options.Authority = "https://accounts.google.com"; // Google OAuth provider
                    options.ClientId = builder.Configuration["Google:ClientId"]; // Load from appsettings.json
                    options.ClientSecret = builder.Configuration["Google:ClientSecret"]; // Load from appsettings.json
                    options.ResponseType = OpenIdConnectResponseType.Code;
                    options.SaveTokens = true;
                    options.Scope.Add("openid");
                    options.Scope.Add("profile");
                    options.Scope.Add("email");
                    options.CallbackPath = new PathString("/signin-oidc"); // Ensure this matches the callback URL in Google Developer Console
                    options.SignedOutCallbackPath = new PathString("/signout-callback-oidc");
                    options.RemoteAuthenticationTimeout = TimeSpan.FromMinutes(10);
                });

                // Configure Redis caching
                builder.Services.AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = builder.Configuration.GetValue<string>("CachSetings:Redissetings");
                });

                // Add controllers and API versioning
                builder.Services.AddControllers();
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                // Configure MediatR
                builder.Services.AddMediatR(typeof(UserHandler).GetTypeInfo().Assembly);

                // Register repositories
                builder.Services.AddScoped<IOTPRepository, OTPRedisRepository>();

                // Configure API versioning
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

                // Configure DbContext for CAP
                builder.Services.AddDbContext<ShopComandDBContext>(options =>
                {
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                });

                // Configure CAP with EF Core
                builder.Services.AddCap(options =>
                {
                    options.UseEntityFramework<ShopComandDBContext>(); // Use EF Core as the storage provider
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
                    options.FailedRetryInterval = 5; // seconds
                });

                // Configure JWT authentication (if needed)
                Auth.Extension.AddJwt(builder.Services, builder.Configuration);

                var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }
                else
                {
                    app.UseExceptionHandler("/Error");
                    app.UseHsts();
                }

                app.UseHttpsRedirection();
                app.UseStaticFiles();
                app.UseRouting();

                // Enable authentication and authorization
                app.UseAuthentication();
                app.UseAuthorization();

                app.MapRazorPages();
                app.MapControllers();

                app.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Application failed to start: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                throw;
            }
        }

    }
}