IF OBJECT_ID(N'dbo.__MigrationHistory', N'U') IS NOT NULL
BEGIN
	drop table [dbo].[__MigrationHistory]
	drop table [dbo].[__RefactorLog]
END