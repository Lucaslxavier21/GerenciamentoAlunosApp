# AlunosAPP

Este é um projeto de gerenciamento de alunos, onde você pode visualizar, adicionar e gerenciar informações de alunos.

## Estrutura do Projeto

O projeto consiste em duas partes principais:

1. **API**: Exposição de dados dos alunos via uma API RESTful.
2. **Aplicação Web**: Interface de usuário para visualização.

## Configuração do Banco de Dados

A `ConnectionString` para o banco de dados está configurada no arquivo `appsettings.json` do projeto, conforme abaixo:

```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=.;Initial Catalog=AlunosAPP;Integrated Security=True;Encrypt=False"
}

## Endereço API
https://localhost:7003/swagger/index.html

## Endereço programa
https://localhost:7003/ListaDeAlunos
