<details>
<summary>English version 🇺🇸</summary>

<h1 align="center">
  <br>
  <img src="https://github.com/user-attachments/assets/620f926b-da49-41d0-8a51-b1b1214f4c4b" alt="ContactRegionator Logo" width="180">
  <br>
  ContactRegionator
  <br>
</h1>

<h4 align="center">
Application developed as part of the Phase 1 Tech Challenge — <a href="https://www.fiap.com.br/" target="_blank">FIAP</a> Postgraduate Program in .NET Software Architecture with Azure.
</h4>

<p align="center">
  <a href="#-key-features">✨ Key Features</a> •
  <a href="#-technical-requirements">🧠 Technical Requirements</a> •
  <a href="#-how-to-use">🤝 How to Use</a>
</p>

---

[![Notion](https://img.shields.io/badge/Notion-Tech%20Challenge%20Phase%201-000000?style=for-the-badge&logo=notion)](https://fuschia-runner-0d2.notion.site/Tech-Challenge-Fase-1-1c546151da8e806a9865ddb08cecb4a3)

## ✨ Key Features

- Register contacts with DDD (area code)
- Query contacts with DDD filter
- Update and delete contacts
- Data persistence using EF Core or Dapper
- In-memory caching on contact query endpoint
- Strong validation rules
- Automated unit testing with xUnit
- Swagger API documentation

---

## 🧠 Technical Requirements

- .NET 8  
- C# 12  
- ASP.NET Core Web API  
- Dapper or Entity Framework Core  
- IMemoryCache  
- PostgreSQL or SQL Server  
- xUnit (testing)  
- Swagger (documentation)  

---

## 🚀 How to Use

To clone and run this application, you need [Git](https://git-scm.com) and the [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) installed.

```bash
# Clone this repository
git clone https://github.com/your-username/ContactRegionator.git

# Enter the project folder
cd ContactRegionator

# Restore packages
dotnet restore

# Run migrations (if using EF Core)
dotnet ef database update

# Run the API
dotnet run
```
</details>

<h1 align="center">
  <br>
  <img src="https://github.com/user-attachments/assets/620f926b-da49-41d0-8a51-b1b1214f4c4b" alt="ContactRegionator Logo" width="180">
  <br>
  ContactRegionator
  <br>
</h1>
 
<h4 align="center">
Aplicativo desenvolvido como parte do Tech Challenge da Fase 1 — Pós-graduação <a href="https://www.fiap.com.br/" target="_blank">FIAP</a> em Arquitetura de Software .NET com Azure.
</h4>

<p align="center">
  <a href="#-principais-características">✨ Principais Características</a> •
  <a href="#-requisitos-técnicos">🧠 Requisitos Técnicos</a> •
  <a href="#-como-usar">🤝 Como Usar</a> •
</p>

---
[![Notion](https://img.shields.io/badge/Notion-Tech%20Challenge%20Fase%201-000000?style=for-the-badge&logo=notion)](https://fuschia-runner-0d2.notion.site/Tech-Challenge-Fase-1-1c546151da8e806a9865ddb08cecb4a3)


## ✨ Principais Características

- Cadastro de contatos com DDD
- Consulta de contatos com filtro por DDD
- Atualização e exclusão de contatos
- Persistência com EF Core ou Dapper
- Cache em memória no endpoint de consulta
- Validações robustas
- Testes automatizados com xUnit
- Documentação com Swagger

---

## 🧠 Requisitos Técnicos

- .NET 8
- C# 12
- ASP.NET Core Web API
- Dapper ou Entity Framework Core
- IMemoryCache
- PostgreSQL ou SQL Server
- xUnit (testes)
- Swagger (documentação)

---

## 🚀 Como Usar

Para clonar e executar esta aplicação, você precisará do [Git](https://git-scm.com) e do [SDK do .NET 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) instalados.

```bash
# Clonar este repositório
git clone https://github.com/seu-usuario/ContactRegionator.git

# Entrar na pasta do projeto
cd ContactRegionator

# Restaurar pacotes
dotnet restore

# Rodar migrations (caso EF Core)
dotnet ef database update

# Executar a API
dotnet run
