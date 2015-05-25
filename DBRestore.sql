USE [master]
ALTER DATABASE [forecast] SET SINGLE_USER WITH ROLLBACK IMMEDIATE
RESTORE DATABASE [forecast] FROM  DISK = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\Backup\forecast.bak' WITH  FILE = 1,  MOVE N'forecast' TO N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\forecast.mdf',  MOVE N'forecast_log' TO N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\forecast_log.ldf',  NOUNLOAD,  REPLACE,  STATS = 5
ALTER DATABASE [forecast] SET MULTI_USER

GO
use forecast
go
sp_change_users_login 'update_one', 'forecast', 'forecast'