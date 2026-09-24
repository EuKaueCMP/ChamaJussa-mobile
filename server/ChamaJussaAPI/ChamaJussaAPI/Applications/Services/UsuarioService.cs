using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using ChamaJussaAPI.Domains;
using ChamaJussaAPI.DTOs.UsuarioDto;
using ChamaJussaAPI.Exceptions;
using ChamaJussaAPI.Interfaces;

namespace ChamaJussaAPI.Applications.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        private static LerUsuarioDto LerDto(Usuario usuario)
        {
            return new LerUsuarioDto
            {
                Id = usuario.usuarioID,
                Nome = usuario.nome,
                NIF = usuario.NIF,
                Email = usuario.email
            };
        }

        public List<LerUsuarioDto> Listar()
        {
            List<Usuario> usuarios = _repository.Listar();
            return usuarios.Select(u => LerDto(u)).ToList();
        }

        public LerUsuarioDto ObterPorId(Guid id)
        {
            Usuario? usuario = _repository.ObterPorId(id);
            if (usuario == null)
            {
                throw new DomainException("Usuário não existe.");
            }
            return LerDto(usuario);
        }

        public LerUsuarioDto ObterPorNif(string nif)
        {
            Usuario usuario = _repository.ObterPorNif(nif);
            if (usuario == null)
                throw new DomainException("Usuario nao localizado");

            return LerDto(usuario);
        }

        private static void ValidarEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            {
                throw new DomainException("Email inválido.");
            }
        }

        private static byte[] HashSenha(string senha)
        {
            if (string.IsNullOrWhiteSpace(senha))
            {
                throw new DomainException("Senha é obrigatória.");
            }

            using var sha256 = SHA256.Create();
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(senha));
        }

        public LerUsuarioDto Adicionar(CriarUsuarioDto usuarioDto)
        {
            ValidarEmail(usuarioDto.Email);

            if (_repository.EmailExiste(usuarioDto.Email))
            {
                throw new DomainException("Já existe um usuário com este e-mail.");
            }

            Usuario usuario = new Usuario
            {
                nome = usuarioDto.Nome,
                NIF = usuarioDto.NIF,
                email = usuarioDto.Email,
                senha = HashSenha(usuarioDto.Senha)
            };

            _repository.Adicionar(usuario);

            return LerDto(usuario);
        }

        public void AtualizarSenha(Guid id, string senha)
        {
            if (string.IsNullOrEmpty(senha) || _repository.ObterPorId(id) == null)
                throw new DomainException("Dados invalidos!");

            var novaSenha = HashSenha(senha);
            _repository.AtualizarSenha(id, novaSenha);
        }
    }
}
