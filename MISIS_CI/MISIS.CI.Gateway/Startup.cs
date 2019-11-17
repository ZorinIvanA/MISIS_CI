using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MISIS.CI.Gateway.BusinessLogic;
using MISIS.CI.Gateway.BusinessLogic.Interfaces;
using MISIS.CI.Gateway.Infrastructure.Repositories;
using MISIS.CI.Gateway.Infrastructure.Settings;

namespace MISIS.CI.Gateway
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
            services.AddScoped<IInteractionLogic, InteractionLogic>();
            services.AddScoped<IRemoteApiServiceRepository, RemoteApiRepository>();
            services.AddScoped<IStorageServiceRepository, StorageServiceRepository>();
            services.Configure<GatewaySettings>(Configuration.GetSection("GatewaySettings"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=storage}/{action=Index}/{id?}");
            });
            app.UseHttpsRedirection();
        }
    }
}
