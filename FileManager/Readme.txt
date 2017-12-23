
#Exit from the system exit command
	exit

#Database created and filled using migrations.

	update-database -ProjectName NetMastery.Lab05.FileManager.DAL

#User athenticate commands:
	login -l | --login <userLogin> -p | --password <userpassword> - register in the system
	logoff - unregister in the system

#Examples:
	#login existing the same user
	login -l admin -p admin 
	---Hi, admin---
	
	#repeat login the same user
	login -l admin -p admin 
	---You are user of the system already---

	#repeat login the same user
	logoff
	---Goodby---

	#login nonexisting user
	login -l someone -p something 
	---Account with such login doesn't exist---


	#login the existing user with incorrect password
	login -l admin -p something 
	---Password is wrongy---	


#User service commands:
	user  -i | --info - shows info about registered user; 

#Examples:
	#show user info
	login -l admin -p admin 
	---Hi, admin---

	user --info
	
	---
	Login: admin
	Creation date: 17-12-23
	Used disk space: 37096 kB
	Max disk space: 256000000 kB
	Root directory path: ~\adminRoot
	---

#Directory service commands:

Notes.

There is current path in the system. 
It is ~\ - common root directory <....\CommonStorage> where located different root folders for each user
Authenticated user could change it in the scope of it's directorys tree.
Curent path is presented at the begining of command line.
All relative pathes are created using curent path if additional path begin: 
.\  (current directory)
..\ - (previous directory)
if there is no reference on the perent directories the path is obsolute.
For working with virtual directories in database using absolete pathes they should start with ~\ at the begining,
if not this path was related to the OS file system (shold starts with Disk:\....);
 
	directory  -i | --info <path> - shows info about directory;
	directory  create <path> <name> - create a directory;
	directory  remove <path>  - delete a directory;
	directory search <path> <pattern> - search a pattern
	directory list <path> - search directories and files in the directory
	directory move <pathSource> <pathDestination> - search directories and files in the directory


#Examples:
	#show directory info
	login -l admin -p admin 
	---Hi, admin---

	directory --info
	
	---
	Login: admin
	Creation date: 17-12-23
	Used disk space: 37096 kB
	Max disk space: 256000000 kB
	Root directory path: ~\adminRoot
	---

	#create new directory
	directory create .\ delete1

	#delete a directory
	
	--------------------
	#create directory for remove
	directory cd .\delete1
	directory create .\ delete2
	directory create .\ delete3
	directory create .\delete2 delete4
	directory create .\delete2 delete5
	directory create .\delete2 delete6
	
	#remove directory
	directory cd .\delete1

	#move directory
	directory move .\admin-Dir1-Lvl1 .\admin


	#Search by pattern in directory
	directory search .\ ete

	#List by pattern in directory
	directory list .\ 


