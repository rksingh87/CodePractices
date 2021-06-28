using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PIB.SchedularService.WorkerService.Processors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PIB.SchedularService.WorkerService.Workers
{
    public class DbQueueReaderHealthCheck : IHealthCheck
    {

        private readonly TransactionQueueProcesor _queueProcesor;

        public DbQueueReaderHealthCheck(TransactionQueueProcesor queueProcesor)
        {
            _queueProcesor = queueProcesor;
        }


        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var lastProcess = _queueProcesor.LastProcessTime;

            var timeAgo = DateTime.UtcNow.Subtract(lastProcess);

            var data = new Dictionary<string, object> { { "Last process", lastProcess }, { "Time ago", timeAgo }
                                                        } as IReadOnlyDictionary<string, object>;

            if (lastProcess > DateTime.UtcNow.AddSeconds(-30))
            {
                return Task.FromResult(HealthCheckResult.Healthy("Processing as much as we can", data));
            }

            return Task.FromResult(HealthCheckResult.Unhealthy("Processing is stuck somewhere", null, data));
        }

        public static Task DbQueueReaderHealthResponse(HttpContext httpContext, HealthReport result)
        {
            httpContext.Response.ContentType = "application/json";
            var json = new JObject(
              new JProperty("status", result.Status.ToString()),
              new JProperty("results", new JObject(result.Entries.Select(pair =>
                new JProperty(pair.Key, new JObject(
                  new JProperty("status", pair.Value.Status.ToString()),
                  new JProperty("description", pair.Value.Description),
                  new JProperty("data", new JObject(pair.Value.Data.Select(
                    p => new JProperty(p.Key, p.Value)
                  )))
                ))
              )))
            );
            return httpContext.Response.WriteAsync(json.ToString(Formatting.Indented));
        }
    }
}
