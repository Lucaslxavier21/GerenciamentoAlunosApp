using Dapper;
using GestaoDeAlunosApi.Interfaces;
using GestaoDeAlunosApi.Models;
using System.Data;

namespace GestaoDeAlunosApi.Repositories
{
    public class TurmaRepository : ITurmaRepository
    {
        private readonly IDbConnection _connection;

        public TurmaRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<int> AdicionarTurma(TurmaModel model)
        {
            var query = @"
            INSERT INTO Turma (Turma, Ano, Ativo, CursoId)
            VALUES (@Turma, @Ano, @Ativo, @CursoId)";

            return await _connection.ExecuteScalarAsync<int>(query, model);
        }

        public async Task<IEnumerable<TurmaModel>> ListarTurmas()
        {
            var query = "SELECT * FROM Turma";
            return await _connection.QueryAsync<TurmaModel>(query);
        }
        public async Task<TurmaModel> BuscarTurmaPorId(int id)
        {
            var query = "SELECT * FROM Turma WHERE Id = @id";
            return await _connection.QueryFirstAsync<TurmaModel>(query, new {Id = id});
        }
        public async Task AtualizarTurma(int idTurma, TurmaModel model)
        {
            var query = @"
            UPDATE Turma
            SET Turma = @Turma, Ano = @Ano, Ativo = @Ativo, CursoId = @CursoId
            WHERE Id = @Id";

            await _connection.ExecuteAsync(query, new { model.Turma, model.Ativo, Id = idTurma, model.Ano, model.CursoId });
        }

        public async Task AlterarStatus(int idTurma, bool status)
        {
            var query = "UPDATE Turma SET Ativo = @status WHERE Id = @idTurma";
            await _connection.ExecuteAsync(query, new { idTurma = idTurma, status = status});
        }

        public async Task<string> BuscarTurmaPorNome(string nomeTurma)
        {
            var select = "SELECT * FROM Turma WHERE Turma = @nomeTurma"; // Use @nomeTurma como parâmetro
            return await _connection.QueryFirstOrDefaultAsync<string>(select, new { nomeTurma });
        }
    }
}
