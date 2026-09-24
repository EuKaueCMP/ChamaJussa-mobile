using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ChamaJussaAPI.Contexts;
using ChamaJussaAPI.Domains;
using ChamaJussaAPI.Interfaces;

namespace ChamaJussaAPI.Repositories
{
    public class OrdemServicoRepository : IOrdemServicoRepository
    {
        private readonly ChamaJussaContext _context;

        public OrdemServicoRepository(ChamaJussaContext context)
        {
            _context = context;
        }

        public void Adicionar(OrdemServico os)
        {
            _context.OrdemServico.Add(os);
            _context.SaveChanges();
        }

        public void Atualizar(OrdemServico os)
        {
            _context.OrdemServico.Update(os);
            _context.SaveChanges();
        }

        public void Deletar(OrdemServico os)
        {
            _context.OrdemServico.Remove(os);
            _context.SaveChanges();
        }

        public List<OrdemServico> ListarPorUsuario(Guid usuarioId)
        {
            return _context.OrdemServico
                .Include(os => os.localizacao)
                .Include(os => os.usuarioSolicitanteNavigation)
                .Include(os => os.status)
                .Include(os => os.fila)
                .Where(os => os.usuarioSolicitante == usuarioId)
                .ToList();
        }

        public OrdemServico? ObterPorId(int id)
        {
            return _context.OrdemServico
                .Include(os => os.localizacao)
                .Include(os => os.usuarioSolicitanteNavigation)
                .Include(os => os.status)
                .Include(os => os.fila)
                .FirstOrDefault(os => os.ordemServicoID == id);
        }

        public bool LocalizacaoExiste(int localizacaoId)
        {
            return _context.Localizacao.Any(l => l.localizacaoID == localizacaoId);
        }

        public bool StatusExiste(int statusId)
        {
            return _context.StatusItem.Any(s => s.statusID == statusId);
        }

        public int ObterStatusInicialId()
        {
            var statusAberto = _context.StatusItem
                .FirstOrDefault(s => s.nomeStatus.ToLower() == "aberto" || s.nomeStatus.ToLower() == "aberta");

            if (statusAberto != null)
            {
                return statusAberto.statusID;
            }

            var primeiroStatus = _context.StatusItem.OrderBy(s => s.statusID).FirstOrDefault();
            if (primeiroStatus != null)
            {
                return primeiroStatus.statusID;
            }

            var novoStatus = new StatusItem { nomeStatus = "Aberto" };
            _context.StatusItem.Add(novoStatus);
            _context.SaveChanges();
            return novoStatus.statusID;
        }

        public int ObterFilaInicialId()
        {
            var primeiraFila = _context.Fila.OrderBy(f => f.filaID).FirstOrDefault();
            if (primeiraFila != null)
            {
                return primeiraFila.filaID;
            }

            var novaFila = new Fila { nomeFila = "Geral" };
            _context.Fila.Add(novaFila);
            _context.SaveChanges();
            return novaFila.filaID;
        }
    }
}
