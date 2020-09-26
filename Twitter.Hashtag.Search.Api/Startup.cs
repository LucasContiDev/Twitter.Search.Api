using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Refit;
using Twitter.Hashtag.Search.Services;
using Twitter.Hashtag.Search.Services.Abstraction;

namespace Twitter.Hashtag.Search.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.TryAddScoped<ITwitterMessageService, TwitterMessageService>();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen();
            services.AddControllersWithViews().AddNewtonsoftJson();
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
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "TwitterMessageSearch"); });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

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