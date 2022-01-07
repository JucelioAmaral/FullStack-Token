
# Projeto FullStack

Api feita em .Net SDK 5.0.202, usando Entity framework para persistencia e acesso ao banco de dados,
Swagger para documentação da Api, SQLServer como SGBD.
Front-End utilizando Angular 12.

## Pré requisitos
 
1. [Node.js - Recommended For Most Users](https://nodejs.org/en/download/)
2. [Visual Code](https://code.visualstudio.com/download)
3. [Download .NET 5.0](https://dotnet.microsoft.com/download/dotnet/5.0)
4. [Angular](https://angular.io/guide/setup-local)

## Como baixar o código

git clone https://github.com/JucelioAmaral/FullStack-Token.git

## Como configurar a api(Back end)?

1. Abrir a Visual Code;
2. Configurar o arquivo "appsettings.Development.json" com a connectionString, apontando para o banco SQL server;
3. Instalar o pacote do sql server: "Install-Package Microsoft.EntityFrameworkCore.SqlServer" (ou no NuGet Gallery: (ctrl+shift+p) e instalar o pacote "Microsoft.EntityFrameworkCore.SqlServer" no "ProEventos.Persistence.csproj")
4. Abrir o Console/Terminal do Visual Code e entrar no diretório da api(...\Back\src). Pelo Visual Studio, basta abrir o Package Manager Console, alterar o "Default project" (que fica na parte superior do console) para o Class Library que encontra-se os arquivos de persistência;
5. Executar o comando: "dotnet ef migrations add Initial -p ProEventos.Persistence -s ProEventos.Api";
6. Executar o comando: "dotnet ef database update -s ProEventos.Api";
7. Executar a API pelo Visual Code usando o comando: dotnet watch run;

**API roda na porta 5001 e pode ser testada pelo link: https://localhost:5001/swagger/index.html**

## Como executar o app (Front end)?

1. Abrir o Console/Terminal do Visual Code e entrar no diretório do app;
2. Instalar o Angular versão mais nova usando o comando: npm install -g @angular/cli;
3. Execute ao comando: npm start ou ng serve -o;
4. Acesso a página Angula: http://localhost:4200/

