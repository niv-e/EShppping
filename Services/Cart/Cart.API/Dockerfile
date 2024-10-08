#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Cart/Cart.API/Cart.API.csproj", "Services/Cart/Cart.API/"]
COPY ["Services/Cart/Cart.Infrastructure/Cart.Infrastructure.csproj", "Services/Cart/Cart.Infrastructure/"]
COPY ["Services/Cart/Cart.Application/Cart.Application.csproj", "Services/Cart/Cart.Application/"]
COPY ["Services/Cart/Cart.Core/Cart.Core.csproj", "Services/Cart/Cart.Core/"]
RUN dotnet restore "./Services/Cart/Cart.API/Cart.API.csproj"
COPY . .
WORKDIR "/src/Services/Cart/Cart.API"
RUN dotnet build "./Cart.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Cart.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cart.API.dll"]