using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PIB.SchedularService.WorkerService.Model
{
    public enum TransactionStatus
    {
        Waiting = 1,
        InProgress = 2,
        Completed = 3,
        Error = 4
    }
}
