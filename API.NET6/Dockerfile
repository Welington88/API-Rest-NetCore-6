#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["API.NET6/API.NET6.csproj", "API.NET6/"]
RUN dotnet restore "API.NET6/API.NET6.csproj"
COPY . .
WORKDIR "/src/API.NET6"
RUN dotnet build "API.NET6.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "API.NET6.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API.NET6.dll"]
