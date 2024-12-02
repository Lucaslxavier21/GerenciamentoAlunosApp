using Dapper;
using GestaoDeAlunosApi.Interfaces;
using GestaoDeAlunosApi.Models;
using System.Data;
using System.Data.Common;

namespace GestaoDeAlunosApi.Repositories
{
    public class AlunoTurmaRepository : IAlunoTurmaRepository
    {
        private readonly IDbConnection _connection;

        public AlunoTurmaRepository(IDbConnection connection)
        {
            _connection = connection;
        }
        public async Task<int> RelacionamentoAlunoTurma(int alunoId, int turmaId)
        {
            var query = @"
            INSERT INTO Aluno_Turma (AlunoId, TurmaId, Ativo)
            VALUES (@AlunoId, @TurmaId, 1)";
            return await _connection.ExecuteScalarAsync<int>(query, new { alunoId = alunoId , turmaId = turmaId});
        }
        public async Task<int> AtualizarRelacionamentoAlunoTurma(int id, int alunoId, int turmaId)
        {
            var query = @"
                        UPDATE Aluno_Turma
                        SET TurmaId = @TurmaId
                        WHERE Id = @Id AND Ativo = 1";

            return await _connection.ExecuteAsync(query, new {id, alunoId, turmaId });
        }
        public async Task<IEnumerable<AlunoTurmaModel>> Listar()
        {
            var query = @"
            SELECT at.Id, at.AlunoId, at.TurmaId, at.Ativo,
                   a.Nome, t.Turma
            FROM Aluno_Turma at
            INNER JOIN Aluno a ON at.AlunoId = a.Id
            INNER JOIN Turma t ON at.TurmaId = t.Id";
            return await _connection.QueryAsync<AlunoTurmaModel>(query);
        }
        public async Task<IEnumerable<AlunoTurmaModel>> BuscarAluno(int alunoId)
        {
            var query = @"
            SELECT Id as AlunoId, Nome, Usuario
            FROM Aluno 
            WHERE Id = @AlunoId AND Ativo = 1";
            return await _connection.QueryAsync<AlunoTurmaModel>(query, new { AlunoId = alunoId });
        }
        public async Task<IEnumerable<AlunoTurmaModel>> BuscarTurma(int turmaId)
        {
            var query = @"
            SELECT Id as TurmaId, Turma, CursoId, Ano
            FROM Turma 
            WHERE Id = @TurmaId AND Ativo = 1";
            return await _connection.QueryAsync<AlunoTurmaModel>(query, new { TurmaId = turmaId });
        }
        public async Task<IEnumerable<TurmaModel>> ListarTurmas()
        {
            var query = @"
            SELECT Id, Turma, CursoId, Ano
            FROM Turma 
            WHERE Ativo = 1";
            return await _connection.QueryAsync<TurmaModel>(query);
        }
        public async Task<IEnumerable<AlunoModel>> ListarAlunos()
        {
            var query = @"
            SELECT Id, Nome, Usuario
            FROM Aluno 
            WHERE Ativo = 1";
            return await _connection.QueryAsync<AlunoModel>(query);
        }
        public async Task<AlunoTurmaModel> BuscarRelacionamento(int alunoId, int turmaId)
        {
            var query = "SELECT * FROM Aluno_Turma WHERE AlunoId = @AlunoId AND TurmaId = @TurmaId";
            return await _connection.QuerySingleOrDefaultAsync<AlunoTurmaModel>(query, new { AlunoId = alunoId, TurmaId = turmaId });
        }
        public async Task<AlunoTurmaModel> ObterPorId(int id)
        {
            var query = "SELECT * FROM Aluno_Turma WHERE Id = @Id";
            return await _connection.QueryFirstOrDefaultAsync<AlunoTurmaModel>(query, new { Id = id });
        }
        public async Task AlterarStatus(int id, bool status)
        {
            var query = "UPDATE Aluno_Turma SET Ativo = @status WHERE Id = @Id";
            await _connection.ExecuteAsync(query, new { Id = id, status = status });
        }
    }

}
