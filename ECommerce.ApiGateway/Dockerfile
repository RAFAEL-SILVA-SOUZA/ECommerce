#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ECommerce.ApiGateway/ECommerce.ApiGateway.csproj", "ECommerce.ApiGateway/"]
RUN dotnet restore "ECommerce.ApiGateway/ECommerce.ApiGateway.csproj"
COPY . .
WORKDIR "/src/ECommerce.ApiGateway"
RUN dotnet build "ECommerce.ApiGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ECommerce.ApiGateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ECommerce.ApiGateway.dll"]