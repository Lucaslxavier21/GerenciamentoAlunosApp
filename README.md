# Gerenciamento de Alunos

Este projeto é uma aplicação para gerenciar informações de alunos, permitindo visualizar, adicionar e gerenciar dados de maneira prática e eficiente.

## Estrutura do Projeto

O projeto segue uma arquitetura de camadas, com as seguintes divisões:

1. **API**:  
   Responsável por expor os dados dos alunos por meio de uma API RESTful.

2. **Aplicação Web**:  
   Uma interface de usuário desenvolvida para interação e gerenciamento dos dados dos alunos.

3. **Camada de Testes**:  
   Inclui testes automatizados para validar a funcionalidade do sistema.

---

## Configuração do Projeto

### Requisitos
- **.NET 8.0**
- **Banco de Dados**: SQL Server  
  - Configuração da `ConnectionString` no arquivo `appsettings.json`.

### Estrutura das Camadas
- **Data**:  
  Contém repositórios, interfaces, models e serviços essenciais para a lógica de negócio e de dados.
- **WebUI**:  
  Apresenta a interface do usuário e também expõe as rotas da API.
- **Testes**:  
  Inclui os testes necessários para garantir a qualidade e estabilidade da aplicação.

### Pacotes Necessários
Certifique-se de instalar os seguintes pacotes na camada **Data**:
- `Dapper` (ORM)
- `Microsoft.Data.SqlClient` (cliente para conexão com SQL Server)

### Configuração do Banco de Dados
Certifique-se de ajustar a `ConnectionString` no arquivo `appsettings.json`, conforme o exemplo abaixo:
```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=.;Initial Catalog=AlunosAPP;Integrated Security=True;Encrypt=False"
}
```
