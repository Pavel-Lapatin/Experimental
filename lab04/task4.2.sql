--Task 4.2

USE AdventureWorks2017
GO

--1. Cоздайте таблицу dbo.StateProvince с такой же структурой как Person.StateProvince, кроме поля с тимпом uniqueidentifier, 
--не включая индексы, ограничения и триггеры.

CREATE TABLE [dbo].[StateProvince](
	[StateProvinceID] [int] NOT NULL,
	[StateProvinceCode] [nchar](3) NOT NULL,
	[CountryRegionCode] [nvarchar](3) NOT NULL,
	[IsOnlyStateProvinceFlag] [dbo].[Flag] NOT NULL,
	[Name] [dbo].[Name] NOT NULL,
	[TerritoryID] [int] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 )
 GO

 --2. Используя инструкцию ALTER TABLE, создайте для таблицы dbo.StateProvince ограничение UNIQUE для поля Name.

 ALTER TABLE dbo.StateProvince
 ADD CONSTRAINT UQ_StateProvince_Name UNIQUE(Name)
 GO

 --3. Используя инструкцию ALTER TABLE, создайте для таблицы dbo.StateProvince ограничение для поля CountryRegionCode, 
 --запрещающее заполнение этого поля цифрами.

 ALTER TABLE dbo.StateProvince
 ADD CONSTRAINT CHK_StateProvince_CountryRegionCode CHECK (CountryRegionCode NOT LIKE '%[0-9]%')
 GO

 --4. Используя инструкцию ALTER TABLE, создайте для таблицы dbo.StateProvince ограничение DEFAULT для поля ModifiedDate, 
 --задайте значение по умолчанию текущую дату и время.

 ALTER TABLE dbo.StateProvince
 ADD CONSTRAINT DF_StateProvince_ModifiedDate DEFAULT GETDATE() FOR ModifiedDate
 GO

 --5. Заполните новую таблицу данными из Person.StateProvince. 
 --Выберите для вставки только те данные, где имя штата/государства совпадает с именем страны/региона в таблице CountryRegion.

 GO
 INSERT INTO dbo.StateProvince (StateProvinceID, StateProvinceCode, CountryRegionCode, IsOnlyStateProvinceFlag, Name, TerritoryID)
 SELECT  sp.StateProvinceID, sp.StateProvinceCode, sp.CountryRegionCode, sp.IsOnlyStateProvinceFlag, sp.Name, sp.TerritoryID
   FROM Person.StateProvince AS sp 
        INNER JOIN Person.CountryRegion as cr 
		ON sp.CountryRegionCode = cr.CountryRegionCode
  WHERE sp.Name = cr.Name
 GO

--6. Удалите поле IsOnlyStateProvinceFlag из таблицы dbo.StateProvince. Создайте поле Population типа INT, поддерживающее NULL-значения.

ALTER TABLE dbo.StateProvince
 DROP COLUMN IsOnlyStateProvinceFlag
GO
ALTER TABLE dbo.StateProvince
  ADD Population INT NULL
GO