using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using MISIS.CI.API.BusinessLogic;
using MISIS.CI.API.BusinessLogic.Interfaces;
using MISIS.CI.API.Infrastructure.Settings;
using MISIS.CI.Storage.BusinessLogic.Interfaces;
using MISIS.CI.Storage.Infrastructure;

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
