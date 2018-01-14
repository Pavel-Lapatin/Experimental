
#Exit from the system exit command
	exit

#Database created or updated during the start of the application.

#User authenticate commands:
	login -l | --login <userLogin> -p | --password <userpassword> - register in the system
	logoff - unregister in the system

Notes. 
	There are two users in data base:
	1) login: admin; password: admin;

#Examples.

#login existing user
--> login -l admin -p admin 
	
	---------Output---------------
	Messages:
	Welcome to the system, admin
	
	~\adminRoot-->_
	------------------------------
	
#repeat login the same user
~\adminRoot--> login -l admin -p admin 
	
	---------Output---------------
	Messages: 
	admin has already signed in th the system
	
	~\adminRoot-->_
	------------------------------
	
#logoff of the system
~\adminRoot--> logoff
	
	---------Output---------------
	Goodby!

	--> 
	------------------------------
		
#login nonexisting user
--> login -l someone -p something 
	
	---------Output---------------
	Account with such login doesn't exist

	-->
	------------------------------
	
#login the existing user with incorrect password
--> login -l admin -p something 

	---------Output---------------
	Password is wrong

	-->
	------------------------------
#login the with password which consist of less than thre characters 
--> login -l admin -p so
	---------Output---------------
	Errors assosiated with Password:
	Password mustn't be more than 3 characters

	-->
	------------------------------


#User service commands

	user info - shows info about registered user; 

#Examples:

#show user info
--> login -l admin -p admin 
...
~\adminRoot--> user info
	
	---------Output---------------
	User info:
	Login: admin
	Creation date: 17-12-30
	Current Storage Size: 37096
	Max Storage Size: 256000000
	Root directory: ~\adminRoot

	~\adminRoot-->_
	------------------------------
	
#Directory service commands:

Notes.

The storage is located on the disk according to the relative path by folder with .exe. 
It is located in app.Config file. In virtual storage path presents as “~\”.
When you registered in the system, you start working from your root directory, 
which called login+"Root" and its current path is "~\login+"Root"".
Authenticated user can change the current directory using "directory cd <path>" command.
If path contains whiteSpacies, you shoul use double quotes "...".
If path don't have ~ at the beginnin it is considered as absolute path.
In virtual storage you can work only using virtual pathes.
Absolute pathes used during uploading and downloading files.

#Commands:
	directory cd <path> - change working path
	directory info <path> - shows info about directory;
	directory  create <path> <name> - create a directory;
	directory  remove <path>  - delete a directory;
	directory search <path> <pattern> - search a pattern
	directory list <path> - search directories and files in the directory
	directory move <pathSource> <pathDestination> - search directories and files in the directory

#Examples:
--> login -l admin -p admin 
	...
	~\adminRoot-->	
#Initial current path: ~\adminRoot
	
#directory cd <path>:
	
~\adminRoot--> directory cd ..\
	
	---------Output---------------
	Access is denied

	~\adminRoot-->_
	------------------------------
	
~\adminRoot--> directory cd ..\..\..\..\
	
	---------Output---------------
	Access is denied

	~\adminRoot-->_
	------------------------------
	
~\adminRoot--> directory cd ~\PashaRoot
	
	---------Output---------------
	Access is denied

	~\adminRoot-->_
	------------------------------
	
~\adminRoot--> directory cd ~\adminR<oot
	
	---------Output---------------
	Errors assosiated with Path:
	The characters: /,|,*,<,>,\,~" are not allowed

	~\adminRoot-->_
	------------------------------
	~\adminRoot--> directory cd "E:\Godel\new fold\p-lapatin\FileManager\CommonStorage\PashaRoot"

	---------Output---------------
	Access is denied

	~\adminRoot-->_
	------------------------------

~\adminRoot--> directory cd "E:\Godel\p-lapatin\FileManager\CommonStorage\adminRoot"

	---------Output---------------
	Access is denied

	~\adminRoot-->_	
	#You couldn't use absolute paths when you work with virtual storage.
	------------------------------

~\adminRoot--> directory cd .\admin-Dir1-Lvl1\admin-Dir2.1-Lvl2

	---------Output---------------
	~\adminRoot\admin-Dir1-Lvl1\admin-Dir2.1-Lvl2-->_
	------------------------------	

~\adminRoot--> directory cd ..\..\
	
	---------Output---------------
	~\adminRoot-->_
	------------------------------	
	
~\adminRoot--> directory cd ~\adminRoot\admin-Dir1-Lvl1\admin-Dir2.1-Lvl2
	
	---------Output---------------
	~\adminRoot\admin-Dir1-Lvl1\admin-Dir2.1-Lvl2-->_
	------------------------------
	
#directory info <path>:

~\adminRoot--> directory info ~\adminRoot\admin-Dir1-Lvl1\admin-Dir2.1-Lvl2	

	---------Output---------------
	Directory info:
	Directory name: admin-Dir2.1-Lvl2
	Creation date: 16-02-06
	Modification date: 16-02-06
	DirectorySize: 34024
	FullPath: ~\adminRoot\admin-Dir1-Lvl1\admin-Dir2.1-Lvl2
	
	~\adminRoot-->_
	-------------------------------
	
