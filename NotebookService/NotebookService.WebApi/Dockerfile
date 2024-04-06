#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 12700
EXPOSE 12700

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["NotebookService.WebApi/NotebookService.WebApi.csproj", "NotebookService.WebApi/"]
COPY ["NotebookService.DataStore/NotebookService.DataStore.csproj", "NotebookService.DataStore/"]
COPY ["NotebookService.Models/NotebookService.Models.csproj", "NotebookService.Models/"]
RUN dotnet restore "./NotebookService.WebApi/NotebookService.WebApi.csproj"
COPY . .
WORKDIR "/src/NotebookService.WebApi"
RUN dotnet build "./NotebookService.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./NotebookService.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NotebookService.WebApi.dll"]