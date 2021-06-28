using Microsoft.Extensions.Logging;
using PIB.SchedularService.WorkerService.Model;
using PIB.SchedularService.WorkerService.Reposoitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PIB.SchedularService.WorkerService.Processors
{
    public class TransactionQueueProcesor
    {

        private readonly ILogger<TransactionQueueProcesor> _logger;

        public TransactionQueueProcesor(ILogger<TransactionQueueProcesor> logger)
        {
            _logger = logger;
        }


        public DateTime LastProcessTime { get; private set; }

        public void ProcessTransaction()
        {
            try
            {

                LastProcessTime = DateTime.UtcNow;

                _logger.LogInformation("Trying to retreive.....");

                Queue<Transaction> trasactions = PostgreSqlHelper.GetTransactions();

                _logger.LogInformation($"{trasactions.Count} Transaction Received");

                while (trasactions.Any())
                {
                    Transaction trasaction = trasactions.Dequeue();

                    _logger.LogInformation($"Processing Transaction Id: {trasaction.TransactionId}");

                    try
                    {
                        trasaction.Amount += trasaction.Amount / 10;
                        trasaction.Status = 2;
                    }
                    catch (Exception ex)
                    {
                        trasaction.Error = ex.Message;
                        _logger.LogError($"Error {ex.Message} in Transaction Id: {trasaction.TransactionId}");
                    }
                    finally
                    {
                        trasaction.ProcessedOn = DateTime.UtcNow;
                        PostgreSqlHelper.UpdateTransaction(trasaction);

                        _logger.LogError($"Transaction completed for Id: {trasaction.TransactionId}");
                    }
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Execption occured: {ex.Message}");
            }
        }
    }
}
