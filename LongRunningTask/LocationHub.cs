using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LongRunningTask
{
    public interface ILocationHubService
    {
        Task GetLocation(string status);
    }

    public class LocationHub : Hub<ILocationHubService>
    {

    }
}
