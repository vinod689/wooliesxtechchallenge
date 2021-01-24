using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;
using System;
using System.Reflection;
using WooliesXTechChallenge.API.Controllers;
using WooliesXTechChallenge.Application.CoreLogger;
using WooliesXTechChallenge.Application.Sort;
using WooliesXTechChallenge.Application.Token;
using WooliesXTechChallenge.Application.Trolley;
using WooliesXTechChallenge.ExternalServices.Interfaces;
using WooliesXTechChallenge.ExternalServices.Services;

namespace WooliesXTechChallenge.API
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

            

            //Add services
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITrolleyService, TrolleyService>();

            // Set up dependency injection for controller's logger
            services.AddScoped<ILogger, Logger<TokenController>>();

            services.AddSingleton<IShopperHistoryService, ShopperHistoryService>();

            services.AddSingleton<SorterFactory>();
            services.AddSingleton<LowToHighSorter>()
                .AddSingleton<ISorter, LowToHighSorter>(s => s.GetService<LowToHighSorter>());

            services.AddSingleton<HighToLowSorter>()
                .AddSingleton<ISorter, HighToLowSorter>(s => s.GetService<HighToLowSorter>());

            services.AddSingleton<AscendingSorter>()
                .AddSingleton<ISorter, AscendingSorter>(s => s.GetService<AscendingSorter>());

            services.AddSingleton<DescendingSorter>()
                .AddSingleton<ISorter, DescendingSorter>(s => s.GetService<DescendingSorter>());

            services.AddSingleton<RecommendedSorter>()
                .AddSingleton<ISorter, RecommendedSorter>(s => s.GetService<RecommendedSorter>());

            

            //Add handlers
            services.AddMediatR(typeof(GetProductsHandler).Assembly);

            //ConfigureServices()  - Startup.cs
            // Configure client named as "WooliesX", with various default properties.
            services.AddHttpClient("WooliesX", client =>
            {
                client.BaseAddress = new Uri("http://dev-wooliesx-recruitment.azurewebsites.net/api/resource");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            }).AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(new[]
            {
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(5),
                TimeSpan.FromSeconds(10)
            }));

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ExceptionHandler>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
    app.UseSwagger();

    // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
    // specifying the Swagger JSON endpoint.
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });


        }
    }
}
