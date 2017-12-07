--Task 4.1

USE AdventureWorks2017
GO


--1. Cоздайте таблицу dbo.Person с такой же структурой как Person.Person, 
--кроме полей с типами xml, uniqueidentifier, не включая индексы, ограничения и триггеры.

CREATE TABLE [dbo].[Person](
	[BusinessEntityID] [int] NOT NULL,
	[PersonType] [nchar](2) NOT NULL,
	[NameStyle] [dbo].[NameStyle] NOT NULL,
	[Title] [nvarchar](8) NULL,
	[FirstName] [dbo].[Name] NOT NULL,
	[MiddleName] [dbo].[Name] NULL,
	[LastName] [dbo].[Name] NOT NULL,
	[Suffix] [nvarchar](10) NULL,
	[EmailPromotion] [int] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 )
 GO

 --2.Используя инструкцию ALTER TABLE, добавьте в таблицу dbo.Person новое поле PersonId, которое является первичным ключом типа int и имеет свойство identity. 
 --Начальное значение для поля identity задайте 3 и приращение задайте 5.

 ALTER TABLE dbo.Person
 ADD PersonId INT IDENTITY(3, 5)
 CONSTRAINT PK_Person_PersonID PRIMARY KEY (PersonId)
 GO

 --3. Используя инструкцию ALTER TABLE, создайте для таблицы dbo.Person ограничение для поля Middle Name, чтобы заполнить его можно было только значениями J. или L.

 ALTER TABLE dbo.Person
 ADD CONSTRAINT CHK_Person_MiddelName CHECK (MiddleName IN ('J', 'L'))
 GO

 --4. Используя инструкцию ALTER TABLE, создайте для таблицы dbo.Person ограничение DEFAULT для поля Title, задайте значение по умолчанию N/A.

 ALTER TABLE dbo.Person
 ADD CONSTRAINT DF_Person_Title DEFAULT N'N/A' FOR Title
 GO

 --5. Заполните новую таблицу данными из Person.Person только для тех сотрудников, 
 --которые существуют в таблице HumanResources.Employee, исключив сотрудников из отдела Finance.

INSERT INTO dbo.Person (BusinessEntityID, PersonType, NameStyle, FirstName, MiddleName, LastName, Suffix, EmailPromotion, ModifiedDate, Title)
SELECT p.BusinessEntityID, p.PersonType, p.NameStyle, p.FirstName, p.MiddleName, p.LastName, p.Suffix, p.EmailPromotion, p.ModifiedDate, p.Title
  FROM Person.Person AS p 
       INNER JOIN HumanResources.Employee as e 
	   ON p.BusinessEntityID = e.BusinessEntityID
	   INNER JOIN HumanResources.EmployeeDepartmentHistory AS edh 
	   ON edh.BusinessEntityID = e.BusinessEntityID
	   INNER JOIN HumanResources.Department AS d 
	   ON d.DepartmentID = edh.DepartmentID
 WHERE d.Name <> 'Finance' AND p.MiddleName IN ('J','L')
GO

--6. Измените размерность поля Title, уменьшите размер поля до 6-ти символов.

ALTER TABLE dbo.Person
ALTER COLUMN Title NVARCHAR(6);
GO 