using System;
using System.Collections.Generic;
using ChamaJussaAPI.Domains;

namespace ChamaJussaAPI.Interfaces
{
    public interface IOrdemServicoRepository
    {
        void Adicionar(OrdemServico os);
        void Atualizar(OrdemServico os);
        void Deletar(OrdemServico os);
        List<OrdemServico> ListarPorUsuario(Guid usuarioId);
        OrdemServico? ObterPorId(int id);
        bool LocalizacaoExiste(int localizacaoId);
        bool StatusExiste(int statusId);
        int ObterStatusInicialId();
        int ObterFilaInicialId();
    }
}
