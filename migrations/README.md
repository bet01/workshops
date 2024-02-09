# Migrations

Migrations can be useful to get a developer up and running quickly with a project. When used with local containers running MS SQL (or whichever other DB you like) the developer has their own play area and will not interfere with others. Other benefits are that you can destroy and re-create the database at any time (especially after making a mess). In more mature processes migrations can also be used to apply new database changes to QA & Production environments removing the need for DBA & DevOps involvement.

## Migration Weather API Sample Project

This is the sample API generated with `dotnet new webapi`. Migrations have been added using FluentMigrator and data access with Dapper. The WeatherForecast table has been setup as Memory Optimised.

## MS SQL Container Information

### Enterprise MS SQL
https://learn.microsoft.com/en-us/sql/linux/sql-server-linux-docker-container-deployment?view=sql-server-ver16&pivots=cs1-bash

### Linux Memory Tables
https://learn.microsoft.com/en-us/sql/linux/sql-server-linux-performance-get-started?view=sql-server-ver16
