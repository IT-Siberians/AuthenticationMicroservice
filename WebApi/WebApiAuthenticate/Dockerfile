# См. статью по ссылке https://aka.ms/customizecontainer, чтобы узнать как настроить контейнер отладки и как Visual Studio использует этот Dockerfile для создания образов для ускорения отладки.

# Этот этап используется при запуске из VS в быстром режиме (по умолчанию для конфигурации отладки)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# Этот этап используется для сборки проекта службы
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WebApi/WebApiAuthenticate/WebApiAuthenticate.csproj", "WebApi/WebApiAuthenticate/"]
COPY ["Application/Repositories.Abstractions/Repositories.Abstractions.csproj", "Application/Repositories.Abstractions/"]
COPY ["Domain/Domain.Entities/Domain.Entities.csproj", "Domain/Domain.Entities/"]
COPY ["Domain/Domain.ValueObjects/Domain.ValueObjects.csproj", "Domain/Domain.ValueObjects/"]
COPY ["Common/Common.Resources/Common.Helpers.csproj", "Common/Common.Resources/"]
COPY ["Domain/Domain.Helpers/Domain.Helpers.csproj", "Domain/Domain.Helpers/"]
COPY ["Application/Services.Abstractions/Services.Abstractions.csproj", "Application/Services.Abstractions/"]
COPY ["Application/Services.Contracts/Services.Contracts.csproj", "Application/Services.Contracts/"]
COPY ["Application/Services.Implementations/Services.Implementations.csproj", "Application/Services.Implementations/"]
COPY ["Infrastructure/PasswordHasher/PasswordHasher.csproj", "Infrastructure/PasswordHasher/"]
COPY ["Infrastructure/Repositories.Implementations/Repositories.Implementations.csproj", "Infrastructure/Repositories.Implementations/"]
COPY ["Infrastructure/EntityFramework/EntityFramework.csproj", "Infrastructure/EntityFramework/"]
RUN dotnet restore "./WebApi/WebApiAuthenticate/WebApiAuthenticate.csproj"
COPY . .
WORKDIR "/src/WebApi/WebApiAuthenticate"
RUN dotnet build "./WebApiAuthenticate.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Этот этап используется для публикации проекта службы, который будет скопирован на последний этап
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WebApiAuthenticate.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Этот этап используется в рабочей среде или при запуске из VS в обычном режиме (по умолчанию, когда конфигурация отладки не используется)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApiAuthenticate.dll"]