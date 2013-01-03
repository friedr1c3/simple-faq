/**
 * Simple FAQ SQL installation script.
 *
 * This script must be executed last.
 */

SET NOCOUNT ON;

-- Default users
INSERT INTO Users (Username, Password, Salt, Email, IsAdmin) VALUES('admin', '', '', 'admin@localhost.com', 1)

-- Default settings
INSERT INTO Settings(Name, Value) VALUES ('SiteName', 'Simple FAQ')
INSERT INTO Settings(Name, Value) VALUES ('RegistrationEnabled', '1')