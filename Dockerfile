# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.sln .
COPY FactoryGenerateSudoku/*.csproj ./Sudoguru/FactoryGenerateSudoku/
COPY GenerateSudoku/*.csproj ./Sudoguru/GenerateSudoku/
COPY ILogic/*.csproj ./Sudoguru/ILogic/
COPY ISudoku/*.csproj ./Sudoguru/ISudoku/

RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:3.1
WORKDIR /Sudoguru
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Sudoguru.dll"]