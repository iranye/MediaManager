USE MediaManagerDb -- ON (localdb)\mssqllocaldb

SELECT * FROM dbo.Volumes
SELECT * FROM dbo.M3uFiles
SELECT * FROM dbo.FileEntries

RETURN

DELETE dbo.M3uFiles WHERE id > 3
