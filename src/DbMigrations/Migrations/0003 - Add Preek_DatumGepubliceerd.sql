IF NOT EXISTS (SELECT column_name 
			   FROM INFORMATION_SCHEMA.columns 
			   WHERE table_name = 'Preek' 
			   AND column_name = 'DatumGepubliceerd')
BEGIN
	-- Add published date column.
	ALTER TABLE dbo.Preek ADD 
		DatumGepubliceerd datetime NULL

	-- UPDATE DatumGepubliceerd for already published sermons.
	EXEC('UPDATE dbo.Preek SET DatumGepubliceerd = DatumAangemaakt WHERE Gepubliceerd = 1')
END