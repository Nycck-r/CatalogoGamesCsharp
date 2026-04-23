# Catálogo de Jogos API

API REST em ASP.NET Core C# para gerenciar um Catálogo de Jogos. Utiliza Entity Framework Core (SQLite) e Repository Pattern. Conta com CRUD completo, relacionamentos e regras de negócio customizadas (descontos e validações). Projeto acadêmico.


### Jogos
* **GET** `/api/jogos` - Lista todos os jogos (com regra de desconto de 20% para jogos com 5+ anos aplicada).
* **GET** `/api/jogos/{id}` - Busca um jogo específico pelo ID.
* **POST** `/api/jogos` - Adiciona um novo jogo (Validações: Título obrigatório, Preço > 0, Ano não pode ser no futuro).
* **PUT** `/api/jogos/{id}` - Atualiza os dados de um jogo existente.
* **DELETE** `/api/jogos/{id}` - Remove um jogo do catálogo.