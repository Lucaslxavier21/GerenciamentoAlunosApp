using GestaoDeAlunosApi.Models;
using GestaoDeAlunosApi.Service;
using Microsoft.AspNetCore.Mvc;

namespace GestaoDeAlunosApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoTurmaController : ControllerBase
    {
        private readonly AlunoTurmaService _alunoTurmaService;

        public AlunoTurmaController(AlunoTurmaService alunoTurmaService)
        {
            _alunoTurmaService = alunoTurmaService;
        }

        [HttpPost]
        [Route("CriarRelacionamento/{alunoId}/{turmaId}")]
        public async Task<IActionResult> CriarRelacionamento(int alunoId, int turmaId)
        {
            try
            {
                var id = await _alunoTurmaService.RelacionamentoAlunoTurma(alunoId, turmaId);
                return Ok(new { Id = id });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("ListarRelacionamentos")]
        public async Task<IActionResult> Listar()
        {
            var alunoTurmas = await _alunoTurmaService.Listar();
            return Ok(alunoTurmas);
        }

        [HttpGet]
        [Route("BuscarAluno/{alunoId}")]
        public async Task<IActionResult> BuscarAluno(int alunoId)
        {
            var alunoTurmas = await _alunoTurmaService.BuscarAluno(alunoId);
            return Ok(alunoTurmas);
        }

        [HttpGet]
        [Route("BuscarTurma/{turmaId}")]
        public async Task<IActionResult> BuscarTurma(int turmaId)
        {
            var turma = await _alunoTurmaService.BuscarTurma(turmaId);
            return Ok(turma);
        }

        [HttpGet]
        [Route("ListarTurmas")]
        public async Task<IActionResult> ListarTurmas()
        {
            var turmas = await _alunoTurmaService.ListarTurmas();
            return Ok(turmas);
        }

        [HttpGet]
        [Route("ListarAlunos")]
        public async Task<IActionResult> ListarAlunos()
        {
            var alunos = await _alunoTurmaService.ListarAlunos();
            return Ok(alunos);
        }

        [HttpGet("ObterRelacionamentoPorId/{id}")]
        public async Task<IActionResult> ObterRelacionamentoPorId(int id)
        {
            try
            {
                var relacionamento = await _alunoTurmaService.ObterPorId(id);
                if (relacionamento == null)
                {
                    return NotFound(new { message = "Relacionamento não encontrado." });
                }

                return Ok(new
                {
                    AlunoId = relacionamento.AlunoId,
                    TurmaId = relacionamento.TurmaId
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno no servidor.", details = ex.Message });
            }
        }

        [HttpPut]
        [Route("AtualizarRelacionamento/{id}/{alunoId}/{turmaId}")]
        public async Task<IActionResult> AtualizarRelacionamento(int id, int alunoId, int turmaId)
        {
            try
            {
                var idAlunoTurma = await _alunoTurmaService.AtualizarRelacionamentoAlunoTurma(id, alunoId, turmaId);
                return Ok(new { Id = idAlunoTurma });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("AlterarStatus/{id}/{status}")]
        public async Task<IActionResult> AlterarStatus(int id, bool status)
        {
            await _alunoTurmaService.AlterarStatus(id, status);
            return Ok("Relacionamento desativado do sistema.");
        }

    }

}
