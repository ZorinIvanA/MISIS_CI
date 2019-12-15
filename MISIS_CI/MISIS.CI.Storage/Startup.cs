using System;
using Google.Cloud.Diagnostics.AspNetCore;
using Google.Cloud.Diagnostics.Common;
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
using Stackdriver;

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
            services.AddOptions();
            services.Configure<StackdriverOptions>(
                Configuration.GetSection("Stackdriver"));
            services.AddGoogleExceptionLogging(options =>
            {
                options.ProjectId = Configuration["Stackdriver:ProjectId"];
                options.ServiceName = Configuration["Stackdriver:ServiceName"];
                options.Version = Configuration["Stackdriver:Version"];
            });

            // Add trace service.
            services.AddGoogleTrace(options =>
            {
                options.ProjectId = Configuration["Stackdriver:ProjectId"];
                options.Options = TraceOptions.Create(
                    bufferOptions: BufferOptions.NoBuffer());
            });


            services.AddMvc();
            services.AddScoped<IStorageLogic, StorageLogic>();
            services.AddScoped<IStorageRepository, StorageRepository>();
            services.Configure<StorageSettings>(Configuration.GetSection("StorageSettings"));


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {            
            // Configure error reporting service.
            app.UseGoogleExceptionLogging();
            // Configure trace service.
            app.UseGoogleTrace();

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=storage}/{action=Index}/{id?}");
            });
            //app.UseHttpsRedirection();
        }
    }
}
