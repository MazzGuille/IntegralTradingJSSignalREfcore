using IntegralTradingJSSignalREfcore.Models;

namespace IntegralTradingJSSignalREfcore.Repository.Interfaces
{
    public interface IHviService
    {
        Task<List<Hvicsv>> GetHvi();
    }
}
