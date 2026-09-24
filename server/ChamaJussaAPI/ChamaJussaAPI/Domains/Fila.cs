using System;
using System.Collections.Generic;

namespace ChamaJussaAPI.Domains;

public partial class Fila
{
    public int filaID { get; set; }

    public string? nomeFila { get; set; }

    public virtual ICollection<OrdemServico> OrdemServico { get; set; } = new List<OrdemServico>();
}
