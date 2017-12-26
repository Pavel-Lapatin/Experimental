
#Exit from the system exit command
	exit

#Database created and filled using migrations.

	update-database -ProjectName NetMastery.Lab05.FileManager.DAL

#User authenticate commands:
	login -l | --login <userLogin> -p | --password <userpassword> - register in the system
	logoff - unregister in the system

Notes. 
	There are two users in data base:
	1) login: admin; password: admin;
	2) login: Pasha; password: Pasha the best; 

#Examples.

	#login existing user
	login -l admin -p admin 
	---Hi, admin---
	
	#repeat login the same user
	login -l admin -p admin 
	---You are user of the system already---

	#logoff of the system
	logoff
	---Goodby---

	#login nonexisting user
	login -l someone -p something 
	---Account with such login doesn't exist---


	#login the existing user with incorrect password
	login -l admin -p something 
	---Password is wrong---	


#User service commands:
	user info - shows info about registered user; 

#Examples:
	#show user info
	login -l admin -p admin 
	---Hi, admin---

	user info
	
	---
	Login: admin
	Creation date: 17-12-23
	Used disk space: 37096 kB
	Max disk space: 256000000 kB
	Root directory path: ~\adminRoot
	---

#Directory service commands:

Notes.

The storage is located on the disk according to the relative path by folder with .exe. 
It located in app.Config file. In virtual storage system such path presents as “~\”.
When you registered in the system,
you start working from your root directory, 
which called login+"Root". And current path is "~\login+"Root"".
Authenticated user can change the current directory using "directory cd <path>" command.
If path contains whiteSpacies, you shoul use double quotes "..."
If path don't have ~ at the beginnin it is considered as absolute patht.
	directory info <path> - shows info about directory;
	directory  create <path> <name> - create a directory;
	directory  remove <path>  - delete a directory;
	directory search <path> <pattern> - search a pattern
	directory list <path> - search directories and files in the directory
	directory move <pathSource> <pathDestination> - search directories and files in the directory
	directory cd <path> - change working path


#Example of directory cd <path>:	
	#Initial current path: ~\adminRoot
	
	input cmd: ~\adminRoot--> directory cd ..\
	output: Access is denied
	output cmd: ~\adminRoot-->

	input cmd: ~\adminRoot--> directory cd .\
	output cmd: ~\adminRoot-->
	output cmd: ~\adminRoot-->

	input cmd: ~\adminRoot--> directory cd ..\..\..\..\
	output: Access is denied
	output cmd: ~\adminRoot-->

	input cmd: ~\adminRoot--> directory cd ~\PashaRoot
	output: Access is denied
	output cmd: ~\adminRoot-->

	input cmd: ~\adminRoot--> directory cd "E:\Godel\new fold\p-lapatin\FileManager\CommonStorage\PashaRoot"
	output: Access is denied

	input cmd: ~\adminRoot--> directory cd "E:\Godel\new fold\p-lapatin\FileManager\CommonStorage\adminRoot"
	output: Directory doesn't exist
	#This is happens because cd works with virtual pathes not absolute.

	input cmd: ~\adminRoot--> directory cd .\admin-Dir1-Lvl1\admin-Dir2.1-Lvl2
	output cmd: ~\adminRoot\admin-Dir1-Lvl1\admin-Dir2.1-Lvl2-->

	input cmd: ~\adminRoot--> directory cd ..\..\
	output cmd: ~\adminRoot-->
	
	input cmd: ~\adminRoot--> directory cd ~\adminRoot\admin-Dir1-Lvl1\admin-Dir2.1-Lvl2	
	output cmd: ~\adminRoot\admin-Dir1-Lvl1\admin-Dir2.1-Lvl2-->

#Example of directory info <path>:

	input cmd: ~\adminRoot--> directory info ~\adminRoot\admin-Dir1-Lvl1\admin-Dir2.1-Lvl2	

	---
	Login: admin
	Path: ~\adminRoot\admin-Dir1-Lvl1\admin-Dir2.1-Lvl2
	CreationDate: 16-02-06	
	ModificationDate: 16-02-06
	Size: 34024 kB
	---
	output cmd: ~\adminRoot-->

	input cmd: ~\adminRoot--> directory info .\

	----
	Login: admin
	Path: ~\adminRoot
	CreationDate: 16-02-06
	ModificationDate: 16-02-06
	Size: 3072 kB
	----

#Example of directory create <path> <name>:
	
	input cmd: ~\adminRoot--> directory create .\ delete1
	output: Directorry created successfully

	input cmd: ~\adminRoot--> directory cd .\delete1
	output cmd: ~\adminRoot\delete1-->
	
	input cmd: ~\adminRoot\delete1--> directory create .\ delete2
	output: Directorry created successfully

	input cmd: ~\adminRoot\delete1--> directory create .\ delete3
	output: Directorry created successfully

	input cmd: ~\adminRoot\delete1--> directory create .\ delete3
	output:This directory already exists

	input cmd: ~\adminRoot\delete1--> directory cd .\delete3
	output cmd: ~\adminRoot\delete1\delete3-->

	input cmd: ~\adminRoot\delete1\delete3--> directory create .\ delete4
	output: Directorry created successfully

	input cmd: ~\adminRoot\delete1\delete3--> directory create .\ delete5
	output: Directorry created successfully

	input cmd: ~\adminRoot\delete1\delete3--> directory create .\ delete6
	output: Directorry created successfully

#Example of directory search <path> <pattern>:

	input cmd: ~\adminRoot--> directory search .\ ete
	
	output: There are following results for pattern ete:
		~\adminRoot\delete1\delete2
		~\adminRoot\delete1\delete3\delete4
		~\adminRoot\delete1\delete3\delete5
		~\adminRoot\delete1\delete3\delete6
		~\adminRoot\delete1\delete3
		~\adminRoot\delete1

	input cmd: ~\adminRoot\delete1\delete3\--> directory search .\ ete
	
	output: There are following results for pattern ete:
		~\adminRoot\delete1\delete3\delete4
		~\adminRoot\delete1\delete3\delete5
		~\adminRoot\delete1\delete3\delete6
		~\adminRoot\delete1\delete3
	
#Example of directory list <path>:

	input cmd: ~\adminRoot--> directory list .\
	
	output:<---Directories:--->
		admin-Dir1-Lvl1
		admin-Dir2-Lvl1
		Downloads
		delete1

		<---Files:--->
		file2.html
		file3.txt

#Example of directory move <path> <path>:
	input cmd: ~\adminRoot--> directory move .\delete1 .\admin-Dir1-Lvl1
	output: Directory successfully moved

	input: ~\adminRoot-->directory cd .\delete1
	output: Directory dosen't exist

	input: ~\adminRoot-->directory cd .\admin-Dir1-Lvl1\delete1
	output cmd: ~\adminRoot\admin-Dir1-Lvl1\delete1-->


#Example of directory remove <path>:
	input cmd: ~\adminRoot\admin-Dir1-Lvl1\delete1--> directory remove .\
	output: Directory successfully removed


	



	
