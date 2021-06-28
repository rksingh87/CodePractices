using PIB.SchedularService.WorkerService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PIB.SchedularService.WorkerService.Reposoitory
{
    public class PostgreSqlHelper
    {
        public static Queue<Transaction> GetTransactions()
        {
            return new Queue<Transaction>();
        }

        public static void UpdateTransaction(Transaction transaction)
        {

        }
    }
}
