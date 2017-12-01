--Task 3

USE AdventureWorks2017
GO

--1.Вывести на экран список сотрудников с указанием максимальной ставки, по которой им выплачивали денежные средства. 

SELECT e.BusinessEntityID, e.JobTitle, MAX(eph.Rate) AS 'Max Rate'
FROM HumanResources.Employee AS e INNER JOIN HumanResources.EmployeePayHistory AS eph ON e.BusinessEntityID = eph.BusinessEntityID
GROUP BY e.BusinessEntityID, e.JobTitle
GO

--2. Разбить все почасовые ставки на группы таким образом, чтобы одинаковые ставки входили в одну группу. 
--Номера групп должны быть распределены по возрастанию ставок. Назовите столбец RankRate.

SELECT e.BusinessEntityID, e.JobTitle, eph.Rate, DENSE_RANK() OVER (ORDER BY eph.Rate) AS RateRank  
FROM HumanResources.Employee AS e INNER JOIN HumanResources.EmployeePayHistory AS eph ON e.BusinessEntityID = eph.BusinessEntityID
GO

 --3. Вывести на экран список сотрудников, у которых почасовая ставка изменялась хотя бы один раз. 
 --Если сотрудник больше не работает в отделе — не учитывать такие данные. 

SELECT e.BusinessEntityID, e.JobTitle, COUNT(eph.Rate) AS RateCount 
FROM HumanResources.Employee AS e INNER JOIN HumanResources.EmployeePayHistory AS eph ON e.BusinessEntityID = eph.BusinessEntityID
								  INNER JOIN HumanResources.EmployeeDepartmentHistory AS edh ON e.BusinessEntityID = edh.BusinessEntityID
WHERE edh.EndDate IS NULL
GROUP BY e.BusinessEntityID, e.JobTitle
HAVING COUNT(eph.Rate) > 1
GO

	--Исправленный запрос 3. С учетом "Если сотрудник больше не работает в отделе — не учитывать такие данные."
	--Запрос отображает количество изменений ставок работников при их работе в текущем отделе

SELECT e.BusinessEntityID, e.JobTitle, COUNT(eph.Rate) AS RateCount 
FROM HumanResources.Employee AS e INNER JOIN HumanResources.EmployeePayHistory AS eph ON e.BusinessEntityID = eph.BusinessEntityID
								  INNER JOIN HumanResources.EmployeeDepartmentHistory AS edh ON e.BusinessEntityID = edh.BusinessEntityID
WHERE edh.EndDate IS NULL AND  edh.StartDate <= eph.RateChangeDate
GROUP BY e.BusinessEntityID, e.JobTitle
HAVING COUNT(eph.Rate) > 1
GO


 --4.Вывести на экран количество сотрудников в каждом отделе. Назовите столбец, содержащий результат - EmployeeCount.

SELECT  d.Name, COUNT(e.BusinessEntityID) AS EmployeeCount
FROM HumanResources.Department AS d INNER JOIN HumanResources.EmployeeDepartmentHistory AS edh ON d.DepartmentID = edh.DepartmentID
									INNER JOIN HumanResources.Employee AS e ON e.BusinessEntityID = edh.BusinessEntityID
WHERE edh.EndDate IS NULL
GROUP BY d.Name
GO

 --5.Вывести на экран количество сотрудников в каждом отделе. Назовите столбец, содержащий результат - EmployeeCount.

 --Вариант №1
set statistics time on;
SELECT  e.BusinessEntityID, e.JobTitle, eph.Rate, eph.RateChangeDate, LAG(eph.Rate, 1, 0) 
																		OVER (PARTITION BY e.BusinessEntityID ORDER BY eph.Rate) AS PrevRepRate, 
																		(eph.Rate - LAG(eph.Rate, 1, 0) 
																		OVER (PARTITION BY e.BusinessEntityID ORDER BY eph.Rate)) AS Diff
FROM (HumanResources.Employee AS e INNER JOIN HumanResources.EmployeePayHistory AS eph ON e.BusinessEntityID = eph.BusinessEntityID)
set statistics time off;
GO

 --Вариант №2.
