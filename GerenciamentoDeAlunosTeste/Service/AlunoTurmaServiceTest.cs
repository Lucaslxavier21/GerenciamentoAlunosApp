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
    public class AlunoTurmaServiceTest
    {
        private readonly Mock<IAlunoTurmaRepository> _alunoTurmaRepositoryMock;
        private readonly AlunoTurmaService _alunoTurmaService;
        public AlunoTurmaServiceTest()
        {
            _alunoTurmaRepositoryMock = new Mock<IAlunoTurmaRepository>();
            _alunoTurmaService = new AlunoTurmaService(_alunoTurmaRepositoryMock.Object);
        }

        [Fact]
        public async void RelacionamentoAlunoTurma()
        {
            var alunoId = 1;
            var turmaId = 2;
            var idRelacionamento = 1;

            _alunoTurmaRepositoryMock
                .Setup(x => x.BuscarRelacionamento(alunoId, turmaId))
                .ReturnsAsync((AlunoTurmaModel)null);

            _alunoTurmaRepositoryMock
                .Setup(x => x.RelacionamentoAlunoTurma(alunoId, turmaId))
                .ReturnsAsync(idRelacionamento);

            var resultado = await _alunoTurmaService.RelacionamentoAlunoTurma(alunoId, turmaId);

            Assert.Equal(idRelacionamento, resultado);
            _alunoTurmaRepositoryMock.Verify(x => x.BuscarRelacionamento(alunoId, turmaId), Times.Once);
            _alunoTurmaRepositoryMock.Verify(x => x.RelacionamentoAlunoTurma(alunoId, turmaId));
        }

        [Fact]
        public async void RelacionamentoAlunoTurma_DeveLancarExcecaoQuandoIdsForemInvalidos()
        {
            var alunoId = 0;
            var turmaId = 0;
            var idRelacionamento = 0;

            var ex = await Assert.ThrowsAsync<ArgumentException>(() =>
            _alunoTurmaService.RelacionamentoAlunoTurma(alunoId, turmaId));
            Assert.Equal("AlunoId e TurmaId devem ser válidos.", ex.Message);

            _alunoTurmaRepositoryMock.Verify(repo => repo.BuscarRelacionamento(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
            _alunoTurmaRepositoryMock.Verify(repo => repo.RelacionamentoAlunoTurma(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task RelacionamentoAlunoTurma_DeveLancarExcecaoQuandoRelacaoJaExistir()
        {
            var alunoId = 1;
            var turmaId = 2;
            var relacaoExistente = new AlunoTurmaModel { AlunoId = alunoId, TurmaId = turmaId };

            _alunoTurmaRepositoryMock
                .Setup(repo => repo.BuscarRelacionamento(alunoId, turmaId))
                .ReturnsAsync(relacaoExistente);

            var ex = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _alunoTurmaService.RelacionamentoAlunoTurma(alunoId, turmaId));
            Assert.Equal("O Aluno já está relacionado com esta Turma.", ex.Message);

            _alunoTurmaRepositoryMock.Verify(repo => repo.BuscarRelacionamento(alunoId, turmaId), Times.Once);
            _alunoTurmaRepositoryMock.Verify(repo => repo.RelacionamentoAlunoTurma(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }
        [Fact]
        public async Task AtualizarRelacionamentoAlunoTurma_DeveRetornarIdQuandoAtualizacaoForBemSucedida()
        {
            var id = 1;
            var alunoId = 2;
            var turmaId = 3;
            var idEsperado = 100; 

            _alunoTurmaRepositoryMock
                .Setup(repo => repo.BuscarRelacionamento(alunoId, turmaId))
                .ReturnsAsync((AlunoTurmaModel)null); 

            _alunoTurmaRepositoryMock
                .Setup(repo => repo.AtualizarRelacionamentoAlunoTurma(id, alunoId, turmaId))
                .ReturnsAsync(idEsperado);

            var resultado = await _alunoTurmaService.AtualizarRelacionamentoAlunoTurma(id, alunoId, turmaId);

            Assert.Equal(idEsperado, resultado);
            _alunoTurmaRepositoryMock.Verify(repo => repo.BuscarRelacionamento(alunoId, turmaId), Times.Once);
            _alunoTurmaRepositoryMock.Verify(repo => repo.AtualizarRelacionamentoAlunoTurma(id, alunoId, turmaId), Times.Once);
        }
        [Fact]
        public async Task AtualizarRelacionamentoAlunoTurma_DeveLancarExcecaoQuandoIdsForemInvalidos()
        {
            var id = 0;
            var alunoId = 0;
            var turmaId = 0;

            var ex = await Assert.ThrowsAsync<ArgumentException>(() =>
                _alunoTurmaService.AtualizarRelacionamentoAlunoTurma(id, alunoId, turmaId));
            Assert.Equal("AlunoId e TurmaId devem ser válidos.", ex.Message);

            _alunoTurmaRepositoryMock.Verify(repo => repo.BuscarRelacionamento(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
            _alunoTurmaRepositoryMock.Verify(repo => repo.AtualizarRelacionamentoAlunoTurma(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }

    }
}
