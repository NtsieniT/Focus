@ECHO OFF
SET env_copy=%ASPNETCORE_ENVIRONMENT%
SET /p env="Enter environment: "
SET /p migration="Enter migration name: "
SET ASPNETCORE_ENVIRONMENT=%env%
dotnet ef migrations add ApplicationDb_%migration% -c Focus.Incident.Infrastructure.DB.EntityModels.ApplicationDbContext -o Migrations/%ASPNETCORE_ENVIRONMENT%/ApplicationDb
SET ASPNETCORE_ENVIRONMENT=%env_copy%