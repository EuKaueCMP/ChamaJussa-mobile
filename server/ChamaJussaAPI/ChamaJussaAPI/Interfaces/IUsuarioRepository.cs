using System;
using System.Collections.Generic;
using ChamaJussaAPI.Domains;

namespace ChamaJussaAPI.Interfaces
{
    public interface IUsuarioRepository
    {
        List<Usuario> Listar();
        Usuario? ObterPorId(Guid id);
        Usuario? ObterPorEmail(string email);
        Usuario ObterPorNif(string nif);
        bool EmailExiste(string email);
        void Adicionar(Usuario usuario);
        void AtualizarSenha(Guid id, byte[] senha);
    }
}
