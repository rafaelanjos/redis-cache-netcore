using RedisLab.Domain;
using System.Collections.Generic;

namespace RedisLab.Services
{
    public interface IUsuarioRepository
    {
        Usuario GetUsuarioByLogin(string login);
        void Add(Usuario usuario);
        List<Usuario> GetUsuarios();
    }
}
