IF EXISTS (SELECT column_name 
		   FROM INFORMATION_SCHEMA.columns 
		   WHERE table_name = 'Preek' 
		   AND column_name = 'Youtube')
BEGIN
	EXEC sp_rename 'dbo.Preek.Youtube', 'Video', 'COLUMN';
END