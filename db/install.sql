/**
 * Simple FAQ SQL installation script.
 *
 * This script must be executed last.
 */

SET NOCOUNT ON;

-- Default users
INSERT INTO Users (Username, Password, Salt, Email, IsAdmin) VALUES('admin', '644AAD0F184FDD3CA0738EFFE293E73393E52DF2FDEF3194E87F65568CEB3AE2', 'E^AudMDAft', 'admin@localhost.com', 1)

-- Default settings
INSERT INTO Settings(Name, Value) VALUES ('SiteName', 'Simple FAQ')
INSERT INTO Settings(Name, Value) VALUES ('RegistrationEnabled', 'true')