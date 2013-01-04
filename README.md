Simple FAQ
============

Simple ASP.NET MVC 4 FAQ/KB.

Features
============

* User system. (N/A)
* Add questions. (N/A)
* View questions. (N/A)
* Search for questions. (N/A)

Dependencies
============
* See **/packages/** folder for NuGet packages.
* Some components and portions of code taken from [SEDE](http://code.google.com/p/stack-exchange-data-explorer/).
* Metro UI by [metroui.org.ua](http://metroui.org.ua).

Installation
============
**Important**: I don't commit an SQL database with this project, so you'll need to create one yourself and execute the SQL scripts.

**Important**: These instructions are for future reference. This project is incomplete.

1. Download the latest release (i.e published releases or source from **master**).
2. Open Web.config file and configure the SQL connection string (**DBConnection**). Do not change the connection string name!
3. Open the **/db/** folder and execute all of the SQL scripts (do not execute the **install** script).
4. Once all of the tables have been set up, execute the **install** script. This will install default data such as the admin account and application settings.
5. Log in using the default admin account. Username: admin, Password: admin
6. Update the admin account. Change the password, email, etc.

License
============
I haven't decided on a license; I might not even use one. Feel free to use whatever code is available in this project. Some credit would be nice, but I'm not really bothered if you choose not to credit me.