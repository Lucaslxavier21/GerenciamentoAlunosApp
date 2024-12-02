async function alternarStatus(id, status) {
    const url = `https://localhost:7003/api/Aluno/AlterarStatus/${id}/${status}`;

    const data = { Ativo: status };

    try {
        const response = await fetch(url, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)
        });

        if (response.ok) {
            alert(status ? 'Aluno ativado com sucesso!' : 'Aluno desativado com sucesso!');
            location.reload();
        } else {
            const errorData = await response.json();
            alert(`Erro ao alternar o status do Aluno: ${errorData.message || 'Erro desconhecido.'}`);
        }
    } catch (error) {
        console.error('Erro ao alternar status do Aluno:', error);
        alert('Erro ao se conectar à API.');
    }
}

document.getElementById('btnSalvarAluno').addEventListener('click', async () => {
    const form = document.getElementById('formCadastroAluno');
    const alunoId = form.getAttribute('data-aluno-id');

    const nome = document.getElementById('nome').value.trim();
    const usuario = document.getElementById('usuario').value.trim();
    const senha = document.getElementById('senha').value.trim();
    const ativo = document.getElementById('ativo').checked;

    if (!nome || !usuario || !senha) {
        alert('Por favor, preencha todos os campos!');
        return;
    }

    const aluno = {
        Nome: nome,
        Usuario: usuario,
        Senha: senha,
        Ativo: ativo
    };

    const url = alunoId
        ? `https://localhost:7003/api/Aluno/AtualizarAluno/${alunoId}`
        : 'https://localhost:7003/api/Aluno/AdicionarAluno';

    try {
        const response = await fetch(url, {
            method: alunoId ? 'PUT' : 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(aluno)
        });

        if (response.ok) {
            alert(alunoId ? 'Aluno atualizado com sucesso!' : 'Aluno cadastrado com sucesso!');
            const modal = bootstrap.Modal.getInstance(document.getElementById('cadastrarAlunoModal'));
            modal.hide();
            form.reset();
            form.removeAttribute('data-aluno-id');
            location.reload();
        } else {
            const errorData = await response.json();
            const errorMessage = errorData.message || 'Erro ao salvar os dados do aluno.';
            alert(`Erro: ${errorMessage}`);
        }
    } catch (error) {
        console.error('Erro:', error);
        alert('Erro ao se conectar à API.');
    }
});

document.getElementById('btnAbrirModalCadastro').addEventListener('click', () => {
    document.getElementById('nome').value = '';
    document.getElementById('usuario').value = '';
    document.getElementById('senha').value = '';
    document.getElementById('ativo').checked = true;

    const form = document.getElementById('formCadastroAluno');
    form.removeAttribute('data-aluno-id');

    document.getElementById('tituloModal').innerText = 'Adicionar Cadastro';
});

async function abrirModalEditar(id) {
    const url = `https://localhost:7003/api/Aluno/BuscarAlunoPorId/${id}`;

    try {
        const response = await fetch(url);
        if (!response.ok) {
            const errorData = await response.json();
            alert(`Erro ao buscar dados do aluno: ${errorData.message || 'Erro desconhecido.'}`);
            return;
        }

        const aluno = await response.json();
        document.getElementById('nome').value = aluno.nome;
        document.getElementById('usuario').value = aluno.usuario;
        document.getElementById('senha').value = ''; 
        document.getElementById('ativo').checked = aluno.ativo;

        document.getElementById('tituloModal').innerText = 'Editar Cadastro';
        document.getElementById('formCadastroAluno').setAttribute('data-aluno-id', id);

        const modal = new bootstrap.Modal(document.getElementById('cadastrarAlunoModal'));
        modal.show();
    } catch (error) {
        console.error('Erro:', error);
        alert('Erro ao buscar os dados do aluno.');
    }
}
