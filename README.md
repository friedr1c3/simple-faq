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
**Important**: I don't commit an SQL database with this project, so you'll need to create one yourself!

**Important**: These instructions are for future reference. This project is incomplete.

1. Download the latest release (i.e published releases or source from **master**).
2. Open Web.config and configure the SQL connection string (**DBConnection**). Do not change the connection string name!
3. Open the **/db/** folder and execute all of the SQL scripts (excluding **install.sql**).
4. Execute the **install** (install.sql) script.
5. Login using the default admin account. Username: admin, Password: _9b27a_#8@ys92n
6. Update the admin account. Change the password, email, etc.

License
============
I haven't decided on a license; I might not even use one. Feel free to use whatever code is available in this project. Some credit would be nice, but I'm not really bothered if you choose not to credit me.