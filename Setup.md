docker run -v ~/docker --name sqlserver -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=1q2w3e4r@#$" -p 1433:1433 -d mcr.microsoft.com/mssql/server
docker run -e "ACCEPT_EULA=1" -e "MSSQL_SA_PASSWORD=1q2w3e4r@#$" -e "MSSQL_PID=Developer" -e "MSSQL_USER=SA" -p 1433:1433 -d --name=sqlserver mcr.microsoft.com/azure-sql-edge

dotnet ef  migrations add initial-migration --project "ECommerce.Order" --startup-project "ECommerce.Order" --context "ECommerce.Order.Infra.OrderDbContext"
dotnet ef database update --project "ECommerce.Order" --startup-project "ECommerce.Order" --context "ECommerce.Order.Infra.OrderDbContext"



dotnet ef  migrations add initial-migration --project "ECommerce.Catalog" --startup-project "ECommerce.Catalog" --context "ECommerce.Catalog.Infra.CatalogDBContext"
dotnet ef database update --project "ECommerce.Catalog" --startup-project "ECommerce.Catalog" --context "ECommerce.Catalog.Infra.CatalogDBContext"