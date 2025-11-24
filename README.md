# 💰 Controle de Despesas

Sistema completo para gerenciamento de despesas pessoais, permitindo registrar, listar, editar e excluir despesas, além de aplicar filtros e paginação.  
O projeto utiliza **Angular 17** no front-end e **.NET 8 (ASP.NET Web API)** no back-end, seguindo boas práticas de arquitetura.

---

## 🚀 Tecnologias Utilizadas

### **Frontend**
- Angular 17
- TypeScript
- PrimeNG
- HTML / SCSS

### **Backend**
- .NET 8 — ASP.NET Core Web API
- Entity Framework Core
- AutoMapper
- FluentValidation
- MySQL
- Teste de Unidade e de Integração
- Implementação de JWT e Refresh Token para segurança de autenticação



## 📦 Funcionalidades

### 🔹 **Despesas**
- Cadastrar despesas
- Editar despesas
- Excluir despesas
- Visualização detalhada
- Filtros:
  - Descrição
  - Data inicial
  - Data final
- Paginação e busca dinâmica
- Botão **Limpar Filtro**

### 🔹 **Metas**
- Cadastro de meta mensal de despesas

### 🔹 **Receita**
- Cadastrar receita
- Editar receita
- Excluir receita
- Visualização detalhada
- Filtros:
  - Descrição
  - Data inicial
  - Data final
- Paginação e busca dinâmica
- Botão **Limpar Filtro**

### 🔹 **Dashboard**
- Exibi o total das receitas do usuario
- Exibi o total das despesas do usuario
- Exibi a meta do mês
- Exibi os tres ultimas receitas e despesas cadastradas  




---

---

## 🏗️ Arquitetura Backend

O backend segue uma arquitetura limpa, organizada em camadas, aplicando princípios de **SOLID** e **Domain-Driven Design (DDD)**.

### 🔹 **Camadas**
- **Domain** → Contém entidades, agregados, interfaces de repositório, regras de negócio.
- **Application** → Contém DTOs, serviços de aplicação, AutoMapper, casos de uso e validações.
- **Infrastructure** → Implementações concretas (repositórios, EF Core, banco de dados),Geração do Token e Refresh Token.
- **API** → Controllers, autenticação, validações.
- **Tests** → Testes Unitários e de Integração (xUnit).


---

## 🧱 Arquitetura Frontend

O front-end foi desenvolvido em **Angular 17**

### 🔹 Estrutura do Frontend
- **pages** → telas principais (Despesas, Receitas, Metas, Dashboard)
- **services** → comunicação com a API
- **models/dtos** → modelos e tipos usados na aplicação
- **interceptors** → autenticação via JWT
- **shared** → enums, pipes

---

## 🔐 Autenticação

O projeto utiliza uma camada de segurança baseada em:

- **JWT (Bearer Token)**
- **Refresh Token**
- **Autorização por usuário**


Fluxo:
1. Usuário faz login  
2. API retorna **Token + Refresh Token**  
3. O front salvo o token no *LocalStorage*  
4. Um **Interceptor** anexa o token a cada requisição  
5. Se expirar, o Refresh Token gera um novo automaticamente  

---

## 🧪 Testes Automatizados

Foram implementados tanto **testes unitários** quanto **testes de integração**.

### 🔹 Testes Unitários
- Services
- Validações
- Regras de domínio

### 🔹 Testes de Integração
- Controllers






