using GestaoDeAlunosApi.Models;

namespace GestaoDeAlunosApi.Interfaces
{
    public interface ITurmaRepository
    {
        Task<int> AdicionarTurma(TurmaModel model);
        Task<IEnumerable<TurmaModel>> ListarTurmas();
        Task<TurmaModel> BuscarTurmaPorId(int id);
        Task AtualizarTurma(int idTurma, TurmaModel model);
        Task AlterarStatus(int idTurma, bool status);
        Task<string> BuscarTurmaPorNome(string nomeTurma);
    }
}
