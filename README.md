## Hahn Assignment

### Use Command

#### build docker images
docker-compose up --build

#### enter the db container command line
docker exec -it sqlserver_c /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "Medlmz@2016"
#### inside container execute
create database hahndb
#### enter the backend image command line
docker exec -it hahnsimback_c sh
#### run
dotnet ef database update

sorry I was in rush! I'll make running the project more practical soon

