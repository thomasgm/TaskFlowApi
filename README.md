# AssetFlow API - Portfólio Técnico

Este repositório contém uma aplicação **ASP.NET Core Web API** desenvolvida em **.NET 8** e **C#**. 
O objetivo deste documento é fornecer a você (recrutador ou engenheiro) uma visão aprofundada de **como** a arquitetura foi desenhada, **por que** certas decisões foram tomadas e as **funções** de cada camada da aplicação.

Obs>: README criado com IA.

---

## 🏛 Arquitetura e Padrões de Projeto (Design Patterns)

A aplicação segue uma arquitetura em camadas concisa, focada em **desacoplamento**, **manutenibilidade** e na separação de responsabilidades (Clean Code / SOLID).

### 1. Controllers (`AssetsController.cs`) - *A Porta de Entrada*
A camada de Controller atua estritamente como o "Maestro" do tráfego HTTP.
- **Responsabilidade:** Receber requisições (GET, POST, PUT, DELETE), delegar as operações para a camada de Serviço e formatar a resposta no padrão REST.
- **Boas Práticas Adotadas:**
  - Uso de status codes adequados (`200 OK`, `201 Created` retornando a rota completa no `location` header, `204 NoContent` para fluxos de delete/put e `404 NotFound`).
  - **Zero regras de negócio:** O Controller não faz validações complexas, ele apenas responde aos resultados e exceções geradas pelo domínio/serviços.

### 2. Service Layer (`AssetService.cs` e `IAssetService`) - *A Regra de Negócio*
A camada de Serviço onde a o "coração" financeiro do projeto bate.
- **Responsabilidade:** Validações de domínio. Por exemplo, antes de um Ativo ser salvo no banco, o Serviço valida se o `Price` é maior que 0 e se o `Name` possui conteúdo.
- **Vantagem:** Isolar a regra de negócio da Web (Controller) e do Banco (Repository). Isso facilita massivamente a criação de **Testes Unitários**, pois testamos apenas a lógica de negócio sem precisar de um banco de dados real rodando.

### 3. Repository Pattern (`AssetRepository.cs` e `IAssetRepository`) - *O Acesso a Dados*
Em vez de permitir que o Serviço converse diretamente com o banco de dados (Entity Framework), adicionamos uma camada de abstração (Repositório).
- **Responsabilidade:** Toda leitura, escrita, atualização e exclusão (`CRUD`) passa por aqui, usando as classes do Entity Framework Core (`AddAsync`, `ToListAsync`, etc).
- **Vantagem:** O `AssetService` não sabe (e não se importa) de onde os dados vêm. Se no futuro o banco relacional SQLite for trocado por um NoSQL ou por requisições para outra API, a mudança ocorrerá **exclusivamente** no Repositório. O resto do projeto fica intocado.

### 4. Entidades e Mapeamento ORM (`Models` e `ApplicationDbContext`)
O banco de dados escolhido é o **SQLite** local (por portabilidade), intermediado através da ferramenta **Entity Framework Core**.
- **Modelos (`Asset.cs`, `Category.cs`):** Classes ricas no C# que o EF traduz diretamente para tabelas relacionais. Utilizam *Data Annotations* do C# (`[StringLength]`, `[Required]`) para as restrições no banco.
- **Seed Data (`HasData`):** Dentro da configuração do `ApplicationDbContext`, utilizamos a função fluente do EF Core para popular categorias iniciais ("Hardware", "Software"). 

### 5. Injeção de Dependências - DI (`Program.cs`)
O fluxo de dependências é completamente gerenciado pelo contêiner do ASP.NET.
- As integrações entre Controllers -> Services -> Repositories e DbContext estão utilizando injeções em construtores acoplados via interfaces (`AddScoped`). Isso cria o tempo de vida perfeito para operações web (uma instância por requisição).

---

## 🚀 Desafios e Refinamentos Técnicos Implementados

Para demonstrar conhecimento aprofundado, resolvi problemas clássicos de retornos de APIs em .NET:

1. **Serialização Limpa (Omission de Nulls):** 
   - No `Program.cs`, foi configurado no `System.Text.Json` o `DefaultIgnoreCondition = WhenWritingNull`. Isso otimiza o peso da banda da API, impedindo que campos sem preenchimento (como Propriedades de Navegação não engatilhadas por um join) poluam o JSON devolvido ao usuário ou ao front-end.
2. **Schema do Swagger Otimizado:**
   - Foi utilizado a anotação bidirecional do atributo `[JsonIgnore]` diretamente nas Propriedades de Navegação do modelo `Asset.cs`. Isso garante que requisições `POST` documentadas no ambiente Swagger fiquem cirurgicamente limpas de ruídos, solicitando ao consumidor apenas as Chaves Estrangeiras reais para inserção, de modo que o Binding Form do ASP.NET não recuse as chamadas na porta de entrada.

---

## 🧪 Testes de Qualidade (`AssetFlow.Tests`)
O ambiente dispõe de um projeto exclusivo para Testes de Unidade, alimentado por framerows como `xUnit`. Eles atestam a resiliência das validações de regras construídas na `Service Layer`.

## ⚙️ Instruções de Execução

1. Certifique-se de possuir o SDK local do .NET 8;
2. Clone o repositório:
```bash
git clone https://github.com/SeuUsuario/AssetFlow.git
cd AssetFlow/AssetFlow.API
```
3. Execute o comando Restore/Run:
```bash
dotnet run
```
4. A documentação mapeada da API estará disponível através de **Swagger** na rota HTTP/HTTPS exposta pelo console host (Adicione o `/swagger` na URL do seu navegador).
