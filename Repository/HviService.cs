using IntegralTradingJSSignalREfcore.Models;
using IntegralTradingJSSignalREfcore.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IntegralTradingJSSignalREfcore.Repository
{
    public class HviService : IHviService
    {
        private readonly DevExtremeContext _context;
        private readonly List<Hvicsv> hvilist = new();

        public HviService(DevExtremeContext context)
        {
            _context = context;
        }


        public async Task<List<Hvicsv>> GetHvi()
        {
            var list = await _context.Hvicsvs.ToListAsync();

            return list;
        }
    }
}
