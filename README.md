
---
<div align="center">

# 📚 Sistema Diário de Leitura

API REST desenvolvida em .NET 8 + Entity Framework Core para registrar livros, autores, gêneros e acompanhar o progresso de leitura dos usuários (início, fim, status, notas e comentários).

<sub>Projeto criado para fins de estudo e evolução prática em ASP.NET Core / EF Core.</sub>

</div>

---

## 🚀 Objetivos do Projeto
Fornecer uma base simples e extensível para um diário de leitura que permita:

* Cadastrar e consultar usuários
* Registrar livros com múltiplos autores e gêneros (relacionamentos N:N)
* Definir status de leitura (Ex.: Lendo, Concluído, Pausado, Abandonado)
* Acompanhar sessões de leitura (data início/fim, nota e comentário)
* Servir como laboratório para práticas com EF Core, Migrations, relacionamentos e boas práticas de API

---

## 🧱 Stack Tecnológica
* **Linguagem:** C# (.NET 8)
* **Framework Web:** ASP.NET Core Minimal Hosting (Controllers)
* **ORM:** Entity Framework Core + Pomelo.EntityFrameworkCore.MySql
* **Banco de Dados:** MySQL
* **Documentação:** Swagger / OpenAPI (ambiente Development)
* **Serialização JSON:** System.Text.Json (ReferenceHandler.IgnoreCycles)

Link Modelagem de Dados (MER): https://www.drawdb.app/editor?shareId=8eeebd1b54228e64d495cf2b08f29579

---

## 🗃️ Modelos Principais
| Entidade | Descrição | Campos Relevantes |
|----------|-----------|-------------------|
| Usuario | Usuários do sistema | Id, Nome, Email, Senha (hash futuramente), DataCadastro |
| Autor | Autores de livros | Id, Nome, DataNascimento, Biografia |
| Genero | Classificação de livros | Id, Nome (único) |
| Livro | Obras cadastradas | Id, Titulo, AnoPublicacao, Autores (N:N), Generos (N:N) |
| StatusLeitura | Tipos de status | Id, Nome (único) |
| Leitura | Registro de progresso | IdUsuario, IdLivro, IdStatusLeitura, Nota, Comentario, DataInicio, DataFim |

Relacionamentos:
* Livro ⇄ Autor (N:N) via tabela `livro_autor`
* Livro ⇄ Genero (N:N) via tabela `livro_genero`
* Leitura → Usuario (N:1)
* Leitura → Livro (N:1)
* Leitura → StatusLeitura (N:1)

---

## 🔌 Endpoints (Resumo)

Base URL (dev): `https://localhost:PORT/api`

| Recurso | Verbo | Caminho | Ação |
|---------|-------|--------|------|
| Usuario | GET | /usuario | Lista usuários |
| Usuario | GET | /usuario/{id} | Detalhe |
| Usuario | POST | /usuario | Cria |
| Usuario | PUT | /usuario/{id} | Atualiza |
| Usuario | DELETE | /usuario/{id} | Remove |
| Autor | GET | /autor | Lista autores |
| Autor | GET | /autor/{id} | Detalhe |
| Autor | POST | /autor | Cria |
| Autor | PUT | /autor/{id} | Atualiza |
| Autor | DELETE | /autor/{id} | Remove |
| Genero | GET | /genero | Lista gêneros |
| Genero | GET | /genero/{id} | Detalhe |
| Genero | POST | /genero | Cria |
| Genero | PUT | /genero/{id} | Atualiza |
| Genero | DELETE | /genero/{id} | Remove |
| Livro | GET | /livro | Lista livros |
| Livro | GET | /livro/{id} | Detalhe |
| Livro | POST | /livro | Cria |
| Livro | PUT | /livro/{id} | Atualiza |
| Livro | DELETE | /livro/{id} | Remove |
| Leitura | GET | /leitura | Lista leituras |
| Leitura | GET | /leitura/{id} | Detalhe |
| Leitura | GET | /leitura/usuario/{idUsuario} | Leituras por usuário |
| Leitura | POST | /leitura | Cria |
| Leitura | PUT | /leitura/{id} | Atualiza |
| Leitura | DELETE | /leitura/{id} | Remove |
| Leitura | DELETE | /leitura/usuario/{idUsuario} | Remove todas de um usuário |
| StatusLeitura | GET | /statusleitura | Lista status |
| StatusLeitura | GET | /statusleitura/{id} | Detalhe |
| StatusLeitura | POST | /statusleitura | Cria |
| StatusLeitura | PUT | /statusleitura/{id} | Atualiza |
| StatusLeitura | DELETE | /statusleitura/{id} | Remove |

Para experimentar, acesse o Swagger em: `https://localhost:PORT/swagger` (executando em Development).

---

## ⚙️ Como Executar Localmente

Pré-requisitos:
* .NET 8 SDK instalado
* MySQL 8+ em execução (ou compatível)

1. Clone o repositório:
```
git clone https://github.com/IcaroRaphael/sistema-diario-leitura.git
cd sistema-diario-leitura/SistemaDiarioLeitura
```
2. Configure a connection string em `appsettings.Development.json` (ou `appsettings.json`):
```json
"ConnectionStrings": {
	"DefaultConnection": "server=localhost;port=3306;database=sistema_diario_leitura;user=root;password=SUASENHA"
}
```
3. Criar banco e tabelas via scripts SQL fornecidos na raiz:
	 * `ScriptCriacao.sql`
	 * `ScriptInsercao(Test).sql` (dados de exemplo - opcional)
4. Executar a API:
```
dotnet run
```
5. Abrir o navegador: `https://localhost:PORT/swagger`

## 🔒 Melhorias Planejadas
* Autenticação e autorização (JWT / Identity)
* Hash de senha (BCrypt) e remoção de armazenamento em texto puro
* Inclusão de navegações (Autores/Generos) em endpoints de Livro
* DTOs e AutoMapper para evitar exposição direta de entidades
* Implementação do Front-End

---

## 🗂️ Estrutura Simplificada
```
SistemaDiarioLeitura/
 ├── Controllers/ (endpoints REST)
 ├── Models/ (entidades EF Core)
 ├── Context/ (AppDbContext)
 ├── Properties/ (launchSettings)
 ├── appsettings*.json (configurações)
 └── Program.cs (bootstrap da aplicação)
```

---

## 🧩 Decisões Técnicas
* Uso de `ReferenceHandler.IgnoreCycles` para evitar loops em serialização de coleções N:N
* Relacionamentos muitos-para-muitos configurados via Fluent API no `OnModelCreating`
* Chaves únicas para `Genero.Nome` e `StatusLeitura.Nome` conforme regras de domínio
* Controllers diretos (sem serviços) para simplicidade inicial

---

## 📄 Licença
Distribuído sob a licença MIT. Veja `LICENSE` para mais detalhes.

---

> Obrigado por conferir este projeto! Sugestões são bem-vindas. 😊
