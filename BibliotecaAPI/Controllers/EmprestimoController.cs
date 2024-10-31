using BibliotecaAPI.Models;
using BibliotecaAPI.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace BibliotecaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmprestimoController : ControllerBase
    {
        private readonly EmprestimoRepository _emprestimoRepository;

        public EmprestimoController(EmprestimoRepository emprestimoRepository)
        {
            _emprestimoRepository = emprestimoRepository;
        }

        // para cadastrar um novo empréstimo
        [HttpPost("cadastrar-emprestimo")]
        public async Task<IActionResult> CadastrarEmprestimo([FromBody] Emprestimo emprestimo)
        {
            // verificar disponibilidade do livro
            var disponivel = await _emprestimoRepository.ValidaDisponibilidadeLivroDB(emprestimo.LivroId);
            if (disponivel == 0) 
            {
                return BadRequest (new { mensagem = "O livro não esta disponível para emprestimo" });
            }

            // cadastrar o empréstimo
            emprestimo.DataDevolucao = emprestimo.DataEmprestimo.AddDays(14);
            var emprestimoId = await _emprestimoRepository.CadastrarEmprestimoDB(emprestimo);
            return Ok(new { mensagem = "Emprestimo cadastrado com sucesso", emprestimoId });


        }

        //historico
        
        [HttpGet("historico-emprestimos")]
        public async Task<IActionResult> ListarHistoricoEmprestimosDB()
        {
            var emprestimos = await _emprestimoRepository.ListarHistoricoEmprestimosDB();
            return Ok(emprestimos); 
        }

    }
}
