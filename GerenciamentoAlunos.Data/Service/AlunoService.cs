using GestaoDeAlunosApi.Interfaces;
using GestaoDeAlunosApi.Models;

namespace GestaoDeAlunosApi.Service
{
    public class AlunoService
    {
        private readonly IAlunoRepository _alunoRepository;

        public AlunoService(IAlunoRepository alunoRepository)
        {
            _alunoRepository = alunoRepository;
        }
        private bool ValidarSenha(string senha)
        {
            if (string.IsNullOrEmpty(senha)) return false;

            var validacoes = new List<Func<bool>>
            {
                () => senha.Length >= 8,                           
                () => senha.Any(char.IsUpper),                     
                () => senha.Any(char.IsLower),                     
                () => senha.Any(char.IsDigit),                     
                () => senha.Any(ch => !char.IsLetterOrDigit(ch))   
            };

            return validacoes.All(validacao => validacao());
        }

        public async Task<int> AdicionarAluno(AlunoRequestModel aluno)
        {
            if (!ValidarSenha(aluno.Senha))
                throw new Exception("Senha fraca. A senha deve ter pelo menos 8 caracteres, incluindo letras maiúsculas, minúsculas, números e caracteres especiais.");

            return await _alunoRepository.AdicionarAluno(aluno);
        }


        public async Task<IEnumerable<AlunoModel>> ListarAlunos() =>
            await _alunoRepository.ListarAlunos();
        public async Task<AlunoModel> BuscarAlunoPorId(int id)
        {
            return await _alunoRepository.BuscarAlunoPorId(id);
        }

        public async Task<AlunoModel> AtualizarAluno(int id, AlunoModel aluno)
        {
            if (!ValidarSenha(aluno.Senha))
                throw new Exception("Senha fraca. A senha deve ter pelo menos 8 caracteres, incluindo letras maiúsculas, minúsculas, números e caracteres especiais.");

            return await _alunoRepository.AtualizarAluno(id, aluno);
        }

        public async Task AlterarStatus(int id, bool status) =>
            await _alunoRepository.AlterarStatus(id, status);
    }
}
