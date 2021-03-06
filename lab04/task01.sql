﻿--1.Создание базу данных с именем Something. Для этого можно использовать инструкцию CREATE DATABASE.
CREATE DATABASE Something
GO

--2.Убедитесь в том, что база данных была успешно создана. Для этих целей напишите простейший SELECT,
--который выводит на экран имя базы данных и дату её создания. 
--Результат его выполнения должен содержать имя только что созданной базы Something.

SELECT name, create_date
FROM sys.databases
WHERE name='Something'
GO

--3.В базе Something создайте таблицу Wicked с одной колонкой Id типа INT. 
--Колонка Id не должна поддерживать NULL значения.

USE Something
GO
CREATE TABLE Wicked (
	Id INT NOT NULL
)
GO

--4. Создайте бэкап базы данных Something при помощи инструкции BACKUP DATABASE.
BACKUP DATABASE Something
TO DISK='D:\Labs\LAB04SQL\Somthing.bak'
GO

--5. Удалите базу данных при помощи инструкции DROP DATABASE.
USE master
GO
DROP DATABASE Something
GO

--6. Восстановите базу данных при помощи инструкции RESTORE DATABASE.
RESTORE DATABASE Something
FROM DISK='D:\Labs\LAB04SQL\Somthing.bak'
GO