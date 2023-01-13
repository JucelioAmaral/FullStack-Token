
# Projeto FullStack

Api feita em .Net SDK 5.0.202, usando Entity framework para persistencia e acesso ao banco de dados,
Swagger para documentação da Api, SQLServer como SGBD.
Front-End utilizando Angular 11 e Node js 12.11.1

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
5. Executar o comando: "add-migration Initial" ou "dotnet ef migrations add Initial -p ProEventos.Persistence -s ProEventos.Api";
6. Executar o comando: "update-database" ou "dotnet ef database update -s ProEventos.Api";
7. Criar as pastas: Resources, Images e Perfil, conforme a print "Para salvar imagens dos perfis.png";
8. Executar a API pelo Visual Code usando o comando: dotnet watch run.

**API roda na porta 5001 e pode ser testada pelo link: https://localhost:5001/swagger/index.html**

## Como executar o app (Front end)?


1. Instalar a versão do Angular 11.2.1, usando o comando: npm install -g @angular/cli@11.2.1;
2. Instalar o NVM (Node Version Manager), caso não tenha instalado o node: nvm install 12.11.1;
3. Abrir o Console/Terminal do Visual Code e entrar no diretório do app (ProEventos-App);
4. Execute ao comando: npm start ou ng serve -o;
5. Acesso a página Angula: http://localhost:4200/