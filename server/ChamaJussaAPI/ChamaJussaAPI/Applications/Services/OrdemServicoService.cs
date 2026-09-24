using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ChamaJussaAPI.Domains;
using ChamaJussaAPI.DTOs.OrdemServicoDto;
using ChamaJussaAPI.Exceptions;
using ChamaJussaAPI.Interfaces;

namespace ChamaJussaAPI.Applications.Services
{
    public class OrdemServicoService
    {
        private readonly IOrdemServicoRepository _repository;
        private readonly IStorageService _storageService;

        public OrdemServicoService(IOrdemServicoRepository repository, IStorageService storageService)
        {
            _repository = repository;
            _storageService = storageService;
        }

        private static LerOrdemServicoDto ConverterParaDto(OrdemServico os)
        {
            return new LerOrdemServicoDto
            {
                OsId = os.ordemServicoID,
                NomeItem = os.nomeItem,
                Solicitante = os.usuarioSolicitante,
                SolicitanteNome = os.usuarioSolicitanteNavigation?.nome,
                DtCriacao = os.dataCriacao,
                LocalizacaoId = os.localizacaoID,
                LocalizacaoNome = os.localizacao != null ? $"{os.localizacao.nome} (Andar: {os.localizacao.andar})" : null,
                Descricao = os.descricao,
                Imagem = os.imagem,
                StatusId = os.statusID,
                StatusNome = os.status?.nomeStatus,
                FilaId = os.filaID,
                FilaNome = os.fila?.nomeFila
            };
        }

        public async Task<LerOrdemServicoDto> AdicionarAsync(CriarOrdemServicoDto osDto, Guid usuarioId)
        {
            if (string.IsNullOrWhiteSpace(osDto.NomeItem))
            {
                throw new DomainException("Nome do item é obrigatório.");
            }

            if (string.IsNullOrWhiteSpace(osDto.Descricao))
            {
                throw new DomainException("Descrição é obrigatória.");
            }

            if (osDto.LocalizacaoId > 0 && !_repository.LocalizacaoExiste(osDto.LocalizacaoId))
            {
                throw new DomainException("A localização informada não existe.");
            }

            // Realiza upload no Supabase Storage se a imagem for fornecida
            string? urlImagem = null;
            if (osDto.Imagem != null && osDto.Imagem.Length > 0)
            {
                urlImagem = await _storageService.UploadImagemAsync(osDto.Imagem);
            }

            int statusIdInicial = _repository.ObterStatusInicialId();
            int filaIdInicial = _repository.ObterFilaInicialId();

            OrdemServico os = new OrdemServico
            {
                nomeItem = osDto.NomeItem,
                usuarioSolicitante = usuarioId,
                dataCriacao = DateTime.Now,
                localizacaoID = osDto.LocalizacaoId,
                descricao = osDto.Descricao,
                imagem = urlImagem,
                statusID = statusIdInicial,
                filaID = filaIdInicial
            };

            _repository.Adicionar(os);

            // Recarrega a OS do banco de dados para popular as entidades navegacionais
            var osBanco = _repository.ObterPorId(os.ordemServicoID);
            return osBanco != null ? ConverterParaDto(osBanco) : ConverterParaDto(os);
        }

        public List<LerOrdemServicoDto> ListarPorUsuario(Guid usuarioId)
        {
            List<OrdemServico> ordens = _repository.ListarPorUsuario(usuarioId);
            return ordens.Select(os => ConverterParaDto(os)).ToList();
        }

        public LerOrdemServicoDto ObterPorId(int id)
        {
            OrdemServico? os = _repository.ObterPorId(id);
            if (os == null)
            {
                throw new DomainException("Ordem de serviço não encontrada.");
            }
            return ConverterParaDto(os);
        }

        public string ObterImagem(int id)
        {
            OrdemServico? os = _repository.ObterPorId(id);
            if (os == null)
            {
                throw new DomainException("Ordem de serviço não encontrada.");
            }
            return os.imagem;
        }

        private static bool IsStatusAberto(OrdemServico os)
        {
            if (os.status != null && !string.IsNullOrWhiteSpace(os.status.nomeStatus))
            {
                return string.Equals(os.status.nomeStatus, "Aberto", StringComparison.OrdinalIgnoreCase) ||
                       string.Equals(os.status.nomeStatus, "Aberta", StringComparison.OrdinalIgnoreCase);
            }
            return os.statusID == 1;
        }

        public async Task<LerOrdemServicoDto> EditarAsync(int id, EditarOrdemServicoDto dto)
        {
            OrdemServico? os = _repository.ObterPorId(id);
            if (os == null)
            {
                throw new DomainException("Ordem de serviço não encontrada.");
            }

            if (!IsStatusAberto(os))
            {
                throw new DomainException("A Ordem de Serviço não pode ser editada pois seu status já foi modificado.");
            }

            if (!string.IsNullOrWhiteSpace(dto.NomeItem))
            {
                os.nomeItem = dto.NomeItem;
            }

            if (!string.IsNullOrWhiteSpace(dto.Descricao))
            {
                os.descricao = dto.Descricao;
            }

            if (dto.LocalizacaoId.HasValue)
            {
                if (!_repository.LocalizacaoExiste(dto.LocalizacaoId.Value))
                {
                    throw new DomainException("A localização informada não existe.");
                }
                os.localizacaoID = dto.LocalizacaoId.Value;
            }

            if (dto.Imagem != null && dto.Imagem.Length > 0)
            {
                os.imagem = await _storageService.UploadImagemAsync(dto.Imagem);
            }

            _repository.Atualizar(os);

            var osAtualizada = _repository.ObterPorId(os.ordemServicoID);
            return osAtualizada != null ? ConverterParaDto(osAtualizada) : ConverterParaDto(os);
        }

        public void Deletar(int id)
        {
            OrdemServico? os = _repository.ObterPorId(id);
            if (os == null)
            {
                throw new DomainException("Ordem de serviço não encontrada.");
            }

            if (!IsStatusAberto(os))
            {
                throw new DomainException("A Ordem de Serviço não pode ser excluída pois seu status já foi modificado.");
            }

            _repository.Deletar(os);
        }

        public LerOrdemServicoDto AtualizarStatus(int id, int statusId)
        {
            OrdemServico? os = _repository.ObterPorId(id);
            if (os == null)
            {
                throw new DomainException("Ordem de serviço não encontrada.");
            }

            if (!_repository.StatusExiste(statusId))
            {
                throw new DomainException("O status informado não existe.");
            }

            os.statusID = statusId;
            _repository.Atualizar(os);

            var osAtualizada = _repository.ObterPorId(os.ordemServicoID);
            return osAtualizada != null ? ConverterParaDto(osAtualizada) : ConverterParaDto(os);
        }
    }
}
