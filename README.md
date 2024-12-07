# Gerenciamento de Alunos

Este é um projeto de gerenciamento de alunos, onde você pode visualizar, adicionar e gerenciar informações de alunos.

## Estrutura do Projeto

O projeto consiste em duas partes principais:

1. **API**: Exposição de dados dos alunos via uma API RESTful.
2. **Aplicação Web**: Interface de usuário para visualização.

## Configuração do Projeto:
```json
  - Versão do .NET => 8.0
  - O projeto está separado em 3 camadas, Data ( onde contém repositórios, interfaces, models e serviços ), WebUI ( interface do projeto e API ) e Camada de testes.
  - Pacotes necessários: Dapper (ORM), Microsoft.Data.SqlClient. ( Intalar os pacotes na camada de Data )

## Configuração do Banco de Dados

A `ConnectionString` para o banco de dados está configurada no arquivo `appsettings.json` do projeto, conforme abaixo:

```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=.;Initial Catalog=AlunosAPP;Integrated Security=True;Encrypt=False"
}
```

## Endereço API
https://localhost:7003/swagger/index.html

## Endereço programa
https://localhost:7003/ListaDeAlunos
