using System;
using System.Linq;
using System.Text;

namespace RedisLab.Domain
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }

        internal void GerarLogin()
        {
            Login = Nome.Split(' ').First() + "." + Nome.Split(' ').Last();
            Login = Login.ToLower();
        }

        internal void GerarSenha()
        {
            var rdm = new Random();
            var sb = new StringBuilder();

            for (int i = 0; i < 6; i++)
                sb.Append(Convert.ToChar(rdm.Next(101, 132)));

            Senha = sb.ToString();
        }
    }
}
