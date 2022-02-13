using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LongRunningTask
{
    public class LongRunningService : BackgroundWorkerQueue
    {
         private readonly BackgroundWorkerQueue queue;

    public LongRunningService(BackgroundWorkerQueue queue)
    {
        this.queue = queue;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var workItem = await queue.DequeueAsync(stoppingToken);

            await workItem(stoppingToken);
        }
    }

    }
}
