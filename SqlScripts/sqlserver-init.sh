
#!/bin/bash
function create_db_if_not_exists () {
    echo "Starting background process to create db..."
    DB_NAME=$1

    echo "Creating db if does not exist: $DB_NAME"
    /opt/mssql-tools/bin/sqlcmd -S localhost,1433 -U sa -P 1q2w3e4r@# -d master -Q \
        "IF(db_id(N'ECommerceOrder') IS NULL) CREATE DATABASE $DB_NAME;"

    echo "Background database creation is done!"
}

create_db_if_not_exists ECommerceOrder &  # runs in bg process

echo "Starting SQL Server..."
/opt/mssql/bin/sqlservr