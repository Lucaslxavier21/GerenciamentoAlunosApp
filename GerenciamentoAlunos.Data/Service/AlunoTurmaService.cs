using GestaoDeAlunosApi.Interfaces;
using GestaoDeAlunosApi.Models;

namespace GestaoDeAlunosApi.Service
{
    public class AlunoTurmaService
    {
        private readonly IAlunoTurmaRepository _alunoTurmaRepository;

        public AlunoTurmaService(IAlunoTurmaRepository alunoTurmaRepository)
        {
            _alunoTurmaRepository = alunoTurmaRepository;
        }
        public async Task<int> RelacionamentoAlunoTurma(int alunoId, int turmaId)
        {
            if (alunoId <= 0 || turmaId <= 0)
                throw new ArgumentException("AlunoId e TurmaId devem ser válidos.");

            var existeRelacao = await _alunoTurmaRepository.BuscarRelacionamento(alunoId, turmaId);

            if (existeRelacao != null)
                throw new InvalidOperationException("O Aluno já está relacionado com esta Turma.");

            return await _alunoTurmaRepository.RelacionamentoAlunoTurma(alunoId, turmaId);
        }
        public async Task<int> AtualizarRelacionamentoAlunoTurma(int id, int alunoId, int turmaId)
        {
            if (alunoId <= 0 || turmaId <= 0)
                throw new ArgumentException("AlunoId e TurmaId devem ser válidos.");

            var existeRelacao = await _alunoTurmaRepository.BuscarRelacionamento(alunoId, turmaId);

            if (existeRelacao != null)
                throw new InvalidOperationException("O Aluno já está relacionado com esta Turma.");

            return await _alunoTurmaRepository.AtualizarRelacionamentoAlunoTurma(id, alunoId, turmaId);
        }

        public async Task<IEnumerable<AlunoTurmaModel>> Listar()
        {
            var lista = await _alunoTurmaRepository.Listar();

            return lista;
        }
        public async Task<IEnumerable<AlunoTurmaModel>> BuscarAluno(int alunoId) =>
            await _alunoTurmaRepository.BuscarAluno(alunoId);

        public async Task<IEnumerable<AlunoTurmaModel>> BuscarTurma(int turmaId) =>
            await _alunoTurmaRepository.BuscarTurma(turmaId);
        public async Task<IEnumerable<TurmaModel>> ListarTurmas() =>
            await _alunoTurmaRepository.ListarTurmas();
        public async Task<IEnumerable<AlunoModel>> ListarAlunos() =>
            await _alunoTurmaRepository.ListarAlunos();
        public async Task<AlunoTurmaModel> ObterPorId(int id)
        {
            return await _alunoTurmaRepository.ObterPorId(id);
        }
        public async Task AlterarStatus(int id, bool status) =>
            await _alunoTurmaRepository.AlterarStatus(id, status);
    }

}
