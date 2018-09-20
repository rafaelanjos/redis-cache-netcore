using System.Collections.Generic;
using System.Linq;
using RedisLab.Domain;

namespace RedisLab.Services
{
    public class UsuarioSqlRepository : IUsuarioRepository
    {
        LabContext _ctx;

        public UsuarioSqlRepository(LabContext context)
        {
            _ctx = context;
        }

        public void Add(Usuario usuario)
        {
            _ctx.Usuarios.Add(usuario);
            _ctx.SaveChanges();
        }

        public Usuario GetUsuarioByLogin(string login)
        {
            return _ctx.Usuarios.Where(u => u.Login == login).FirstOrDefault();
        }

        public List<Usuario> GetUsuarios()
        {
            return _ctx.Usuarios.ToList();
        }
    }
}
