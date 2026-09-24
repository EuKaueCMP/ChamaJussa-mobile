using ChamaJussaAPI.Applications.Services;
using ChamaJussaAPI.DTOs.StatusItemDto;
using ChamaJussaAPI.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChamaJussaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly StatusService _service;
        public StatusController(StatusService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<LerStatusDto>> Listar()
        {
            try
            {
                return Ok(_service.Listar());
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("Listar")]
        public ActionResult<LerStatusDto> ObterStatusID(int id)
        {
            try
            {
                return Ok(_service.ObterStatusID(id));
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
