using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PIB.SchedularService.WorkerService.Model
{
    public class Transaction
    {
        public int TransactionId { get; set; }

        public int Amount { get; set; }

        public DateTime CreatedOn { get; set; }

        public int Status { get; set; }

        public DateTime? ProcessedOn { get; set; }

        public string Error { get; set; }
    }
}
