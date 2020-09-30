using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Refit;
using Twitter.Hashtag.Search.Entities;
using Twitter.Search.Services;
using Twitter.Search.Services.Abstraction;

namespace Twitter.Hashtag.Search.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            this.configuration = configuration;
            this.environment  = environment;
        }

        private IConfiguration configuration { get; }
        private IWebHostEnvironment environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<TwitterSearchDatabaseSettings>(
                configuration.GetSection(nameof(TwitterSearchDatabaseSettings)));
            var x = services.AddSingleton<ITwitterSearchDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<TwitterSearchDatabaseSettings>>().Value);
            
            services.AddControllers();
            services.TryAddSingleton<ITwitterSearchDatabaseSettings, TwitterSearchDatabaseSettings>();
            services.TryAddScoped<ITwitterMessageService, TwitterMessageService>();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Twitter Search Api", Version = "v1" });
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Name = "oauth2",
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Implicit = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri(configuration["Jwt:Authority"]+"/protocol/openid-connect/auth"),
                            Scopes = new Dictionary<string, string>
                            {
                                { "web-origins", "OpenID Connect scope for add allowed web origins to the access token" }
                            }
                        },
                        ClientCredentials = new OpenApiOAuthFlow
                        {
                            TokenUrl = new Uri(configuration["Jwt:Authority"]+"/protocol/openid-connect/token"),
                            Scopes = new Dictionary<string, string>
                            {
                                { "web-origins", "OpenID Connect scope for add allowed web origins to the access token" }
                            }
                        }
                    },
                });
 
                    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
                            },
                            new[] {"web-origins"}
                        }
                    });
                });
                
            services.AddControllersWithViews().AddNewtonsoftJson();
            
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.Authority = configuration["Jwt:Authority"];
                o.Audience = configuration["Jwt:Audience"];
                o.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = c =>
                    {
                        c.NoResult();
            
                        c.Response.StatusCode = 500;
                        c.Response.ContentType = "text/plain";
                        return Task.CompletedTask;
                    }
                };
                o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidAudiences = new string[] { "master-realm", "account", configuration["Jwt:Audience"] }
                };
                o.RequireHttpsMetadata = false;
            });
            
            AddRestServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TwitterMessageSearch");
                c.OAuthClientId(configuration["Jwt:Audience"]);
                c.OAuthAppName("Twitter Search Api");
            });

            app.UseHttpsRedirection();

            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseCors(opt =>
            {
                opt.AllowAnyOrigin();
                opt.AllowAnyMethod();
                opt.AllowAnyHeader();
            });

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        /// <summary>
        /// Initialize RestFit configurations
        /// </summary>
        /// <param name="services"></param>
        private void AddRestServices(IServiceCollection services)
        {
            var millisecondsTimeout = 5000;

            services.AddHttpClient("TwitterApi", client =>
                {
                    client.BaseAddress = new Uri("https://api.twitter.com");
                    client.Timeout = TimeSpan.FromMilliseconds(millisecondsTimeout);
                })
                .AddTypedClient(client => RestService.For<ITwitterMessageApiService>(client)
                );
        }
    }
}