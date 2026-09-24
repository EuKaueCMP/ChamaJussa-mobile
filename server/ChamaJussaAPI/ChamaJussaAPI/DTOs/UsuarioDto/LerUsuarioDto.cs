using System;

namespace ChamaJussaAPI.DTOs.UsuarioDto
{
    public class LerUsuarioDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty!;
        public string NIF { get; set; } = string.Empty!;
        public string Email { get; set; } = string.Empty!;
    }
}
