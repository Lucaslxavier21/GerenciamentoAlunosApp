using GestaoDeAlunosApi.Models;

namespace GestaoDeAlunosApi.Interfaces
{
    public interface IAlunoTurmaRepository
    {
        Task<int> RelacionamentoAlunoTurma(int alunoId, int turmaId);
        Task<int> AtualizarRelacionamentoAlunoTurma(int id, int alunoId, int turmaId);
        Task<AlunoTurmaModel> BuscarRelacionamento(int alunoId, int turmaId);
        Task<AlunoTurmaModel> ObterPorId(int id);
        Task<IEnumerable<AlunoTurmaModel>> Listar();
        Task<IEnumerable<AlunoTurmaModel>> BuscarAluno(int alunoId);
        Task<IEnumerable<AlunoTurmaModel>> BuscarTurma(int turmaId);
        Task<IEnumerable<TurmaModel>> ListarTurmas();
        Task<IEnumerable<AlunoModel>> ListarAlunos();
        Task AlterarStatus(int id, bool status);

    }
}