set statistics time on;
SELECT  e.BusinessEntityID, e.JobTitle, eph.Rate, eph.RateChangeDate, tpeph.pr AS PrevRate, eph.Rate-tpeph.pr AS Diff 
FROM  HumanResources.EmployeePayHistory AS eph INNER JOIN (SELECT peph.BusinessEntityID AS pbid, peph.RateChangeDate AS cd, LAG(peph.Rate, 1, 0) 
																							                  OVER (PARTITION BY peph.BusinessEntityID 
																								              ORDER BY peph.Rate) AS pr  
										                   FROM HumanResources.EmployeePayHistory AS peph) AS tpeph  ON tpeph.pbid = eph.BusinessEntityID AND tpeph.cd = eph.RateChangeDate
												INNER JOIN HumanResources.Employee AS e ON e.BusinessEntityID = eph.BusinessEntityID
set statistics time off;
GO

--Вариант №3
set statistics time on;
SELECT  e.BusinessEntityID, e.JobTitle, eph.Rate, ISNULL((SELECT TOP(1) HumanResources.EmployeePayHistory.Rate
														  FROM HumanResources.EmployeePayHistory
														  WHERE HumanResources.EmployeePayHistory.Rate < eph.Rate AND eph.BusinessEntityID = HumanResources.EmployeePayHistory.BusinessEntityID
														  ORDER BY HumanResources.EmployeePayHistory.RateChangeDate DESC), 0) AS p,  
																	(eph.Rate- ISNULL((SELECT TOP(1) HumanResources.EmployeePayHistory.Rate
																					   FROM HumanResources.EmployeePayHistory
																					   WHERE HumanResources.EmployeePayHistory.Rate < eph.Rate AND eph.BusinessEntityID = HumanResources.EmployeePayHistory.BusinessEntityID
														                               ORDER BY HumanResources.EmployeePayHistory.RateChangeDate DESC), 0)) AS Diff
FROM (HumanResources.Employee AS e INNER JOIN HumanResources.EmployeePayHistory AS eph ON e.BusinessEntityID = eph.BusinessEntityID)
set statistics time off;

--6. Вывести на экран почасовые ставки сотрудников, с указанием максимальной ставки для каждого отдела в столбце MaxInDepartment. 
--В рамках каждого отдела разбейте все ставки на группы таким образом, чтобы ставки с одинаковыми значениями входили в состав одной группы. 

SELECT  d.Name, t.BusinessEntityID, t.maxRate, MAX(t.maxRate) OVER (PARTITION BY d.Name ORDER BY t.maxRate DESC) AS MaxInDepartment, DENSE_RANK() OVER (PARTITION BY d.Name ORDER BY t.maxRate) AS RateGroup						
FROM  HumanResources.Department AS d INNER JOIN HumanResources.EmployeeDepartmentHistory AS edh ON d.DepartmentID = edh.DepartmentID
									 INNER JOIN (SELECT  HumanResources.Employee.BusinessEntityID, MAX(HumanResources.EmployeePayHistory.Rate) AS maxRate
												 FROM  HumanResources.Employee INNER JOIN HumanResources.EmployeePayHistory ON  HumanResources.EmployeePayHistory.BusinessEntityID = HumanResources.Employee.BusinessEntityID
												 GROUP BY HumanResources.Employee.BusinessEntityID
												 ) AS t ON t.BusinessEntityID = edh.BusinessEntityID 
												 
GROUP BY d.Name, t.BusinessEntityID, t.maxRate
ORDER BY d.Name, maxRate
GO

--7. Вывести на экран список сотрудников, которые работают в вечернюю смену. 

SELECT edh.BusinessEntityID, e.JobTitle, s.Name, CONVERT(VARCHAR(8), s.StartTime, 108), CONVERT(VARCHAR(8), s.EndTime, 108)
FROM HumanResources.EmployeeDepartmentHistory AS edh INNER JOIN HumanResources.Shift AS s ON edh.ShiftID = s.ShiftID 
													 INNER JOIN HumanResources.Employee AS e ON e.BusinessEntityID = edh.BusinessEntityID								
WHERE s.Name = 'Evening'

--8. Вывести на экран количество лет, которые каждый сотрудник проработал в каждом отделе - столбец Experience. Если сотрудник работает в отделе по настоящее время, количество лет считайте до сегодняшнего дня. 
SELECT e.BusinessEntityID, e.JobTitle, d.Name, edh.StartDate, edh.EndDate, DATEDIFF(YEAR, edh.StartDate, ISNULL(edh.EndDate, GETDATE())) AS Experience
FROM HumanResources.Employee AS e INNER JOIN HumanResources.EmployeeDepartmentHistory AS edh ON edh.BusinessEntityID = e.BusinessEntityID
								  INNER JOIN HumanResources.Department AS d ON d.DepartmentID = edh.DepartmentID
GO