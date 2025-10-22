# Projeto Microservices .NET 8 - Em Desenvolvimento

Este projeto é um exemplo de **microserviços** usando **.NET 8**, **MassTransit**, **RabbitMQ** e **MySQL**, que está **ainda em desenvolvimento**.
Ele possui dois serviços principais:

* **Order.API** – serviço de pedidos, com banco de dados MySQL.
* **Payment.API** – serviço de pagamentos, comunicando-se via RabbitMQ.

O projeto está configurado para execução local ou em container Docker, com Swagger disponível para teste das APIs.

---

## Tecnologias

* .NET 8 (ASP.NET Core)
* MassTransit + RabbitMQ
* MySQL
* Docker / Docker Compose (opcional)
* AutoMapper
* Swagger (API Explorer)

---

## Estrutura do Projeto

```
/Order.API
    Order.API.csproj
    Program.cs
/Payment.API
    Payment.API.csproj
    Program.cs
/docker
    Dockerfile (opcional)
README.md
```

---

## Pré-requisitos

* [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
* [Docker Desktop](https://www.docker.com/products/docker-desktop) (opcional)
* RabbitMQ (local ou container)
* MySQL (local ou container)

---

## Configuração

### RabbitMQ

* Host padrão: `localhost`
* Usuário/pw: `guest/guest`
* Configuração pode ser alterada em `appsettings.json` ou variáveis de ambiente:

```json
{
  "RabbitMQ": {
    "Host": "localhost"
  }
}
```

### MySQL (Order.API)

* Configure a connection string no `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "server=localhost;port=3306;database=OrderDB;user=root;password=senha;"
  }
}
```

---

## Executando Localmente (Em Desenvolvimento)

1. Clone o repositório:

```bash
git clone <URL_DO_REPOSITORIO>
cd <REPOSITORIO>
```

2. Restaurar pacotes e build:

```bash
cd Order.API
dotnet restore
dotnet build

cd ../Payment.API
dotnet restore
dotnet build
```

3. Executar APIs em modo desenvolvimento:

```bash
cd Order.API
dotnet run
# Swagger: http://localhost:5104/swagger

cd ../Payment.API
dotnet run
# Swagger: http://localhost:5140/swagger
```

---

## Executando com Docker (Em Desenvolvimento)

1. Build da imagem:

```bash
docker build -t order-api ./Order.API
docker build -t payment-api ./Payment.API
```

2. Executar containers:

```bash
docker run -d -p 5104:80 --name order-api order-api
docker run -d -p 5140:80 --name payment-api payment-api
```

3. Acesse as APIs via Swagger:

* Order API: `http://localhost:5104/swagger`
* Payment API: `http://localhost:5140/swagger`

---

## Observações

* Projeto **ainda em desenvolvimento**, feito para **testar integração do RabbitMQ via MassTransit**.
* **HTTPS está desabilitado** no container para simplificação.
* MassTransit cria automaticamente filas para os consumidores configurados.
* Para produção, configurar HTTPS, variáveis de ambiente e banco de dados seguro.

---

## Contato

* Desenvolvedor: Gabriel Martinho
* E-mail: [gabriel@email.com](mailto:gabriel@email.com) (exemplo)
