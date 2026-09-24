using ChamaJussaAPI.Domains;

namespace ChamaJussaAPI.Interfaces
{
    public interface ILocalizacaoRepository
    {
        List<Localizacao> Listar();
        Localizacao? ObterLocalizacaoID(int id);
    }
}
