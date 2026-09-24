using ChamaJussaAPI.Contexts;
using ChamaJussaAPI.Domains;
using ChamaJussaAPI.Interfaces;
using System.Data;
using System.Runtime.CompilerServices;

namespace ChamaJussaAPI.Repositories
{
    public class FilaRepository : IFilaRepository
    {
        private readonly ChamaJussaContext _context;
        public FilaRepository(ChamaJussaContext context)
        {
            _context = context;
        }

        public List<Fila> Listar()
        {
            return _context.Fila.ToList();
        }

        public Fila ObterFilaID(int id)
        {
            return _context.Fila.FirstOrDefault(f => f.filaID == id);
        }
    }
}
