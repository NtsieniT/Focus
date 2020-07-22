@ECHO OFF
SET env_copy=%ASPNETCORE_ENVIRONMENT%
SET /p env="Enter environment: "
SET ASPNETCORE_ENVIRONMENT=%env%
dotnet ef database update -c Focus.Incident.Infrastructure.DB.EntityModels.ApplicationDbContext
SET ASPNETCORE_ENVIRONMENT=%env_copy%