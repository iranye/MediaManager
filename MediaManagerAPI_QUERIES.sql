USE MediaManagerDb -- ON (localdb)\mssqllocaldb

SELECT * FROM dbo.Volumes
SELECT * FROM dbo.M3uFiles
SELECT * FROM dbo.FileEntries

RETURN

DELETE dbo.M3uFiles WHERE id > 3

UPDATE Volumes SET Title='Dance-01', Moniker='dance-01' WHERE Id = 3