--Task 2

USE AdventureWorks2017
GO
--1. Вывести на экран список департаментов, отсортированных в порядке убывания названия департамента. 
--Вывести только первые 8 записей.

SELECT TOP (8) DepartmentID, Name, GroupName, ModifiedDate
  FROM HumanResources.Department
 ORDER BY HumanResources.Department.Name DESC;
GO

--2. Вывести на экран список сотрудников, которым было или исполнится 22 в год взятия сотрудника на работу.
 
SELECT e.NationalIDNumber, e.BusinessEntityID, e.JobTitle, e.BirthDate, e.HireDate
  FROM HumanResources.Employee AS e
 WHERE DATEDIFF(YEAR,  e.BirthDate, e.HireDate) = 22
 ORDER BY e.BusinessEntityID;
GO 

--3. Вывести на экран всех семейных сотрудников, начиная с самого взрослого, 
--которые занимают следующие позиции - Design Engineer, Tool Designer, Engineering Manager, Production Control Manager

SELECT e.BusinessEntityID, e.NationalIDNumber, e.JobTitle, e.BirthDate, e.MaritalStatus
  FROM HumanResources.Employee AS e
 WHERE e.MaritalStatus = 'M' AND e.JobTitle IN ('Design Engineer','Tool Designer', 'Engineering Manager', 'Production Control Manager')
 ORDER BY e.BirthDate;
GO

--4. Вывести на экран сотрудников, которых приняли на работу 5-го марта (любого года). 
--Отсортировать результат по возрастанию значения BusinessEntityID. 
--Вывести на экран только 5 строк, пропустив 1 значение.

SELECT e.BusinessEntityID, e.JobTitle, e.Gender, E.BirthDate, e.HireDate
  FROM HumanResources.Employee AS e
 WHERE MONTH(e.HireDate) = 3 AND DAY(e.HireDate) = 5
 ORDER BY e.BusinessEntityID
       OFFSET 1 ROWS
       FETCH NEXT 5 ROWS ONLY;
GO

--5. Вывести на экран список сотрудников женского пола, принятых на работу в среду (Wednesday). В поле LoginID заменить домен adventure-works на adventure-works2017.

SELECT  e.BusinessEntityID, e.JobTitle, e.Gender, e.HireDate, REPLACE(e.LoginId, 'adventure-works', 'adventure-works2017') AS LoginId
  FROM HumanResources.Employee AS e
 WHERE DATENAME(weekday, e.HireDate) = 'Wednesday'  AND e.Gender = 'F'
GO

--6. Вывести на экран сумму часов отпуска и сумму больничных часов у каждого сотрудника

SELECT Sum(e.VacationHours) AS VacationSumInHours, Sum(e.SickLeaveHours) AS SicknessSumInHours
  FROM HumanResources.Employee AS e
GO

--7. Вывести на экран список неповторяющихся должностей в убывающем порядке, причём отобразить только первые 8 названий. 
--В результате должен фигурировать новый столбец - LastWord, содержащий последнее слово из поля JobTitle.

SELECT DISTINCT TOP(8) e.JobTitle, IIF(CHARINDEX(' ', REVERSE(e.JobTitle)) < 1,
									   e.JobTitle, 
									   RIGHT(e.JobTitle, CHARINDEX(' ', REVERSE(e.JobTitle))-1)) AS LastWord
  FROM HumanResources.Employee AS e
 ORDER BY e.JobTitle DESC
GO

--8. Вывести на экран сотрудников, название позиции которых включает слово Control.
 
SELECT e.BusinessEntityID, e.JobTitle, e.Gender, e.BirthDate, e.HireDate
  FROM HumanResources.Employee AS e
 WHERE e.JobTitle LIKE '%Control%'
GO