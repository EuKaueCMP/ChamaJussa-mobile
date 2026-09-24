using ChamaJussaAPI.Domains;
using ChamaJussaAPI.DTOs.LocalizacaoDto;
using ChamaJussaAPI.Exceptions;
using ChamaJussaAPI.Interfaces;
using Superpower.Model;

namespace ChamaJussaAPI.Applications.Services
{
    public class LocalizacaoService
    {
        private readonly ILocalizacaoRepository _localizacao;
        public LocalizacaoService(ILocalizacaoRepository localizacao)
        {
            _localizacao = localizacao;
        }

        private static LerLocalizacaoDto LerLocDto(Localizacao localizacao)
        {
                return new LerLocalizacaoDto
                {
                    localizacaoID = localizacao.localizacaoID,
                    andar = localizacao.andar,
                    nome = localizacao.nome
                };
        }

        public List<LerLocalizacaoDto> Listar()
        {
            List<Localizacao> localizacoes = _localizacao.Listar();
            return localizacoes.Select(l => LerLocDto(l)).ToList();
        }

        public LerLocalizacaoDto ObterLocalizacaoID(int id)
        {
            Localizacao localizacao = _localizacao.ObterLocalizacaoID(id);
            if (localizacao == null)
                throw new DomainException("Nenhum usuairo localizado");
            return LerLocDto(localizacao);
        }
    }
}
