using ChamaJussaAPI.Domains;
using ChamaJussaAPI.DTOs.FilaDto;
using ChamaJussaAPI.Interfaces;
using ChamaJussaAPI.Repositories;
using System.Data;
using System.Runtime.CompilerServices;

namespace ChamaJussaAPI.Applications.Services
{
    public class FilaService
    {
        private readonly IFilaRepository _fila;
        public FilaService(IFilaRepository fila)
        {
            _fila = fila;
        }

        private static LerFilaDto LerFilaDto(Fila fila)
        {
            return new LerFilaDto
            {
                filaID = fila.filaID,
                nomeFila = fila.nomeFila
            };
        }

        public List<LerFilaDto> Listar()
        {
            List<Fila> filas = _fila.Listar();
            return filas.Select(f => LerFilaDto(f)).ToList();
        }

        public LerFilaDto ObterFilaID(int id)
        {
            Fila fila = _fila.ObterFilaID(id);
            return LerFilaDto(fila);
        }
    }
}
