using GestaoDeAlunosApi.Models;

namespace GestaoDeAlunosApi.Interfaces
{
    public interface IAlunoRepository
    {
        Task<int> AdicionarAluno(AlunoRequestModel aluno);
        Task<IEnumerable<AlunoModel>> ListarAlunos();
        Task<AlunoModel> BuscarAlunoPorId(int id);
        Task<AlunoModel> AtualizarAluno(int idAluno, AlunoModel aluno);
        Task AlterarStatus(int idAluno, bool status);
    }
}
