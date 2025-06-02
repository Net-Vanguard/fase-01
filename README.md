<details>
<summary>English version 🇺🇸</summary>

<h1 align="center">
  <br>
  <img src="https://github.com/user-attachments/assets/620f926b-da49-41d0-8a51-b1b1214f4c4b" alt="ContactRegionator Logo" width="180">
  <br>
  FIAP Cloud Games (FCG)
  <br>
</h1>

<h4 align="center">
Service developed as part of the Phase 1 Tech Challenge — <a href="https://www.fiap.com.br/" target="_blank">FIAP</a> Postgraduate in .NET Software Architecture with Azure.
</h4>

<p align="center">
  <a href="#✨-key-features">✨ Key Features</a> •
  <a href="#🧠-technical-requirements">🧠 Technical Requirements</a> •
  <a href="#🚀-ddd">🚀  🏗️ Project Structure (DDD)</a> •
  <a href="#🚀-how-to-use">🚀 How to Use</a>
</p>

---

[![Notion](https://img.shields.io/badge/Notion-Tech%20Challenge%20Phase%201-000000?style=for-the-badge&logo=notion)](https://taisprestes01.notion.site/Desafio-1-1e4d3ce3193b8016a48ad266e08f6ccc)

## ✨ Key Features

- **User Management**: register with name, email & password (complexity rules)  
- **Authentication & Authorization**: JWT-based; roles `User` (library access) and `Admin` (manage users/games)  
- **Game Catalog**: CRUD operations for digital games (title, description, price)  
- **User Library**: purchase and list acquired games (many-to-many relation)  
- **Data Persistence**: EF Core with migrations (optional MongoDB/Dapper)  
- **API Documentation**: Swagger UI for all endpoints  
- **Error Handling & Logging**: global middleware with structured logs  
- **Automated Testing**: unit tests with xUnit; at least one module with TDD/BDD  

## 🧠 Technical Requirements

- .NET 8  
- C# 12  
- ASP.NET Core Web API (Minimal API or MVC Controllers)  
- Entity Framework Core (or Dapper / MongoDB optional)  
- JWT Authentication  
- Swagger (Swashbuckle)  
- SQL Server or PostgreSQL  
- xUnit + FluentAssertions  
- (Optional) SpecFlow for BDD scenarios  

## 🏗️ Project Structure (DDD)
This project follows Domain-Driven Design (DDD) principles with the following layered structure:

```
src/
├── Application/
│   └── Services, DTOs, Interfaces
├── Domain/
│   └── Entities, Enums, ValueObjects, Interfaces
├── Infrastructure/
│   └── Repositories, Persistence (EF Core), Context
├── WebAPI/
│   └── Controllers, Middlewares, Authentication, Swagger
Tests/
    └── Unit tests using xUnit and FluentAssertions
```
</details>

<h1 align="center">
  <br>
    <img src="https://github.com/user-attachments/assets/620f926b-da49-41d0-8a51-b1b1214f4c4b" alt="ContactRegionator Logo" width="180">
  <br>
  FIAP Cloud Games (FCG)
  <br>
</h1>

<h4 align="center">
Aplicativo desenvolvido como parte do Tech Challenge da Fase 1 — Pós-graduação <a href="https://www.fiap.com.br/" target="_blank">FIAP</a> em Arquitetura de Software .NET com Azure.
</h4>

<p align="center">
  <a href="#✨-principais-características">✨ Principais Características</a> •
  <a href="#🧠-requisitos-técnicos">🧠 Requisitos Técnicos</a> •
  <a href="#🏗️ -ddd">🏗️ Estrutura do Projeto (DDD)</a> •
  <a href="#🚀-como-usar">🚀 Como Usar</a>
</p>

---

[![Notion](https://img.shields.io/badge/Notion-Tech%20Challenge%20Fase%201-000000?style=for-the-badge&logo=notion)](https://taisprestes01.notion.site/Desafio-1-1e4d3ce3193b8016a48ad266e08f6ccc)

## ✨ Principais Características

- **Gerenciamento de Usuários**: cadastro com nome, e-mail e senha segura  
- **Autenticação & Autorização**: JWT; papéis `User` (acesso à biblioteca) e `Admin` (gestão)  
- **Catálogo de Jogos**: CRUD de jogos digitais (título, descrição, preço)  
- **Biblioteca de Usuário**: compra e listagem de jogos adquiridos  
- **Persistência de Dados**: EF Core com migrations (opcional MongoDB/Dapper)  
- **Documentação**: Swagger UI para todos os endpoints  
- **Tratamento de Erros & Logs**: middleware global com logs estruturados  
- **Testes Automatizados**: xUnit; ao menos um módulo em TDD/BDD  

## 🧠 Requisitos Técnicos

- .NET 8  
- C# 12  
- ASP.NET Core Web API (Minimal API ou MVC Controllers)  
- Entity Framework Core (ou Dapper / MongoDB opcional)  
- Autenticação JWT  
- Swagger (Swashbuckle)  
- SQL Server ou PostgreSQL  
- xUnit + FluentAssertions  
- (Opcional) SpecFlow para cenários BDD  

## 🏗️ Estrutura do Projeto (DDD)
Este projeto segue os princípios de Domain-Driven Design (DDD), com a seguinte estrutura:
```
src/
├── Application/
│   └── Serviços, DTOs, Interfaces
├── Domain/
│   └── Entidades, Enums, ValueObjects, Interfaces
├── Infrastructure/
│   └── Repositórios, Contexto EF Core, Persistência
├── WebAPI/
│   └── Controllers, Middlewares, Autenticação, Swagger
Tests/
    └── Testes unitários com xUnit e FluentAssertions
```

## 🚀 Como Usar

```
1. Clone o repositório:
   git clone https://github.com/Net-Vanguard/fase-01.git
   cd fase-01

2. Rode a API

3. Acesse o Swagger

4. Crie um usuário no endpoint /api/Users por exemplo:
   {
     "name": "Fulano",
     "email": "fulano@email.com",
     "password": "SenhaForte123!",
     "role": "0"
   }

   📌 role = 0 para Usuário comum  
   📌 role = 1 para Administrador

5. Faça login com seu usuário no endpoint /api/Auth.

6. Copie o token JWT retornado e clique em "Authorize" no topo do Swagger.
   Cole o token e confirme.

7. Agora você pode acessar todas as rotas protegidas da API.
```
