-- postgres
-- \c mediamanagerdb
SELECT * FROM "Volumes";
SELECT * FROM "M3uFiles";
SELECT * FROM "M3uFileEntry";
SELECT * FROM "FileEntries";

-- sql server
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

     --SELECT [f].[Id], [f].[Name]
     -- FROM [FileEntries] AS [f]
     -- WHERE EXISTS (
     --     SELECT 1
     --     FROM [M3uFileEntry] AS [m]
     --     INNER JOIN [M3uFiles] AS [m0] ON [m].[M3uFileId] = [m0].[Id]
     --     WHERE ([f].[Id] = [m].[FileEntryId]) AND ([m0].[Id] = @__m3uId_0))