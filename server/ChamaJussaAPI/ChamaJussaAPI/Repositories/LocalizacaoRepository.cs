using ChamaJussaAPI.Contexts;
using ChamaJussaAPI.Domains;
using ChamaJussaAPI.Interfaces;

namespace ChamaJussaAPI.Repositories
{
    public class LocalizacaoRepository : ILocalizacaoRepository
    {
        private readonly ChamaJussaContext _context;
        public LocalizacaoRepository(ChamaJussaContext context)
        {
            _context = context;
        }

        public List<Localizacao> Listar()
        {
            return _context.Localizacao.ToList();
        }

        public Localizacao ObterLocalizacaoID(int id)
        {
            return _context.Localizacao.FirstOrDefault(l => l.localizacaoID == id);
        }
    }
}