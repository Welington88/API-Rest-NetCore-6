#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["API.NET/API.NET.csproj", "API.NET/"]
RUN dotnet restore "API.NET/API.NET.csproj"
COPY . .
WORKDIR "/src/API.NET"
RUN dotnet build "API.NET.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "API.NET.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API.NET.dll"]
