using Amazon.S3.Model;
using ChamaJussaAPI.Applications.Services;
using ChamaJussaAPI.DTOs.LocalizacaoDto;
using ChamaJussaAPI.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChamaJussaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocalizacaoController : ControllerBase
    {
        private readonly LocalizacaoService _service;
        public LocalizacaoController(LocalizacaoService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<LerLocalizacaoDto>> Listar()
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
        public ActionResult<LerLocalizacaoDto> ObterLocalizacaoID(int id)
        {
            try
            {
                return Ok(_service.ObterLocalizacaoID(id));
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
