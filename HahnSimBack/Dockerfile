FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app


COPY HahnSimBack.csproj .
RUN dotnet restore

COPY . .

CMD ["dotnet", "watch", "run", "--project" , "HahnSimBack.csproj",  "--urls", "http://+:5103"]