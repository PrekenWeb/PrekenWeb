IF NOT EXISTS (SELECT column_name 
			   FROM INFORMATION_SCHEMA.columns 
			   WHERE table_name = 'Preek' 
			   AND column_name = 'Youtube')
BEGIN
	-- Add source file name column.
	ALTER TABLE dbo.Preek ADD
		Youtube varchar(MAX) NULL
END