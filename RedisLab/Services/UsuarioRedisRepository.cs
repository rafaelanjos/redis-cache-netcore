using Microsoft.Extensions.Caching.Distributed;
using RedisLab.Commons;
using RedisLab.Domain;
using System;
using System.Collections.Generic;

namespace RedisLab.Services
{
    public class UsuarioRedisRepository : IUsuarioRepository
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IDistributedCache _cache;

        public UsuarioRedisRepository(IDistributedCache distributedCache, IUsuarioRepository usuarioRepository)
        {
            _cache = distributedCache;
            _usuarioRepository = usuarioRepository;
        }

        public void Add(Usuario usuario)
        {
            _usuarioRepository.Add(usuario);
        }

        public Usuario GetUsuarioByLogin(string login)
        {
            var key = GetKeyUsuarioByLogin(login);
            var usuario = _cache.GetObject<Usuario>(key);
            if (usuario == null)
            {
                usuario = _usuarioRepository.GetUsuarioByLogin(login);
                _cache.SetObject(key, usuario, TimeSpan.FromDays(1));
            }

            return usuario;
        }

        public List<Usuario> GetUsuarios()
        {
            const string key = "usuarios";
            var usuarios = _cache.GetObject<List<Usuario>>(key);
            if (usuarios == null)
            {
                //Value do redis suporta 500mb
                usuarios = _usuarioRepository.GetUsuarios();
                _cache.SetObject(key, usuarios);
            }
            return usuarios;
        }

        private string GetKeyUsuarioByLogin(string login)
        {
            return $"usuario:login:{login}";
        }

        private string GetKeyUsuarioById(int id)
        {
            return $"usuario:{id}";
        }
    }
}
