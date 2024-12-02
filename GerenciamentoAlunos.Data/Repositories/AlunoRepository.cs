using Dapper;
using GestaoDeAlunosApi.Interfaces;
using GestaoDeAlunosApi.Models;
using System.Data;

namespace GestaoDeAlunosApi.Repositories
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly IDbConnection _connection;

        public AlunoRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<int> AdicionarAluno(AlunoRequestModel aluno)
        {
            var query = @"
            INSERT INTO Aluno (Nome, Usuario, Senha, Ativo)
            VALUES (@Nome, @Usuario, @Senha, @Ativo)";

            return await _connection.ExecuteScalarAsync<int>(query, aluno);
        }

        public async Task<IEnumerable<AlunoModel>> ListarAlunos()
        {
            var query = "SELECT * FROM Aluno";
            return await _connection.QueryAsync<AlunoModel>(query);
        }
        public async Task<AlunoModel> BuscarAlunoPorId(int id)
        {
            var query = "SELECT * FROM Aluno WHERE Id = @id";
            return await _connection.QueryFirstAsync<AlunoModel>(query, new {Id = id});
        }
        public async Task<AlunoModel> AtualizarAluno(int id, AlunoModel aluno)
        {
            var query = @"
            UPDATE Aluno
            SET Nome = @Nome, Usuario = @Usuario, Ativo = @Ativo, Senha = @Senha
            WHERE Id = @Id";

            return await _connection.ExecuteScalarAsync<AlunoModel>(query, new { aluno.Nome, aluno.Usuario, Id = id,aluno.Ativo, aluno.Senha });
        }

        public async Task AlterarStatus(int id, bool status)
        {
            var query = "UPDATE Aluno SET Ativo = @status WHERE Id = @Id";
            await _connection.ExecuteAsync(query, new { Id = id, status = status });
        }
    }
}
