using System;
using System.IO;
using System.Reflection;
using BrewCoffee.Services;
using BrewCoffee.Services.APIProxy;
using BrewCoffee.Services.Helpers;
using BrewCoffee.Services.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace BrewCoffee
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
            services.AddOptions();
            services.Configure<ApiSettings>(Configuration.GetSection("ApiSettings"));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Brew Coffee",
                    Description = "Coffee Details",
                    Contact = new OpenApiContact
                    {
                        Name = "Jayateerth Kulkarni",
                        Url = new Uri("https://github.com/dreamjaya88"),
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            services.AddHttpClient();
            services.AddSingleton<ICoffeeCounter, CoffeeCounter>();
            services.AddSingleton<IDateTimeHelper, DateTimeHelper>();
            services.AddScoped<ICoffeeBrewingServices, CoffeeBrewingServices>();
            services.AddHttpClient<IWeatherProxy, WeatherProxy>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Brew My Coffee V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
