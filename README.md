# Task Program

Este projeto é um aplicativo de gerenciamento de tarefas desenvolvido em C# com banco de dados MySQL.

## Pré-requisitos

* .NET SDK (versão 6.0 ou superior)
* MySQL (versão 8.0 ou superior)

## Instalação

1.  Clone o repositório: `git clone https://github.com/WeezyHS/Task_Program.git`
2.  Navegue até o diretório do projeto: `cd Task_Program`
3.  Restaure as dependências: `dotnet restore`
4.  Crie o banco de dados MySQL:
    * Execute o seguinte comando no terminal do MySQL: `mysql -u root -p < schema.sql`
    * Ou, utilize um cliente MySQL (como MySQL Workbench) para executar o script `schema.sql`.
5.  Popule o banco de dados (opcional):
    * Execute o seguinte comando no terminal do MySQL: `mysql -u root -p < data.sql`
    * Ou, utilize um cliente MySQL para executar o script `data.sql`.
6.  Configure as variáveis de ambiente:
    * Crie um arquivo `.env` na raiz do projeto.
    * Adicione as seguintes variáveis de ambiente:
        ```
        DB_HOST=localhost
        DB_USER=seu-usuario
        DB_PASSWORD=sua-senha
        DB_NAME=taskprogram_database
        ```
    * **Observação:** Por segurança, evite codificar as credenciais do banco de dados diretamente no código. Utilize variáveis de ambiente.
7.  Execute o aplicativo: `dotnet run`

## Scripts SQL

* [schema.sql](schema.sql): Cria o banco de dados e as tabelas necessárias.
* [data.sql](data.sql): Insere os dados iniciais no banco de dados (opcional).

## Estrutura do banco de dados

* Tabela `tasks`:
    * `Id` (INT): Identificador único da tarefa.
    * `Title` (VARCHAR): Título da tarefa.
    * `IsCompleted` (VARCHAR): Status da tarefa ("Completed" ou "Pending").

## Funcionalidades

* Adicionar tarefas com IDs aleatórios (verificação de duplicatas).
* Listar tarefas do banco de dados.
* Atualizar tarefas (título ou status).
* Excluir tarefas (uma tarefa ou todas).
* Menu interativo via console.
* Teste de conexão com o banco de dados.

## Tecnologias utilizadas

* C#
* .NET
* MySQL
* MySql.Data.MySqlClient

## Contribuição

Contribuições são bem-vindas! Sinta-se à vontade para abrir issues e pull requests.
