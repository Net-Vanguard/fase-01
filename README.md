
<h1 align="center">
  <br>
  <img src="https://github.com/user-attachments/assets/620f926b-da49-41d0-8a51-b1b1214f4c4b" alt="ContactRegionator Logo" width="180">
  <br>
  ContactRegionator
  <br>
</h1>
 
<h4 align="center">
Aplicativo desenvolvido como parte do Tech Challenge da Fase 1 — Pós-graduação FIAP em Arquitetura de Software .NET com Azure.
</h4>

<p align="center">
  <a href="#-principais-características">🎯 Objetivo</a> •
  <a href="#Requisitos-Técnicos">🛠 Tecnologias</a> •
  <a href="#-requisitos-funcionais">🤝 Como Participar</a> •
  <a href="#estrutura-do-projeto">📚 Conteúdos por Fase</a>
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
