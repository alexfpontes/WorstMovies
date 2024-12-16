# Worst Movies

API RESTful que possibilita a leitura de uma lista de filmes indicados e vencedores
da categoria Pior Filme do Golden Raspberry Awards.

# Requisito do sistema

1. Lê o arquivo CSV dos filmes que se encontra dentro da pasta Data, inseri os dados na base Sqlite assim que a aplicação é iniciada.


# Requisitos da API

1. Obtem o produtor com maior intervalo entre dois prêmios consecutivos, e o que
obteve dois prêmios mais rápido, seguindo a especificação de formato previamente definida


# Requisitos não funcionais do sistema

Web service RESTful implementado com base no nível 2 de maturidade de Richardson
Testes de integração que garantem que os dados obtidos estão de acordo com os dados fornecidos
Banco de dados Sqlite embarcado e em memória sem nescessidade de instalação externa


## Tecnologias Utilizadas

- .NET Core 8
- Banco de dados Sqlite


## Clonar o código

1. 
   ```bash
   git clone https://github.com/alexfpontes/WorstMovies.git


## Rodar a aplicação

1. 
   ```bash
   cd WorstMovieApi
   dotnet run
  

## Testar a aplicação

1. 
   ```bash
   cd WorstMovieTest
   dotnet test
