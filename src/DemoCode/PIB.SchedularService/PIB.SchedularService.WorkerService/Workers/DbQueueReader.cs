using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PIB.SchedularService.WorkerService.Processors;

namespace PIB.SchedularService.WorkerService.Workers
{
    public class DbQueueReader : BackgroundService
    {
        private readonly ILogger<DbQueueReader> _logger;
        private readonly TransactionQueueProcesor _queueProcesor;

        public DbQueueReader(ILogger<DbQueueReader> logger, TransactionQueueProcesor queueProcesor)
        {
            _logger = logger;
            _queueProcesor = queueProcesor;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var ticks = 0;
            while (!stoppingToken.IsCancellationRequested)
            {
                if (ticks++ > 50) throw new Exception("Oups...");
                _queueProcesor.ProcessTransaction();
                await Task.Delay(1 * 1000, stoppingToken);
            }
        }
    }
}
