# Sistema de Rastreabilidade de Peças (TrackPro)

**Status do Projeto: Concluído ✅**

## Sobre o Projeto

Este projeto consiste em um sistema robusto para rastreabilidade de peças industriais. Ele permite o cadastro, movimentação entre estações, histórico detalhado e validações de processo obrigatórias.
Toda a lógica de negócio foi desenvolvida em uma API RESTful utilizando .NET.

## Funcionalidades do Backend (.NET)

* ✅ **CRUD completo** para as entidades `Peça` e `Estação`.
* ✅ **Validação do fluxo de processo obrigatório**: Recebimento → Montagem → Inspeção Final.
* ✅ **Registro de movimentações** com validações que impedem pulos ou retrocessos entre estações.
* ✅ **Atualização automática do status** da peça com base em sua posição no fluxo.
* ✅ **Consulta ao histórico completo** de movimentações por peça.
* ✅ **Validação de regras de negócio**, como unicidade do código da peça e sequência correta das estações.
* ✅ **Cobertura de testes unitários** utilizando xUnit para as regras críticas do domínio.

## Arquitetura e Tecnologias Utilizadas

O backend foi construído com foco em boas práticas, alta coesão e separação clara de responsabilidades.

### Tecnologias e Estrutura

* **Framework:** .NET 8 (compatível com 6+)
* **Arquitetura:** Clean Architecture, com as camadas:

  * `Domain`
  * `Application`
  * `Infrastructure`
  * `API`
* **Padrões de Projeto:**

  * **CQRS com MediatR** para separação de comandos e queries.
  * **Repository Pattern** para abstração de acesso a dados.
* **Banco de Dados:** SQLite com Entity Framework Core e Migrations.
* **Testes Automatizados:** xUnit + Moq.
* **Tratamento de Erros:** Middleware global para exceções e respostas padronizadas.

## Como Executar o Backend

### Pré-requisitos

* [.NET SDK 8](https://dotnet.microsoft.com/download)
* Ferramenta `dotnet-ef` instalada globalmente:

  ```bash
  dotnet tool install --global dotnet-ef
  ```

### Passos

```bash
# Navegue até a pasta do backend
cd trackpro_back

# Restaure os pacotes
dotnet restore

# Inicie o servidor
dotnet run --project API/TrackPro.API
```

> A API estará disponível em algo como: `https://localhost:7123`.

## Rodar os Testes

```bash
dotnet test
```

### Resultado Esperado

```bash
Resumo do teste: total: 4; falhou: 0; bem-sucedido: 4; ignorado: 0; duração: 2,7s
Construir êxito em 7,7s
```

## Swagger

Acesse a documentação da API pelo Swagger em:

```
http://localhost:5147/swagger/index.html
```
