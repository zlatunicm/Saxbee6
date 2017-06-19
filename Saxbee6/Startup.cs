using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SaxBee6.Data;
using AutoMapper;
using SaxBee6.ViewModels;
using SaxBee6.Data.Models;
using Saxbee6.ViewModels;

namespace Saxbee6
{
    public class Startup
    {
        private IConfigurationRoot _config;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            _config = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_config);

            services.AddDbContext<SaxBeeDBContext>();

            services.AddScoped<IApplicationRepository, ApplicationRepository>();
            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
           

            Mapper.Initialize(config =>
            {
                config.CreateMap<PcelinjakViewModel, Pcelinjak>().ReverseMap();
                config.CreateMap<PzajednicaViewModel, PZajednica>().ReverseMap();

            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

      

            app.UseStaticFiles();




            //app.UseMvc(config =>
            //{
            //    config.MapRoute(
            //      name: "Default",
            //      template: "{controller}/{action}/{id?}",
            //      defaults: new { controller = "Home", action = "Index" }
            //      );

            //});


            app.UseMvc(routes =>
            {
                //routes.MapRoute(
                //    name: "webapi",
                //    template: "api/{controller}/{action}/{id?}",
                //    defaults: new { controller = "Home", action = "Index" });

                routes.MapRoute(
                    name: "default",
                    template: "{*url}",
                    defaults: new { controller = "Home", action = "Index" });

            });
        }
    }
}
