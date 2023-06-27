-- postgres
-- \c mediamanagerdb
SELECT * FROM "Volumes";
SELECT * FROM "M3uFiles";
SELECT * FROM "M3uFileEntry";
SELECT * FROM "FileEntries";
SELECT * FROM "AspNetUsers";
SELECT * FROM "AspNetRoles";
DELETE FROM "AspNetUsers";
DELETE FROM "AspNetUsers" WHERE "AspNetUsers.UserName" = "inye@mailinator.com";


SELECT * FROM information_schema.columns WHERE table_schema = 'public'
   AND table_name   = 'your_table'
     ;

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

SELECT * FROM "AspNetUsers";
SELECT * FROM "AspNetUserRoles";
SELECT * FROM "AspNetRoles";

RETURN

UPDATE AspNetRoles SET [Name] =  'OWNER' WHERE [Name] = 'Owner'

INSERT INTO AspNetUserRoles ([UserId], [RoleId])
VALUES ('7f3954ed-619d-47f8-9d90-af6508267691', '892977a9-b0a8-4911-adea-73cfa9bed9a4')

SELECT * FROM [dbo].[AspNetUserClaims]
SELECT * FROM [dbo].[AspNetRoleClaims]

-- DELETE AspNetUsers
-- DELETE AspNetUserRoles