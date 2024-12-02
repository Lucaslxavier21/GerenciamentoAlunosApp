using GestaoDeAlunosApi.Interfaces;
using GestaoDeAlunosApi.Models;
using GestaoDeAlunosApi.Service;
using Moq;

namespace GerenciamentoDeAlunosTeste.Service
{
    public class AlunoServiceTest
    {
        private readonly Mock<IAlunoRepository> _alunoRepositoryMock;
        private readonly AlunoService _alunoService;
        public AlunoServiceTest()
        {
            _alunoRepositoryMock = new Mock<IAlunoRepository>();
            _alunoService = new AlunoService(_alunoRepositoryMock.Object);
        }
        [Fact]
        public async void AdicionarAluno()
        {
            var aluno = new AlunoRequestModel
            {
                Nome = "Lucas",
                Usuario = "lucas123",
                Senha = "Senha@F0rte010203!",
                Ativo = true
            };
            var alunoIdEsperado = 1;

            _alunoRepositoryMock
                .Setup(x => x.AdicionarAluno(It.IsAny<AlunoRequestModel>()))
                .ReturnsAsync(alunoIdEsperado);

            var resultado = await _alunoService.AdicionarAluno(aluno);

            Assert.Equal(alunoIdEsperado, resultado);
            _alunoRepositoryMock.Verify(repo => repo.AdicionarAluno(It.IsAny<AlunoRequestModel>()), Times.Once);

        }

        [Fact]
        public async void AdicionarAluno_DeveLancarExcecaoQuandoSenhaForFraca()
        {
            var aluno = new AlunoRequestModel
            {
                Nome = "Maria",
                Usuario = "maria123",
                Senha = "12345"
            };

            var exception = await Assert.ThrowsAsync<Exception>(() => _alunoService.AdicionarAluno(aluno));
            Assert.Equal("Senha fraca. A senha deve ter pelo menos 8 caracteres, incluindo letras maiúsculas, minúsculas, números e caracteres especiais.", exception.Message);

            _alunoRepositoryMock.Verify(x => x.AdicionarAluno(It.IsAny<AlunoRequestModel>()), Times.Never);
        }

        [Fact]
        public async void AtualizarAluno()
        {
            var alunoId = 1;
            var alunoAtualizado = new AlunoModel { Id = alunoId, Nome = "Lucas Atualizado", Usuario = "lucas1234", Senha = "SenhaForte@#12345", Ativo = true };

            _alunoRepositoryMock
                .Setup(x => x.AtualizarAluno(alunoId, alunoAtualizado))
                .ReturnsAsync(alunoAtualizado);

            var resultado = await _alunoService.AtualizarAluno(alunoId, alunoAtualizado);

            Assert.Equal(alunoAtualizado, resultado);
            _alunoRepositoryMock.Verify(repo => repo.AtualizarAluno(alunoId, alunoAtualizado), Times.Once);

        }

        [Fact]
        public async void AtualizarAluno_DeveLancarExcecaoQuandoDadosForemInvalidos()
        {
            var alunoId = 1;
            var alunoAtualizado = new AlunoModel { Nome = "Lucas Atualizado", Usuario = "123456", Ativo = true };

            await Assert.ThrowsAsync<Exception>(() => _alunoService.AtualizarAluno(alunoId, alunoAtualizado));
            _alunoRepositoryMock.Verify(repo => repo.AtualizarAluno(alunoId, alunoAtualizado), Times.Never);

        }

        [Fact]
        public async void BuscarAlunoPorId_DeveRetornarAlunoQuandIdExistir()
        {
            var alunoId = 1;
            var alunoEsperado = new AlunoModel { Id = alunoId, Nome = "Lucas", Usuario = "lucas1234", Ativo = true };

            _alunoRepositoryMock
                .Setup(x => x.BuscarAlunoPorId(alunoId))
                .ReturnsAsync(alunoEsperado);

            var resultado = await _alunoService.BuscarAlunoPorId(alunoId);

            Assert.NotNull(resultado);
            Assert.Equal(alunoEsperado.Nome, resultado.Nome);
            _alunoRepositoryMock.Verify(x => x.BuscarAlunoPorId(alunoId), Times.Once);
        }

        [Fact]
        public async void BuscarAlunoPorId_DeveRetornarNuloIdNaoExistir()
        {
            var alunoId = 0;

            _alunoRepositoryMock
                .Setup(x => x.BuscarAlunoPorId(alunoId))
                .ReturnsAsync((AlunoModel)null);

            var resultado = await _alunoService.BuscarAlunoPorId(alunoId);

            Assert.Null(resultado);
            _alunoRepositoryMock.Verify(x => x.BuscarAlunoPorId(alunoId), Times.Once);
        }
    }
}