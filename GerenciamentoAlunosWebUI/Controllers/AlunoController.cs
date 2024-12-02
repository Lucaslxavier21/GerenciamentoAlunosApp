using GestaoDeAlunosApi.Models;
using GestaoDeAlunosApi.Service;
using Microsoft.AspNetCore.Mvc;

namespace GestaoDeAlunosApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : Controller
    {
        private readonly AlunoService _alunoService;

        public AlunoController(AlunoService alunoService)
        {
            _alunoService = alunoService;
        }

        [HttpPost]
        [Route("AdicionarAluno")]
        public async Task<IActionResult> AdicionarAluno(AlunoRequestModel aluno)
        {
            try
            {
                var result = await _alunoService.AdicionarAluno(aluno);
                return Ok("Aluno adicionado com sucesso!");
            }
            catch(Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("ListarAlunos")]
        public async Task<IActionResult> ListarAlunos()
        {
            var alunos = await _alunoService.ListarAlunos();
            return Ok(alunos);
        }

        [HttpGet]
        [Route("BuscarAlunoPorId/{id}")]
        public async Task<IActionResult> BuscarAlunoPorId(int id)
        {
            var aluno = await _alunoService.BuscarAlunoPorId(id);
            return Ok(aluno);
        }

        [HttpPut]
        [Route("AtualizarAluno/{id}")]
        public async Task<IActionResult> AtualizarAluno(int id, AlunoModel aluno)
        {
            await _alunoService.AtualizarAluno(id, aluno);
            return Ok("Dados do aluno atualizado com sucesso!");
        }

        [HttpPut]
        [Route("AlterarStatus/{id}/{status}")]
        public async Task<IActionResult> AlterarStatus(int id, bool status)
        {
            await _alunoService.AlterarStatus(id, status);
            return Ok("Aluno desativado do sistema.");
        }
    }
}
