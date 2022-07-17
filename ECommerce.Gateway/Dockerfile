#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ECommerce.Gateway/ECommerce.Gateway.csproj", "ECommerce.Gateway/"]
RUN dotnet restore "ECommerce.Gateway/ECommerce.Gateway.csproj"
COPY . .
WORKDIR "/src/ECommerce.Gateway"
RUN dotnet build "ECommerce.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ECommerce.Gateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ECommerce.Gateway.dll"]