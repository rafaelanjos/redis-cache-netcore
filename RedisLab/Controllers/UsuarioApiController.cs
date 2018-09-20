using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedisLab.Domain;
using RedisLab.Services;

namespace RedisLab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioApiController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioApiController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpPost]
        public IActionResult Create(Usuario usuario)
        {
            try
            {
                usuario.GerarLogin();
                usuario.GerarSenha();
                _usuarioRepository.Add(usuario);
                return Created($"/Usuario/Details/{usuario.Login}", usuario);
            }
            catch (Exception)
            {
                return StatusCode(500, "Opss, alguma coisa deu errado!");
            }
        }

        [HttpGet]
        public ActionResult Index()
        {
            return Ok(_usuarioRepository.GetUsuarios());
        }
    }
}