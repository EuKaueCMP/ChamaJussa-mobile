using System;
using System.Collections.Generic;
using System.Linq;
using ChamaJussaAPI.Contexts;
using ChamaJussaAPI.Domains;
using ChamaJussaAPI.Interfaces;

namespace ChamaJussaAPI.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ChamaJussaContext _context;

        public UsuarioRepository(ChamaJussaContext context)
        {
            _context = context;
        }

        public List<Usuario> Listar()
        {
            return _context.Usuario.ToList();
        }

        public Usuario? ObterPorId(Guid id)
        {
            return _context.Usuario.Find(id);
        }

        public Usuario? ObterPorEmail(string email)
        {
            return _context.Usuario.FirstOrDefault(u => u.email == email);
        }

        public Usuario ObterPorNif(string nif)
        {
            return _context.Usuario.FirstOrDefault(u => u.NIF == nif);
        }

        public bool EmailExiste(string email)
        {
            return _context.Usuario.Any(u => u.email == email);
        }

        public void Adicionar(Usuario usuario)
        {
            _context.Usuario.Add(usuario);
            _context.SaveChanges();
        }

        public void AtualizarSenha(Guid id, byte[] senha)
        {
            Usuario usuarioBuscado = _context.Usuario.Find(id);
            usuarioBuscado.senha = senha;
            _context.Usuario.Update(usuarioBuscado);
            _context.SaveChanges();
        }
    }
}
