using BibliotecaAPI.Models;
using BibliotecaAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BibliotecaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivrosController : ControllerBase
    {
        private readonly LivroRepository _livroRepository;

        public LivrosController(LivroRepository livroRepository)
        {
            _livroRepository = livroRepository;
        }

        //listar todos os livros
        [HttpGet("listar-livros")]
        public async Task<IActionResult> ListarLivros()
        {
            var livros = await _livroRepository.ListarLivrosDB();
            return Ok(livros);
        }

        // busca por genero
        [HttpGet("busca-por-genero")]
        [SwaggerOperation]

        public async Task<IActionResult> BuscarPorGenero(string genero)
        {
            var livro = await _livroRepository.BuscarPorGenero(genero);
            if (livro == null)
            {
                return NotFound("Não existe livro com o genero informado");
            }
            return Ok(livro);
        }

        // busca por autor
        [HttpGet("busca-por-autor")]
        [SwaggerOperation]

        public async Task<IActionResult> BuscarPorAutor(string autor)
        {
            var livro = await _livroRepository.BuscarPorAutor(autor);
            if (livro == null)
            {
                return NotFound("Não existe livro com o autor informado");
            }
            return Ok(livro);
        }

        // busca por ano de publicacao
        [HttpGet("busca-ano")]
        [SwaggerOperation]

        public async Task<IActionResult> BuscarPorAno(int ano)
        {
            var livro = await _livroRepository.BuscarPorAno(ano);
            if (livro == null)
            {
                return NotFound("Não existe livro com o ano informado");
            }
            return Ok(livro);
        }


        // cadastrar um novo livro
        [HttpPost("cadastrar-livro")]
        public async Task<IActionResult> CadastrarLivro([FromBody] Livro livro)
        {
            var livroId = await _livroRepository.CadastrarLivroDB(livro);
            return Ok(new { mensagem = "Livro cadastrado com sucesso", livroId });
        }

        // atualizar informações de um livro
        [HttpPut("atualizar-livro/{id}")]
        public async Task<IActionResult> AtualizarLivro(int id, [FromBody] Livro livroAtualizado)
        {
            var livro = await _livroRepository.AtualizarLivroDB(id, livroAtualizado);
            if (livro == null)
            {
                return NotFound(new { mensagem = "Livro não encontrado" });
            }
            return Ok(new { mensagem = "Livro atualizado com sucesso" });
        }

        //excluir um livro
        [HttpDelete("excluir-livro/{id}")]
        public async Task<IActionResult> ExcluirLivro(int id)
        {
            var livroExcluido = await _livroRepository.ExcluirLivroDB(id);
            if (!livroExcluido)
            {
                return NotFound(new { mensagem = "Livro não encontrado" });
            }
            return Ok(new { mensagem = "Livro excluído com sucesso" });
        }

    }
}
