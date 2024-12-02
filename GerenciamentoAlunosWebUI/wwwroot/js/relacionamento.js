async function alternarStatus(id, status) {
    const url = `https://localhost:7003/api/AlunoTurma/AlterarStatus/${id}/${status}`;

    const data = {
        Ativo: status
    };

    try {
        const response = await fetch(url, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)
        });

        if (response.ok) {
            alert(status ? 'Relacionamento ativado com sucesso!' : 'Relacionamento desativado com sucesso!');
            location.reload();
        } else {
            const errorData = await response.json();
            alert(`Erro ao alternar o status do relacionamento: ${errorData.message || 'Erro desconhecido.'}`);
        }
    } catch (error) {
        console.error('Erro ao alternar status do relacionamento:', error);
        alert('Erro ao se conectar à API.');
    }
}

document.getElementById('btnAbrirModalCadastro').addEventListener('click', () => {
    document.getElementById('aluno').value = '';
    document.getElementById('turma').value = '';

    const form = document.getElementById('formRelacionarTurmaAluno');
    form.removeAttribute('data-relacionamento-id');

    document.getElementById('tituloModal').innerText = 'Adicionar Cadastro';
});

async function abrirModalEditarRelacionamento(id) {
    const url = `https://localhost:7003/api/AlunoTurma/ObterRelacionamentoPorId/${id}`;
    try {
        const response = await fetch(url);
        if (!response.ok) {
            alert('Erro ao buscar os dados do relacionamento.');
            return;
        }

        const data = await response.json();

        document.getElementById('aluno').value = data.alunoId
        document.getElementById('turma').value = data.turmaId;

        document.getElementById('aluno').disabled = true;

        document.getElementById('tituloModal').innerText = 'Editar Cadastro';

        document.getElementById('formRelacionarTurmaAluno').setAttribute('data-relacionamento-id', id);

        const modal = new bootstrap.Modal(document.getElementById('relacionarTurmaAlunoModal'));
        modal.show();
    } catch (error) {
        console.error('Erro ao carregar o relacionamento:', error);
        alert('Erro ao carregar os dados.');
    }
}

document.getElementById('btnSalvarRelacionamento').addEventListener('click', async () => {
    const alunoId = document.getElementById('aluno').value;
    const turmaId = document.getElementById('turma').value;

    if (!alunoId || !turmaId) {
        alert('Por favor, selecione Aluno e Turma.');
        return;
    }

    const form = document.getElementById('formRelacionarTurmaAluno');
    const relacionamentoId = form.getAttribute('data-relacionamento-id');

    const url = relacionamentoId
        ? `https://localhost:7003/api/AlunoTurma/AtualizarRelacionamento/${relacionamentoId}/${alunoId}/${turmaId}`
        : `https://localhost:7003/api/AlunoTurma/CriarRelacionamento/${alunoId}/${turmaId}`;

    try {
        const response = await fetch(url, {
            method: relacionamentoId ? 'PUT' : 'POST',
            headers: {
                'Content-Type': 'application/json'
            }
        });

        if (response.ok) {
            alert(relacionamentoId ? 'Relacionamento atualizado com sucesso!' : 'Relacionamento criado com sucesso!');
            const modal = bootstrap.Modal.getInstance(document.getElementById('relacionarTurmaAlunoModal'));
            modal.hide();
            form.reset();
            location.reload();
        } else {
            response.json().then(errorData => {
                alert(`Erro ao salvar os dados: ${errorData.message || 'Erro desconhecido.'}`);
            }).catch(() => {
                alert('Erro ao salvar os dados. Não foi possível obter detalhes do erro.');
            });
        }

    } catch (error) {
        console.error('Erro ao salvar o relacionamento:', error);
        alert('Erro ao conectar à API.');
    }
});