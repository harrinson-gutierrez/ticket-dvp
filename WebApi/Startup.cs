using Application;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Migrations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApi.Extensions;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddApplicationLayer();
            services.AddPersistenceInfrastructure(Configuration);
            services.AddFluentMigratorConsole(Configuration);
            services.AddControllers();
            services.AddSwaggerExtension();
            services.AddApiVersioningExtension();
            services.AddHealthChecks();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseSwaggerExtension();
            app.UseErrorHandlingMiddleware();
            app.UseHealthChecks("/");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.MigrateFM();
        }
    }
}
