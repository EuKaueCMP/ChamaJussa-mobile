using System;
using System.Collections.Generic;

namespace ChamaJussaAPI.Domains;

public partial class OrdemServico
{
    public int ordemServicoID { get; set; }

    public string nomeItem { get; set; } = null!;

    public DateTime dataCriacao { get; set; }

    public string descricao { get; set; } = null!;

    public string? imagem { get; set; }

    public int statusID { get; set; }

    public Guid usuarioSolicitante { get; set; }

    public int filaID { get; set; }

    public int localizacaoID { get; set; }

    public virtual Fila fila { get; set; } = null!;

    public virtual Localizacao localizacao { get; set; } = null!;

    public virtual StatusItem status { get; set; } = null!;

    public virtual Usuario usuarioSolicitanteNavigation { get; set; } = null!;
}
