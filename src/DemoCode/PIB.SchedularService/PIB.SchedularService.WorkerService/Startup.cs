using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PIB.SchedularService.WorkerService.Processors;
using PIB.SchedularService.WorkerService.Workers;
using Serilog;


namespace PIB.SchedularService.WorkerService
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
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(Configuration).CreateLogger();
            services.AddSingleton<TransactionQueueProcesor>();
            services.AddHealthChecks().AddCheck<DbQueueReaderHealthCheck>("db_queue_reader");
            services.AddHostedService<DbQueueReader>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("PIB.SchedularService.WorkerService is Running");
                });

                endpoints.MapHealthChecks("/health", new HealthCheckOptions()
                {
                    AllowCachingResponses = false,
                    ResponseWriter = DbQueueReaderHealthCheck.DbQueueReaderHealthResponse
                });
            });
        }
    }
}
