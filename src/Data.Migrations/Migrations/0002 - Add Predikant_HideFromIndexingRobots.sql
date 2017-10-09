IF NOT EXISTS (SELECT column_name 
			   FROM INFORMATION_SCHEMA.columns 
			   WHERE table_name = 'Predikant' 
			   AND column_name = 'HideFromIndexingRobots')
BEGIN
	ALTER TABLE dbo.Predikant ADD 
		HideFromIndexingRobots bit NOT NULL CONSTRAINT DF_Predikant_HideFromIndexingRobots DEFAULT (0),
		HideFromPodcast bit NOT NULL CONSTRAINT DF_Predikant_HideFromPodcast DEFAULT (0)
END