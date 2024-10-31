using BibliotecaAPI.Models;
using BibliotecaAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BibliotecaAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioRepository _usuarioRepository;

        public UsuarioController(UsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        // cadastrar um novo usuario
        [HttpPost("cadastrar-usuario")]
        public async Task<IActionResult> CadastrarUsuario([FromBody] Usuario usuario)
        {
            var usuarioId = await _usuarioRepository.CadastrarUsuarioDB(usuario);
            return Ok(new { mensagem = "Usuario cadastrado com sucesso", usuarioId });
        }

        // busca por nome
        [HttpGet("busca-por-nome")]
        [SwaggerOperation]

        public async Task<IActionResult> BuscarPorId(string nome)
        {
            var usuario = await _usuarioRepository.BuscarPorNome(nome);
            if (usuario == null)
            {
                return NotFound("Não existe usuario cadastrado com o nome informado");
            }
            return Ok(usuario);
        }

        // busca por email
        [HttpGet("busca-por-email")]
        [SwaggerOperation]

        public async Task<IActionResult> BuscarPorEmail(string email)
        {
            var usuario = await _usuarioRepository.BuscarPorEmail(email);
            if (usuario == null)
            {
                return NotFound("Não existe usuario cadastrado com o email informado");
            }
            return Ok(usuario);
        }



    }
}
