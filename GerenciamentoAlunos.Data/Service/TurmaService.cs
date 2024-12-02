using GestaoDeAlunosApi.Interfaces;
using GestaoDeAlunosApi.Models;

namespace GestaoDeAlunosApi.Service
{
    public class TurmaService
    {
        private readonly ITurmaRepository _turmaRepository;

        public TurmaService(ITurmaRepository turmaRepository)
        {
            _turmaRepository = turmaRepository;
        }
        public async Task<int> AdicionarTurma(TurmaModel modelo)
        {
            var turmaExistente = await _turmaRepository.BuscarTurmaPorNome(modelo.Turma);
            if (!string.IsNullOrEmpty(turmaExistente)) 
            {
                throw new ArgumentException("Nome da turma já existente");
            }

            return await _turmaRepository.AdicionarTurma(modelo);
        }

        public async Task<IEnumerable<TurmaModel>> ListarTurmas() =>
            await _turmaRepository.ListarTurmas();
        public async Task<TurmaModel> BuscarTurmaPorId(int id)
        {
            return await _turmaRepository.BuscarTurmaPorId(id);
        }

        public async Task AtualizarTurma(int id, TurmaModel modelo) =>
            await _turmaRepository.AtualizarTurma(id, modelo);

        public async Task AlterarStatus(int id, bool status) =>
            await _turmaRepository.AlterarStatus(id, status);
    }
}
