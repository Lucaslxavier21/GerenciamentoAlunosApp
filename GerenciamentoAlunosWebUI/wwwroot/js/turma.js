async function alternarStatus(id, status) {
    const url = `https://localhost:7003/api/Turma/AlterarStatus/${id}/${status}`;

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
            alert(status ? 'Turma ativada com sucesso!' : 'Turma desativada com sucesso!');
            location.reload();
        } else {
            const errorData = await response.json();
            alert(`Erro ao alternar o status da Turma: ${errorData.message || 'Erro desconhecido.'}`);
        }
    } catch (error) {
        console.error('Erro ao alternar status da Turma:', error);
        alert('Erro ao se conectar à API.');
    }
}

document.getElementById('btnSalvarTurma').addEventListener('click', async () => {
    const turma = document.getElementById('turma').value;
    const curso = document.getElementById('curso').value;
    const ano = document.getElementById('ano').value;
    const ativo = document.getElementById('ativo').checked;

    if (!turma || !ano) {
        alert('Por favor, preencha todos os campos!');
        return;
    }

    const nomeTurma = {
        Turma: turma,
        CursoId: curso,
        Ano: ano,
        Ativo: ativo
    };

    const form = document.getElementById('formCadastroTurma');
    const turmaId = form.getAttribute('data-turma-id');

    const url = turmaId
        ? `https://localhost:7003/api/Turma/AtualizarTurma/${turmaId}`
        : 'https://localhost:7003/api/Turma/AdicionarTurma';

    try {
        const response = await fetch(url, {
            method: turmaId ? 'PUT' : 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(nomeTurma)
        });

        if (response.ok) {
            alert(turmaId ? 'Turma atualizada com sucesso!' : 'Turma cadastrada com sucesso!');
            const modal = bootstrap.Modal.getInstance(document.getElementById('cadastrarTurmaModal'));
            modal.hide();
            document.getElementById('formCadastroTurma').reset();
            location.reload();
        } else {
            const errorData = await response.json();
            const errorMessage = errorData.message || 'Erro ao salvar os dados da turma.';
            alert(`Erro: ${errorMessage}`);
        }
    } catch (error) {
        console.error('Erro:', error);
        alert('Erro ao se conectar à API.');
    }
});

document.getElementById('btnAbrirModalCadastro').addEventListener('click', () => {
    document.getElementById('turma').value = '';
    document.getElementById('ano').value = '';
    document.getElementById('ativo').value = '';

    const form = document.getElementById('formCadastroTurma');
    form.removeAttribute('data-turma-id');

    document.getElementById('tituloModal').innerText = 'Adicionar Cadastro';
});

async function abrirModalEditarTurma(id) {
    const url = `https://localhost:7003/api/Turma/BuscarTurmaPorId/${id}`;

    try {
        const response = await fetch(url);
        if (!response.ok) {
            alert('Erro ao buscar dados da turma.');
            return;
        }

        const turma = await response.json();


        document.getElementById('turma').value = turma.turma;
        document.getElementById('ano').value = turma.ano
        document.getElementById('ativo').checked = turma.ativo;

        document.getElementById('tituloModal').innerText = 'Editar Cadastro';

        document.getElementById('formCadastroTurma').setAttribute('data-turma-id', id);

        const modal = new bootstrap.Modal(document.getElementById('cadastrarTurmaModal'));
        modal.show();
    } catch (error) {
        console.error('Erro:', error);
        alert('Erro ao buscar os dados da turma.');
    }
}