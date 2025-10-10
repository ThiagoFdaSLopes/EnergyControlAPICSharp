# Use a imagem do SDK do .NET 8.0 como base de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Crie um usuário não-root. O ID 1000 é um padrão comum.
RUN addgroup --gid 1000 appgroup && \
    adduser --uid 1000 --gid 1000 --disabled-password --gecos "" appuser

# Copie os arquivos de projeto e restaure as dependências
COPY ["EnergyControlAPI/EnergyControlAPI.csproj", "EnergyControlAPI/"]
RUN dotnet restore "EnergyControlAPI/EnergyControlAPI.csproj"
COPY . .
WORKDIR "/src/EnergyControlAPI"
RUN dotnet build "EnergyControlAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publique a aplicação
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "EnergyControlAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Estágio final usando a imagem de runtime do ASP.NET
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Crie o mesmo usuário não-root no estágio final
RUN addgroup --gid 1000 appgroup && \
    adduser --uid 1000 --gid 1000 --disabled-password --gecos "" appuser

# Crie o diretório de dados e defina suas permissões
RUN mkdir /app/data && \
    chown -R appuser:appgroup /app/data

COPY --from=publish /app/publish .

# Garanta que todos os arquivos do app pertençam ao novo usuário
RUN chown -R appuser:appgroup /app

# Mude para o usuário não-root
USER appuser

ENTRYPOINT ["dotnet", "EnergyControlAPI.dll"]