#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Userservice.WebApi/Userservice.WebApi.csproj", "Userservice.WebApi/"]
COPY ["Userservice.DataStore/Userservice.DataStore.csproj", "Userservice.DataStore/"]
COPY ["Userservice.Models/Userservice.Models.csproj", "Userservice.Models/"]
RUN dotnet restore "./Userservice.WebApi/Userservice.WebApi.csproj"
COPY . .
WORKDIR "/src/Userservice.WebApi"
RUN dotnet build "./Userservice.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Userservice.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Userservice.WebApi.dll"]