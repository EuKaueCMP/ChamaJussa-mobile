using ChamaJussaAPI.Contexts;
using ChamaJussaAPI.Domains;
using ChamaJussaAPI.Interfaces;

namespace ChamaJussaAPI.Repositories
{
    public class StatusRepository : IStatusRepository
    {
        private readonly ChamaJussaContext _context;
        public StatusRepository(ChamaJussaContext context)
        {
            _context = context;
        }
        
        public List<StatusItem> Listar()
        {
            return _context.StatusItem.ToList();
        }

        public StatusItem ObterStatusID(int id)
        {
            return _context.StatusItem.FirstOrDefault(s => s.statusID == id);
        }
    }
}
