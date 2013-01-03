/**
 * Simple FAQ SQL installation script.
 *
 * This script must be executed last.
 */

SET NOCOUNT ON;

-- Default users
INSERT INTO Users (Username, Password, Salt, Email, IsAdmin) VALUES('admin', 'C0-44-01-AB-86-00-97-FC-E5-A2-54-02-7C-5F-24-A3-F5-72-98-E3-34-8C-88-4E-A8-14-45-D4-5F-71-AF-77', 'Qs@3*=Wnl1', 'admin@localhost.com', 1)

-- Default settings
INSERT INTO Settings(Name, Value) VALUES ('SiteName', 'Simple FAQ')
INSERT INTO Settings(Name, Value) VALUES ('RegistrationEnabled', '1')