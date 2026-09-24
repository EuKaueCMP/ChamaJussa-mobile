using ChamaJussaAPI.Domains;

namespace ChamaJussaAPI.Interfaces
{
    public interface IFilaRepository
    {
        List<Fila> Listar();
        Fila ObterFilaID(int id);
    }
}
