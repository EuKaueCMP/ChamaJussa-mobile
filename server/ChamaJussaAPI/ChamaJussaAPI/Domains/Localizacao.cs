using System;
using System.Collections.Generic;

namespace ChamaJussaAPI.Domains;

public partial class Localizacao
{
    public int localizacaoID { get; set; }

    public string nome { get; set; } = null!;

    public string? andar { get; set; }

    public virtual ICollection<OrdemServico> OrdemServico { get; set; } = new List<OrdemServico>();
}
