using ChamaJussaAPI.Domains;

namespace ChamaJussaAPI.Interfaces
{
    public interface IStatusRepository
    {
        List<StatusItem> Listar();
        StatusItem ObterStatusID(int id);
    }
}
