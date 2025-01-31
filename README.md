# Digital Wallet API

Bem-vindo ao repositório do projeto **Digital Wallet API ** (Backend)! Este guia fornecerá instruções para configurar, instalar e executar o backend do projeto localmente.

---

## **Tecnologias Utilizadas**

Certifique-se de que você tenha os seguintes itens instalados em sua máquina:

.NET 7 - Framework principal

Entity Framework Core - ORM para interagir com o banco de dados

PostgreSQL - Banco de dados relacional

Migrations - Gerenciamento de esquema do banco de dados

## **Passos de Instalação**

### 1. **Clonar o Repositório**

Clone o repositório do projeto para sua máquina local:

```bash
git clone https://github.com/delimarenata/DigitalWalletAPI.git
```

### 2. **Instalar as Dependências**

Navegue até o diretório do projeto clonado e execute o seguinte comando para instalar todas as dependências:

```bash
cd horario-iscte-grupo-2
npm install
```

### 3. **Configurar o Banco de Dados**

Crie um banco de dados no PostgreSQL e atualize a string de conexão no appsettings.json:
{
"ConnectionStrings": {
"DefaultConnection": "Host=localhost;Port=5432;Database=meuprojeto;Username=seuusuario;Password=suasenha"
}
}

Aplicar Migrations

Se ainda não houver o banco configurado, execute:
dotnet ef database update

````

### 4. **Executar o Servidor**

Inicie o servidor backend com o comando:

```dotnet run
````

O servidor será executado no endpoint:

```
http://localhost:5275
 ou na porta especificada.
```

### 5. **Documentação da API**

A documentação da API está disponível em:

```
http://localhost:5275/swagger/index.html
```

---

## **Principais Dependências**

AutoMapper.Extensions.Microsoft.DependencyInjection (12.0.1) - Mapeamento de objetos

BCrypt.Net-Next (4.0.3) - Criptografia de senhas

Microsoft.AspNetCore.Authentication.JwtBearer (7.0.0) - Autenticação JWT

Microsoft.AspNetCore.OpenApi (7.0.0) - Suporte a OpenAPI

Microsoft.EntityFrameworkCore (7.0.0) - ORM Entity Framework Core

Microsoft.EntityFrameworkCore.Design (7.0.0) - Ferramentas de design para EF Core

Npgsql.EntityFrameworkCore.PostgreSQL (7.0.0) - Provedor PostgreSQL para EF Core

Swashbuckle.AspNetCore (7.2.0) - Documentação Swagger

Swashbuckle.AspNetCore.Annotations (7.2.0) - Anotações para Swagger

---

```
├── src
│   ├── controllers    # Controladores da API
│   ├── models         # Modelos do Sequelize
├── config             # Configuração do banco de dados
├── docker-compose.yml # Configuração do Docker Compose
├── package.json       # Dependências e scripts do projeto
└── README.md          # Documentação do projeto
```
