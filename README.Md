[![NPM](https://img.shields.io/npm/l/react)](https://github.com/devsuperior/sds1-wmazoni/blob/master/LICENSE) 

## Rodando aplicação localmente

1. Clonar o repositório:

   ```bash
    git clone https://github.com/progmateus/foods-backend.git
   ```

2. Navegar até o diretório do projeto::

   ```bash
    cd foods-backend
   ```

3. Instalar dependências

   ```bash
    dotnet restore
   ```

4. Rodar container Docker
   ```bash
    docker compose up
   ```

5. Fazer login no SQL Server
   ```bash
    User: sa
    Password: 1q2w3e4r@#$
    Port: 1433
   ```

6. Criar banco no SQL Server
   ```bash
    "create database foods"
   ```

7. Rodar migrations
    ```bash
      dotnet ef --project src/data --startup-project src/http database update
    ```

8. Alterar Endereço de ip da aplicação ( http > Properties > launchSettings.json )
    ```bash
        "applicationUrl": "http://localhost:5062;http://[seu-ip]:5062",
    ```

9. Entrar no diretório http
    ```bash
        cd src/http
    ```

10. Rodar a aplicação
    ```bash
        dotnet run
    ```


## Funcionalidades
 - &check; Seed para gerar os Grupos automaticamente ao iniciar a aplicação
 - &check; Job para monitorar os alimentos a cada 5 minutos
 - &check; Filas e eventos para gerar os alimentos (1 para cada página)
 - &check; Conteinerização utilizando Docker
 - &check; Listagem de Alimentos com paginação
 - &check; Buscar alimentos pelo nome

## Tecnologias utilizadas
- .NET
- ASP.NET Core
- Entity Framework
- RabbitMq (Filas)
- MassTransit (Filas)
- Quartz (Cron Jobs)
- AngleSharp (Wrapper)
- Design Pattern SQRS
- Repository Pattern
- SOLID

# Autor

Mateus Lopes

https://www.linkedin.com/in/progmateus