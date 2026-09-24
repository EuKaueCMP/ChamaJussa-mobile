using ChamaJussaAPI.Domains;
using ChamaJussaAPI.DTOs.StatusItemDto;
using ChamaJussaAPI.Interfaces;

namespace ChamaJussaAPI.Applications.Services
{
    public class StatusService
    {
        private readonly IStatusRepository _status;
        public StatusService(IStatusRepository status)
        {
            _status = status;
        }

        private static LerStatusDto LerStatusDto(StatusItem Status)
        {
            return new LerStatusDto
            {
                statusID = Status.statusID,
                nomeStatus = Status.nomeStatus
            };
        }

        public List<LerStatusDto> Listar()
        {
            List<StatusItem> Statuss = _status.Listar();
            return Statuss.Select(f => LerStatusDto(f)).ToList();
        }

        public LerStatusDto ObterStatusID(int id)
        {
            StatusItem status = _status.ObterStatusID(id);
            return LerStatusDto(status);
        }
    }
}
