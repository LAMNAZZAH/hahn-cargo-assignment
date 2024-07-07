FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

RUN dotnet tool install --global dotnet-ef
ENV PATH="${PATH}:/root/.dotnet/tools"

COPY HahnSimBack.csproj .
RUN dotnet restore

COPY . .

CMD ["dotnet", "watch", "run", "--project" , "HahnSimBack.csproj",  "--urls", "http://+:5103"]