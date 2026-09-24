using Microsoft.AspNetCore.Http;
using System;

namespace ChamaJussaAPI.DTOs.OrdemServicoDto
{
    public class CriarOrdemServicoDto
    {
        public string NomeItem { get; set; } = null!;
        public int LocalizacaoId { get; set; }
        public string Descricao { get; set; } = null!;
        public IFormFile? Imagem { get; set; }
    }
}
