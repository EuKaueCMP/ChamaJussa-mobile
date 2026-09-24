using System;
using System.Collections.Generic;

namespace ChamaJussaAPI.Domains;

public partial class Usuario
{
    public Guid usuarioID { get; set; }

    public string nome { get; set; } = null!;

    public string email { get; set; } = null!;

    public byte[]? senha { get; set; }

    public string? NIF { get; set; }

    public virtual ICollection<OrdemServico> OrdemServico { get; set; } = new List<OrdemServico>();
}
