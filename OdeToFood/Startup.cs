using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OdeToFood.Data;

namespace OdeToFood
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
            services.AddRazorPages();

            services.AddDbContextPool<OdeToFoodDbContext>(options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("OdeToFoodDb"));
                });
         
            // This is where you tell the Project, "If you need a 'IRestaurantData' use, 'InMemoryRestaurantData'" or whatever the second parameter is.
            services.AddScoped<IRestaurantData, SqlRestaurantData>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.Use(async (context, next) =>
            {
                logger.LogInformation("Request: " + context.Request.Path);
                
                if (context.Request.Path.StartsWithSegments("/hello"))
                {
                    await context.Response.WriteAsync("Hello, World!");
                }
                else
                {
                    await next.Invoke();
                    if (context.Response.StatusCode == 200)
                    {
                        // Could do a thing
                    }
                }
            });
            
            
            // app.Use(SayHelloMiddleware(logger));
            
            // app.Use(CustomLoggerMiddleWare);
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });
        }

        // private RequestDelegate CustomLoggerMiddleWare(RequestDelegate arg)
        // {
        //     return async context =>
        //     {
        //         if (context.Request != null)
        //         {
        //             _logger.LogError("Network call made: " + context.Request.Body);
        //         }
        //         
        //         await arg(context);
        //     };
        // }

        private RequestDelegate SayHelloMiddleware(ILogger<Startup> logger, RequestDelegate nextMiddleWare)
        {
            return async context =>
            {
                if (context.Request.Path.StartsWithSegments("/hello"))
                {
                    await context.Response.WriteAsync("Hello, World!");
                }
                else
                {
                    await nextMiddleWare(context);
                    if (context.Response.StatusCode == 200)
                    {
                        // Could do a thing
                    }
                }
            };
        }
    }
}