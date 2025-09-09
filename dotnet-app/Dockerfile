FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY dotnet-redis-app.csproj .
RUN dotnet restore dotnet-redis-app.csproj

COPY . .
RUN dotnet publish dotnet-redis-app.csproj -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app .
EXPOSE 8000
ENTRYPOINT ["dotnet", "dotnet-redis-app.dll"]