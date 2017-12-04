--Task 5.


--1. Создайте таблицу dbo.Person_New с такой же структурой как dbo.Person.

USE [AdventureWorks2017]
GO

/****** Object:  Table [dbo].[Person_New]    Script Date: 12/4/2017 4:02:24 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
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
 CONSTRAINT [PK_Person_New_PersonID] PRIMARY KEY CLUSTERED 
(
	[PersonId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Person_New] ADD  CONSTRAINT [DF_Person_New_Title]  DEFAULT (N'N/A') FOR [Title]
GO

ALTER TABLE [dbo].[Person_New]  WITH CHECK ADD  CONSTRAINT [CHK_Person_New_MiddelName] CHECK  (([MiddleName]='L' OR [MiddleName]='J'))
GO

ALTER TABLE [dbo].[Person_New] CHECK CONSTRAINT [CHK_Person_New_MiddelName]
GO

--2. Добавьте в таблицу dbo.Person поле Salutaion типа nvarchar длинной в 80 символов.

ALTER TABLE dbo.Person_New
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
SELECT p.BusinessEntityID, p.PersonType, p.NameStyle, 
	   p.FirstName, p.MiddleName, p.LastName, 
	   p.Suffix, p.EmailPromotion, p.ModifiedDate, CASE
	                                               WHEN  e.Gender = 'M' THEN 'Mr.'
												   ELSE 'Ms.'
	                                               END
  FROM dbo.Person AS p 
	   INNER JOIN HumanResources.Employee as e 
	   ON p.BusinessEntityID = e.BusinessEntityID
GO
 --4. Обновине поле Salutation в таблице dbo.Person значением, которое равно объединению полей Title и FirstName из той же таблицы. 
 --В качестве разделителя использовать пробел. Например, для Dana Burnell Salutation будет выглядеть как Ms. Dana.
UPDATE dbo.Person_New
   SET Salutation = CONCAT(Title, ' ', FirstName) 
GO

--5. Удалите данные из dbo.Person, где значение поля Salutation превысило 10 символов.
DELETE FROM dbo.Person_New
 WHERE LEN(Salutation) > 10
GO

--6. Удалите все созданные ограничения и значения по умолчанию из таблицы dbo.Person.

ALTER TABLE dbo.Person_New
DROP CONSTRAINT PK_Person_New_PersonID
GO

ALTER TABLE dbo.Person_New
DROP CONSTRAINT CHK_Person_New_MiddelName
GO

ALTER TABLE dbo.Person_New DROP DF_Person_New_Title
GO

--------------------------------------------------------------------------------------
-----Удаление всех ограничений сразу с использованием INFORMATION_SCHEMA
DECLARE @sql NVARCHAR(MAX) = N'';

SELECT @sql += N'ALTER TABLE ' + TABLE_NAME +' DROP CONSTRAINT ' + CONSTRAINT_NAME + ';'
FROM AdventureWorks2017.INFORMATION_SCHEMA.CONSTRAINT_TABLE_USAGE
WHERE TABLE_NAME = 'Person_New' AND TABLE_SCHEMA = 'dbo'

EXEC sp_executesql @sql;
GO
-------------------------------------------------------------------------------------
-------Удаление всех default сразу с использованием sys.default_constraints
DECLARE @sql NVARCHAR(MAX) = N''; 

SELECT @sql += N'ALTER TABLE [dbo].[Person_New] DROP ' + dc.name + ';'
FROM AdventureWorks2017.sys.default_constraints AS dc
WHERE parent_object_id = OBJECT_ID('[dbo].[Person_New]')

EXEC sp_executesql @sql;
GO

--7.Удалите поле PersonId из таблицы dbo.Person.
ALTER TABLE dbo.Person
 DROP CONSTRAINT PK_Person_PersonID
GO
ALTER TABLE dbo.Person DROP COLUMN PersonId
GO

--8.Удалите таблицы dbo.Person и dbo.Person_New.
DROP TABLE dbo.Person_New
GO

DROP TABLE dbo.Person
GO


