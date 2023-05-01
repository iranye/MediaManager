USE MediaManagerDb -- ON (localdb)\mssqllocaldb

SELECT * FROM dbo.Volumes
SELECT * FROM dbo.M3uFiles
SELECT * FROM dbo.M3uFileEntry
SELECT * FROM dbo.FileEntries

RETURN

DELETE dbo.M3uFiles WHERE id > 3
DELETE dbo.M3uFiles WHERE id = 10
DELETE dbo.M3uFileEntry WHERE M3uFileId = 10
DELETE dbo.FileEntries WHERE id > 14

UPDATE Volumes SET Title='Dance-01', Moniker='dance-01' WHERE Id = 3