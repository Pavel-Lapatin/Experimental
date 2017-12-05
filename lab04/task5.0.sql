--Task 5.


--1. Создайте таблицу dbo.Person_New с такой же структурой как dbo.Person.

USE [AdventureWorks2017]
GO

CREATE TABLE [dbo].[Person_New](
	[BusinessEntityID] [int] NOT NULL,
	[PersonType] [nchar](2) NOT NULL,
	[NameStyle] [dbo].[NameStyle] NOT NULL,
	[Title] [nvarchar](6) NULL,
	[FirstName] [dbo].[Name] NOT NULL,
	[MiddleName] [dbo].[Name] NULL,
	[LastName] [dbo].[Name] NOT NULL,
	[Suffix] [nvarchar](10) NULL,
	[EmailPromotion] [int] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[PersonId] [int] IDENTITY(3,5) NOT NULL,
)
 ALTER TABLE dbo.Person_New
ADD CONSTRAINT PK_Person_New_PersonID PRIMARY KEY (PersonId)
GO

ALTER TABLE dbo.Person_New 
ADD CONSTRAINT DF_Person_New_Title  DEFAULT (N'N/A') FOR Title
GO

ALTER TABLE dbo.Person_New 
ADD CONSTRAINT [CHK_Person_New_MiddelName] CHECK  ((MiddleName='L' OR MiddleName='J'))
GO

--2. Добавьте в таблицу dbo.Person поле Salutaion типа nvarchar длинной в 80 символов.

ALTER TABLE dbo.Person
ADD Salutation NVARCHAR(80) NULL

--3.Заполните таблицу dbo.Person_New данными из таблицы dbo.Person, 
--при этом выставите значение поля Title согласно следующему правилу: 
--если поле Gender из таблицы HumanResources.Employee равно M, то тогда 
--значение Title=Mr.; если поле Gender равно F, то значение Title=Ms.
INSERT INTO dbo.Person_New (BusinessEntityID, 
							PersonType, 
							NameStyle, 
							FirstName, 
							MiddleName, 
							LastName, 
							Suffix, 
							EmailPromotion, 
							ModifiedDate, 
							Title)
SELECT p.BusinessEntityID, 
       p.PersonType, 
	   p.NameStyle, 
	   p.FirstName, 
	   p.MiddleName, 
	   p.LastName, 
	   p.Suffix, 
	   p.EmailPromotion, 
	   p.ModifiedDate, 
	   IIF(e.Gender = 'M', 'Mr.', 'Ms.')
  FROM dbo.Person AS p 
	   INNER JOIN HumanResources.Employee as e 
	   ON p.BusinessEntityID = e.BusinessEntityID
GO
 --4. Обновите поле Salutation в таблице dbo.Person значением, которое равно объединению полей Title и FirstName из той же таблицы. 
 --В качестве разделителя использовать пробел. Например, для Dana Burnell Salutation будет выглядеть как Ms. Dana.
UPDATE p1
   SET p1.Title = p2.Title
  FROM dbo.Person AS p1
        INNER JOIN dbo.Person_New as p2 ON p1.PersonId = p2.PersonId

UPDATE dbo.Person
   SET Salutation = CONCAT(dbo.Person.Title, ' ', dbo.Person.FirstName) 
GO

--5. Удалите данные из dbo.Person, где значение поля Salutation превысило 10 символов.
DELETE FROM dbo.Person
 WHERE LEN(Salutation) > 10
GO

--6. Удалите все созданные ограничения и значения по умолчанию из таблицы dbo.Person.

ALTER TABLE dbo.Person
DROP CONSTRAINT PK_Person_PersonID
GO

ALTER TABLE dbo.Person
DROP CONSTRAINT CHK_Person_MiddelName
GO

ALTER TABLE dbo.Person 
DROP DF_Person_Title
GO

--------------------------------------------------------------------------------------
-----Удаление всех ограничений сразу с использованием INFORMATION_SCHEMA
DECLARE @sql NVARCHAR(MAX) = N'';

SELECT @sql += N'ALTER TABLE ' + TABLE_NAME +' DROP CONSTRAINT ' + CONSTRAINT_NAME + ';'
FROM AdventureWorks2017.INFORMATION_SCHEMA.CONSTRAINT_TABLE_USAGE
WHERE TABLE_NAME = 'Person' AND TABLE_SCHEMA = 'dbo'

EXEC sp_executesql @sql;
GO
-------------------------------------------------------------------------------------
-------Удаление всех default сразу с использованием sys.default_constraints
DECLARE @sql NVARCHAR(MAX) = N''; 

SELECT @sql += N'ALTER TABLE [dbo].[Person] DROP ' + dc.name + ';'
FROM AdventureWorks2017.sys.default_constraints AS dc
WHERE parent_object_id = OBJECT_ID('[dbo].[Person]')

EXEC sp_executesql @sql;
GO

--7.Удалите поле PersonId из таблицы dbo.Person.

ALTER TABLE dbo.Person 
DROP COLUMN PersonId
GO

--8.Удалите таблицы dbo.Person и dbo.Person_New.

DROP TABLE dbo.Person
GO

DROP TABLE dbo.Person_New
GO