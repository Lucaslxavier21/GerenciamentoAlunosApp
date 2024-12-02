using GestaoDeAlunosApi.Interfaces;
using GestaoDeAlunosApi.Models;
using GestaoDeAlunosApi.Service;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciamentoDeAlunosTeste.Service
{
    public class TurmaServiceTest
    {
        private readonly Mock<ITurmaRepository> _turmaRepositoryMock;
        private readonly TurmaService _turmaService;
        public TurmaServiceTest()
        {
            _turmaRepositoryMock = new Mock<ITurmaRepository>();
            _turmaService = new TurmaService(_turmaRepositoryMock.Object);
        }
        [Fact]
        public async Task AdicionarTurma_DeveRetornarIdQuandoTurmaForAdicionadaComSucesso()
        {
            var novaTurma = new TurmaModel
            {
                Turma = "Turma A",
                CursoId = 1,
                Ano = 2024
            };
            var idEsperado = 10;

            _turmaRepositoryMock
                .Setup(repo => repo.BuscarTurmaPorNome(novaTurma.Turma))
                .ReturnsAsync((string)null); 

            _turmaRepositoryMock
                .Setup(repo => repo.AdicionarTurma(novaTurma))
                .ReturnsAsync(idEsperado);

            var resultado = await _turmaService.AdicionarTurma(novaTurma);

            Assert.Equal(idEsperado, resultado);
            _turmaRepositoryMock.Verify(repo => repo.BuscarTurmaPorNome(novaTurma.Turma), Times.Once);
            _turmaRepositoryMock.Verify(repo => repo.AdicionarTurma(novaTurma), Times.Once);
        }

        [Fact]
        public async Task AdicionarTurma_DeveLancarExcecaoQuandoNomeDaTurmaJaExistir()
        {
            var novaTurma = new TurmaModel
            {
                Turma = "Turma B",
                CursoId = 2,
                Ano = 2025
            };

            _turmaRepositoryMock
                .Setup(repo => repo.BuscarTurmaPorNome(novaTurma.Turma))
                .ReturnsAsync("Turma B"); 

            var ex = await Assert.ThrowsAsync<ArgumentException>(() =>
                _turmaService.AdicionarTurma(novaTurma));
            Assert.Equal("Nome da turma já existente", ex.Message);

            _turmaRepositoryMock.Verify(repo => repo.BuscarTurmaPorNome(novaTurma.Turma), Times.Once);
            _turmaRepositoryMock.Verify(repo => repo.AdicionarTurma(It.IsAny<TurmaModel>()), Times.Never);
        }

        [Fact]
        public async Task BuscarTurmaPorId_DeveRetornarTurmaQuandoIdExistir()
        {
            var turmaId = 1;
            var turmaEsperada = new TurmaModel
            {
                Id = turmaId,
                Turma = "Turma A",
                CursoId = 1,
                Ano = 2024,
                Ativo = true
            };

            _turmaRepositoryMock
                .Setup(repo => repo.BuscarTurmaPorId(turmaId))
                .ReturnsAsync(turmaEsperada);

            var resultado = await _turmaService.BuscarTurmaPorId(turmaId);

            Assert.NotNull(resultado);
            Assert.Equal(turmaId, resultado.Id);
            Assert.Equal("Turma A", resultado.Turma);
            Assert.Equal(1, resultado.CursoId);
            Assert.Equal(2024, resultado.Ano);
            Assert.True(resultado.Ativo);

            _turmaRepositoryMock.Verify(repo => repo.BuscarTurmaPorId(turmaId), Times.Once);
        }
        [Fact]
        public async Task BuscarTurmaPorId_DeveRetornarNuloQuandoIdNaoExistir()
        {
            var turmaId = 0;

            _turmaRepositoryMock
                .Setup(repo => repo.BuscarTurmaPorId(turmaId))
                .ReturnsAsync((TurmaModel)null);

            var resultado = await _turmaService.BuscarTurmaPorId(turmaId);

            Assert.Null(resultado);
            _turmaRepositoryMock.Verify(repo => repo.BuscarTurmaPorId(turmaId), Times.Once);
        }

        [Fact]
        public async Task AtualizarTurma_DeveAtualizarComSucesso()
        {
            var turmaId = 1;
            var turmaAtualizada = new TurmaModel
            {
                Id = turmaId,
                Turma = "Turma Atualizada",
                CursoId = 2,
                Ano = 2025,
                Ativo = true
            };

            _turmaRepositoryMock
                .Setup(repo => repo.AtualizarTurma(turmaId, turmaAtualizada))
                .Returns(Task.CompletedTask);

            await _turmaService.AtualizarTurma(turmaId, turmaAtualizada);

            _turmaRepositoryMock.Verify(repo => repo.AtualizarTurma(turmaId, turmaAtualizada), Times.Once);
        }

        [Fact]
        public async Task AtualizarTurma_DeveLancarExcecaoSeIdForInvalido()
        {
            var turmaId = 0; 
            var turmaAtualizada = new TurmaModel
            {
                Id = turmaId,
                Turma = "Turma Atualizada",
                CursoId = 2,
                Ano = 2025,
                Ativo = true
            };

            _turmaRepositoryMock
                .Setup(repo => repo.AtualizarTurma(turmaId, turmaAtualizada))
                .ThrowsAsync(new ArgumentException("ID inválido."));

            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _turmaService.AtualizarTurma(turmaId, turmaAtualizada));
            Assert.Equal("ID inválido.", exception.Message);

            _turmaRepositoryMock.Verify(repo => repo.AtualizarTurma(turmaId, turmaAtualizada), Times.Once);
        }

        [Fact]
        public async Task AtualizarTurma_DeveLancarExcecaoSeModeloForNulo()
        {
            var turmaId = 1;
            TurmaModel modeloNulo = null;

            _turmaRepositoryMock
                .Setup(repo => repo.AtualizarTurma(turmaId, modeloNulo))
                .ThrowsAsync(new ArgumentNullException("modelo"));

            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => _turmaService.AtualizarTurma(turmaId, modeloNulo));
            Assert.Equal("modelo", exception.ParamName);

            _turmaRepositoryMock.Verify(repo => repo.AtualizarTurma(turmaId, modeloNulo), Times.Once);
        }

    }
}
