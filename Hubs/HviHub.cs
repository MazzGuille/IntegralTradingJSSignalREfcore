using IntegralTradingJSSignalREfcore.Repository.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace IntegralTradingJSSignalREfcore.Hubs
{
    public class HviHub : Hub
    {
        public readonly IHviService _service;

        public HviHub(IHviService service)
        {
            _service = service;
        }

        public async Task ExecuteProcedure()
        {
            var result = await _service.GetHvi();

            await Clients.All.SendAsync("ReceiveStoredProcedureResult", result);
        }
    }
}
