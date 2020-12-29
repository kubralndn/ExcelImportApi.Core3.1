# ExcelImportApi.Core3.1
Excel import to Json file and SQL Database .Net Core 3.1 multi-tier application 

-- for migrations please run those lines on package manager before you start, 
-- it will automatically add the tables and relations on your localhost database

dotnet ef --startup-project CORE/CORE.csproj migrations add "MigrationName" -p DAL/DAL.csproj
dotnet ef --startup-project CORE/CORE.csproj database update

--imported json file saved in this path;

user path....\FileUploadWebApiCore\CORE\bin\Debug\netcoreapp3.1\ImportedFiles

--in any case that you might run 

dotnet tool install --global dotnet-ef
