using ChamaJussaAPI.Applications.Services;
using ChamaJussaAPI.DTOs.FilaDto;
using ChamaJussaAPI.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChamaJussaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilaItemController : ControllerBase
    {
        private readonly FilaService _service;
        public FilaItemController(FilaService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<LerFilaDto>> Listar()
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
        public ActionResult<LerFilaDto> ObterFilaID(int id)
        {
            try
            {
                return Ok(_service.ObterFilaID(id));
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
