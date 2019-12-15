using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using MISIS.CI.API.BusinessLogic;
using MISIS.CI.API.BusinessLogic.Interfaces;
using MISIS.CI.API.Infrastructure.Settings;
using MISIS.CI.Storage.BusinessLogic.Interfaces;
using MISIS.CI.Storage.Infrastructure;
using OpenTracing;
using OpenTracing.Util;

namespace MISIS.CI.Storage
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
            services.AddMvc();
            services.AddScoped<IStorageLogic, StorageLogic>();
            services.AddScoped<IStorageRepository, StorageRepository>();
            services.Configure<StorageSettings>(Configuration.GetSection("StorageSettings"));


            services.AddSingleton(cli =>
            {
                var loggerFactory = new LoggerFactory();
                var config = Jaeger.Configuration.FromEnv(loggerFactory);
                var tracer = config.GetTracer();

                if (!GlobalTracer.IsRegistered())
                {
                    // Allows code that can't use DI to also access the tracer.
                    GlobalTracer.Register(tracer);
                }
                return tracer;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=storage}/{action=Index}/{id?}");
            });
            //app.UseHttpsRedirection();
        }
    }
}
