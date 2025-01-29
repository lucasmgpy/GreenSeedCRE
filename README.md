# 📘 Documentação do Projeto GreenSeed

## 📌 Índice
- [Introdução](#introdução)
- [Estrutura do Projeto](#estrutura-do-projeto)
- [Gestão de Utilizadores](#gestão-de-utilizadores)
- [Gestão de Categorias e Produtos](#gestão-de-categorias-e-produtos)
- [Gestão de Encomendas](#gestão-de-encomendas)
- [Upload de Fotos](#upload-de-fotos)
- [Desafios](#desafios)
- [Banco de Dados (Azure SQL Database)](#banco-de-dados-azure-sql-database)
- [Testes](#testes)
- [Deploy no Azure](#deploy-no-azure)
- [Melhorias Futuras](#melhorias-futuras)
- [Tecnologias Utilizadas](#tecnologias-utilizadas)

---

## 🌱 Introdução
O **GreenSeed** é um sistema desenvolvido com **ASP.NET Core MVC**, onde podemos gerir utilizadores, produtos, encomendas e desafios. A aplicação é implantada no **Azure** e conta com funcionalidades como **armazenamento de imagens no Blob Storage** e **fila de mensagens para processamento assíncrono de encomendas**.

---

## 🏗️ Estrutura do Projeto
```
Solution 'GreenSeed' (3 de 3 projetos)
├── GitHub Actions
├── GreenSeed
│   ├── Areas
│   │   ├── Admin
│   │   ├── Identity
│   ├── Controllers
│   ├── Data
│   ├── Models
│   ├── Services
│   ├── Views
│   ├── wwwroot
│   ├── Program.cs
│   ├── appsettings.json
├── GreenSeed.Tests
│   ├── Controllers
│   ├── Integration
│   ├── Services
├── TransportadoraApp
│   ├── Data
│   ├── Models
│   ├── Program.cs
```
Esta representação da estrutura do projeto auxilia a compreensão dos módulos e organização da solução.

---

## 🔑 Gestão de Utilizadores

### **ApplicationUser.cs**
Modelo de usuário baseado no **ASP.NET Identity**, contendo:
- Propriedades padrão (UserName, Email, PasswordHash, etc.).
- **ICollection<Order> Orders**: Relacionamento com pedidos.

### **SeedData.cs**
- Inicializa dados básicos no banco de dados.
- Cria os papéis `Admin` e `User`.
- Cria um usuário administrador padrão.
- ⚠️ **Nota:** O email e a senha estão hardcoded apenas para testes. O correto seria armazenar credenciais no **Azure Key Vault** ou variáveis de ambiente.

### **UsersController.cs**
Gerencia a lista de usuários (disponível apenas para `Admin`):
- `Index()`: Lista todos os usuários cadastrados.
- `Edit()`: Edita email e roles atribuídos.
- `Delete()`: Exclui usuários.

---

## 🏷️ Gestão de Categorias e Produtos

### **CategoryController.cs**
- CRUD para categorias usando um **repositório genérico**.

### **ProductController.cs**
- CRUD para produtos, incluindo **upload de imagens**.
- 🔄 Implementação alternativa de `AddEdit` para **evitar repetição de código**.

⚠️ **Melhoria pendente:**
- **Armazenamento de imagens no Azure Blob Storage** em vez de wwwroot/images.
- **Categoria e Produto deveriam estar na área Admin** para maior organização.

---

## 📦 Gestão de Encomendas

### **OrderController.cs**
- Permite que usuários adicionem produtos ao carrinho, finalizem compras e acompanhem o status dos pedidos.
- **Usa Azure Storage Queue** para processamento assíncrono.

### **QueueProcessor.cs** (TransportadoraApp)
- **Lê mensagens da fila do Azure**, processa as encomendas e **atualiza o status no banco de dados**.
- Atualiza pedidos como "Entregue".

---

## 🚀 Deploy no Azure

### **Criando Web App**
1. Criar **Azure Web App** (`greenseed-webapp`).
2. Configurar string de conexão no **appsettings.json**.

### **Configurar CI/CD com GitHub Actions**
1. Configurar **GitHub Actions** (`.github/workflows/azure-webapp.yml`).
2. A cada push na branch `master`, o deploy é acionado.

---

## 📈 Melhorias Futuras
✅ **Implementar Azure Key Vault** para credenciais seguras.
✅ **Usar Azure Container Registry + Docker**.
✅ **Adicionar suporte a Docker Compose**.
✅ **Segregar serviços em microsserviços**.

---

## 🛠️ Tecnologias Utilizadas
- **Back-end:** ASP.NET Core MVC, Entity Framework Core
- **Banco de Dados:** Azure SQL Database
- **Armazenamento:** Azure Blob Storage, Azure Storage Queues
- **Testes:** xUnit, Moq, Selenium WebDriver
- **CI/CD:** GitHub Actions
- **DevOps:** Azure Web App, Docker (futuro)

---

Este documento será atualizado conforme o projeto evolui! 🚀

