using GestaoDeAlunosApi.Models;
using GestaoDeAlunosApi.Service;
using Microsoft.AspNetCore.Mvc;

namespace GestaoDeAlunosApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TurmaController : Controller
    {
        private readonly TurmaService _turmaService;

        public TurmaController(TurmaService turmaService)
        {
            _turmaService = turmaService;
        }

        [HttpPost]
        [Route("AdicionarTurma")]
        public async Task<IActionResult> AdicionarTurma(TurmaModel model)
        {
            try
            {
                var result = await _turmaService.AdicionarTurma(model);
                return Ok("Turma adicionada com sucesso!");
            }
            catch(Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("ListarTurmas")]
        public async Task<IActionResult> ListarTurmas()
        {
            var turmas = await _turmaService.ListarTurmas();
            return Ok(turmas);
        }

        [HttpGet]
        [Route("BuscarTurmaPorId/{id}")]
        public async Task<IActionResult> BuscarTurmaPorId(int id)
        {
            var turma = await _turmaService.BuscarTurmaPorId(id);
            return Ok(turma);
        }

        [HttpPut]
        [Route("AtualizarTurma/{id}")]
        public async Task<IActionResult> AtualizarTurma(int id, TurmaModel model)
        {
            await _turmaService.AtualizarTurma(id, model);
            return Ok("Turma atualizada com sucesso.");
        }

        [HttpPut]
        [Route("AlterarStatus/{id}/{status}")]
        public async Task<IActionResult> AlterarStatus(int id, bool status)
        {
            await _turmaService.AlterarStatus(id, status);
            return Ok("Turma dasativada do sistema.");
        }
    }
}
