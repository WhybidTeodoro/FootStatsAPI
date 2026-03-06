# ⚽ FootStats API

![.NET](https://img.shields.io/badge/.NET-8.0-purple)
![C#](https://img.shields.io/badge/C%23-Backend-blue)
![Status](https://img.shields.io/badge/status-em%20desenvolvimento-yellow)

API REST desenvolvida em **ASP.NET Core (.NET 8)** com foco em aprendizado de desenvolvimento backend.

O projeto permite que usuários se cadastrem, criem times de futebol, adicionem jogadores, registrem partidas e acompanhem estatísticas básicas de desempenho.

---

# 🎯 Objetivo do Projeto

Este projeto foi desenvolvido como parte do meu processo de aprendizado em **desenvolvimento backend com .NET**, com foco em:

- Entender como funciona uma **API REST**
- Praticar **C# e ASP.NET Core**
- Trabalhar com **banco de dados relacional**
- Implementar **autenticação com JWT**
- Aplicar conceitos de **arquitetura de software**

---

# 🧱 Arquitetura do Projeto

O projeto foi organizado utilizando uma estrutura inspirada em **Clean Architecture**, separando responsabilidades em diferentes camadas:

- FootStats.API → Controllers e configuração da aplicação
- FootStats.Application → Regras de negócio, DTOs e validações
- FootStats.Domain → Entidades do domínio
- FootStats.Infrastructure→ Acesso a dados e repositórios

---

# 🔧 Tecnologias Utilizadas

- **ASP.NET Core (.NET 8)**
- **C#**
- **Entity Framework Core**
- **MySQL**
- **JWT Authentication**
- **FluentValidation**
- **Swagger (OpenAPI)**
- **xUnit** (Testes unitários)
- **FluentAssertions**

---

# 🚀 Funcionalidades Implementadas

### 👤 Usuários
- Cadastro de usuário
- Login com autenticação JWT
- Acesso protegido por autenticação

### ⚽ Times
- Criar time
- Atualizar time
- Listar times
- Remover time

### 🧑‍🤝‍🧑 Jogadores
- Adicionar jogadores a um time
- Atualizar dados do jogador
- Remover jogador
- Listar jogadores por time

### 🏟️ Partidas
- Registrar partidas de um time
- Atualizar partidas
- Remover partidas
- Listar partidas de um time

### 📊 Estatísticas
As estatísticas são calculadas automaticamente:

- Partidas disputadas
- Gols marcados
- Assistências
- Vitórias, derrotas e empates
- Gols feitos e sofridos

### 📄 Paginação e Ordenação
Endpoints de listagem suportam:

- **Paginação**
- **Ordenação por campos**
- Controle de tamanho de página

Exemplo:

GET /api/team?pageNumber=1&pageSize=10&sortBy=name&direction=asc

---

# 🧪 Testes Unitários

O projeto possui testes unitários utilizando:

- **xUnit**
- **FluentAssertions**

Atualmente existem testes para:

- Paginação (`PaginationParameters`)
- Cálculo de páginas (`PagedResult`)
- Parser de ordenação (`SortParametersParser`)
- Validators de paginação e ordenação

---

# 🛠️ Como Executar o Projeto

1️⃣ Clone o repositório

2️⃣ Configure a **connection string do MySQL**

3️⃣ Execute as migrações do Entity Framework

4️⃣ Execute a aplicação

5️⃣ Acesse o Swagger

---

# 📌 Observações

- Projeto desenvolvido com **foco em aprendizado**
- Não possui frontend (apenas API)
- Estrutura pensada para facilitar aprendizado de backend com .NET

---

# 👨‍💻 Autor

**Whybid Teodoro**

Projeto desenvolvido para estudo e portfólio, com foco em evolução profissional como **desenvolvedor backend .NET**.
