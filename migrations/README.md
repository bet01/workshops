
# MS SQL Container Stuff

## Enterprise MS SQL
https://learn.microsoft.com/en-us/sql/linux/sql-server-linux-docker-container-deployment?view=sql-server-ver16&pivots=cs1-bash

## Linux Memory Tables
https://learn.microsoft.com/en-us/sql/linux/sql-server-linux-performance-get-started?view=sql-server-ver16

Scripts:
```
CREATE DATABASE Weather;

USE Weather;

SELECT d.compatibility_level
    FROM sys.databases as d
    WHERE d.name = Db_Name();

ALTER DATABASE Weather 
SET MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT = ON;

ALTER DATABASE Weather 
ADD FILEGROUP weather_mod CONTAINS MEMORY_OPTIMIZED_DATA;

ALTER DATABASE Weather ADD FILE (
    name='weather_mod1', filename='/var/opt/mssql/data/weather_mod1')
    TO FILEGROUP weather_mod;

CREATE TABLE dbo.ShoppingCart (
ShoppingCartId INT IDENTITY(1,1) PRIMARY KEY NONCLUSTERED,
UserId INT NOT NULL INDEX ix_UserId NONCLUSTERED HASH WITH (BUCKET_COUNT=1000000),
CreatedDate DATETIME2 NOT NULL,
TotalPrice MONEY
) WITH (MEMORY_OPTIMIZED=ON);

```