~\adminRoot--> directory info .\

	---------Output---------------
	Directory info:
	Directory name: adminRoot
	Creation date: 16-02-06
	Modification date: 16-02-06
	DirectorySize: 3072
	FullPath: ~\adminRoot
	
	~\adminRoot-->_
	-------------------------------

#Example of directory create <path> <name>:
	
~\adminRoot--> directory create .\ delete1
	
	---------Output---------------
	Directorry created successfully
	
	~\adminRoot-->_
	------------------------------

~\adminRoot--> directory create .\ delete2
	
	---------Output---------------
	Directorry created successfully
	
	~\adminRoot-->_
	------------------------------

~\adminRoot--> directory cd .\delete1
	
	---------Output---------------
	
	~\adminRoot\delete1-->_
	------------------------------	
	
~\adminRoot\delete1--> directory create .\delete3
	
	---------Output---------------
	Directorry created successfully
	
	~\adminRoot\delete1-->_
	------------------------------
	
~\adminRoot\delete1--> directory create .\delete3
	
	---------Output---------------
	Directory with "~\adminRoot\delete1" has already existed
	
	
	~\adminRoot\delete1-->_
	------------------------------
		
~\adminRoot\delete1--> directory create ~\adminRoot\delete5\delete3 delete8
	
	---------Output---------------
	Directory with "~\adminRoot\delete5\delete3" doesn't exist
		
	~\adminRoot\delete1-->_
	------------------------------
	
~\adminRoot\delete1\delete3--> directory cd .\delete3
	
	---------Output---------------
	
	~\adminRoot\delete1-->_
	------------------------------
	
~\adminRoot\delete1\delete3--> directory create .\ delete4	
	...
~\adminRoot\delete1\delete3--> directory create .\ delete5	
	...
~\adminRoot\delete1\delete3--> directory create .\ delete6
	...

#Example of directory search <path> <pattern>:

~\adminRoot--> directory search .\ ete
	
	---------Output---------------
	Search result:
	~\adminRoot\delete1\delete3\delete4
	~\adminRoot\delete1\delete3\delete5
	~\adminRoot\delete1\delete3\delete6
	~\adminRoot\delete1\delete3
	~\adminRoot\delete1
	~\adminRoot\delete2
		
	~\adminRoot-->_
	------------------------------
	
	
#Example of directory list <path>:

~\adminRoot--> directory list .\
	
	---------Output---------------
	There are following catalogs and files in ~\adminRoot:
	admin-Dir1-Lvl1
	admin-Dir2-Lvl1
	delete1
	delete2
	file2.html
	file3.txt
		
	~\adminRoot-->_
	------------------------------

#Example of directory move <path> <path>:

~\adminRoot--> directory move .\delete1 .\admin-Dir1-Lvl1

	---------Output---------------
	Directorry moved successfully
	
	~\adminRoot-->_
	------------------------------
	
~\adminRoot-->directory cd .\delete1

	---------Output---------------
	Directory with "~\adminRoot\delete1" doesn't exist
	
	~\adminRoot-->_
	------------------------------

~\adminRoot-->directory cd .\admin-Dir1-Lvl1\delete1

	---------Output---------------
	
	~\adminRoot\admin-Dir1-Lvl1\delete1-->_
	------------------------------

~\adminRoot\admin-Dir1-Lvl1\delete1-->directory move .\ .\delete3

	---------Output---------------
	Distanation directory is subfolder of source directory
	
	~\adminRoot-->_
	------------------------------
	
#Example of directory remove <path>:

~\adminRoot\admin-Dir1-Lvl1\delete1--> directory remove .\
	
	---------Output---------------
	Directory successfully removed
	
	~\adminRoot-->_
	------------------------------

	~\adminRoot-->directory cd .\admin-Dir1-Lvl1\delete1
	---------Output---------------
	Directory with "~\adminRoot\admin-Dir1-Lvl1\delete1" doesn't exist
	
	~\adminRoot-->_
	------------------------------
	

#File service commands:

#Commands:
	file Upload <pathToFile> <pathToStorage>  - upload external file
	file Download <pathToFile> <pathToStorage>  - download file from storage
	file info <pathToFile>  - show file info
	file  remove <pathToFile> <pathToStorage>  - move file from one directory to another;
	file remove <pathDestination> - remove the file from storage


~\adminRoot--> file info .\file2.html
	
		---------Output---------------
	File info:
	File name: file2
	Creation date: 01-01-01
	Modification date: 16-02-06
	File Size: 2048
	
		
	~\adminRoot-->_
	------------------------------
	
~\adminRoot-->file upload .\file2.html .\
Access is denied
#Path to file should be absolute

~\adminRoot--> file upload E:\Godel\NetMastery\Lab_02_CLR\README.md ~\adminRoot\admin-Dir1-Lvl1
File uploaded successfully


~\adminRoot--> file download  ~\adminRoot\file2.html E:\

~\adminRoot--> file info .\file2.html

~\adminRoot--> file move .\admin-Dir1-Lvl1\admin-Dir2.1-Lvl2\file1.txt .\

~\adminRoot--> file remove .\file1.html


	
