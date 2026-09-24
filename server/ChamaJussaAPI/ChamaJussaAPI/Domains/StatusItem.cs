using System;
using System.Collections.Generic;

namespace ChamaJussaAPI.Domains;

public partial class StatusItem
{
    public int statusID { get; set; }

    public string? nomeStatus { get; set; }

    public virtual ICollection<OrdemServico> OrdemServico { get; set; } = new List<OrdemServico>();
}
