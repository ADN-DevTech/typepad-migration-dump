@ECHO OFF

REM ===========================================================================
REM                          Vault IIS Log Parser
REM 
REM This tool takes Vault IIS log files, imports them into SQL Server and
REM generates reports summarizing the following:
REM  1. Distribution of concurrent user counts
REM  2. Maximum concurrent users by day of year
REM  3. Total KB transferred between client and server by day of year
REM  4. Total file adds/checkins by day of year
REM  5. Operation type breakdown (read, write, unknown)
REM  6. Distinct users by day of year
REM  7. Max and average number of distinct users per day
REM  8. Top 25 web service requests
REM  9. Service usage by request count
REM  10. Top 25 most active users
REM  11. 503 Errors by day of year
REM  12. Top 25 longest running ADMS methods
REM  13. Top 25 longest running ADMS methods on average
REM 
REM Inputs:
REM  1. REQUIRED: SQL Server name (ex. SQLMACHINE or SQLMACHINE\INSTANCE)
REM  2. REQUIRED: Database to create (ex. VaultLogs)
REM  3. OPTIONAL: /d Drop database after generating the report
REM 
REM Preconditions:
REM  1. Specified SQL Server exists
REM  2. Specified database does not exist on the specified SQL Server
REM  3. IIS logs are in a subdirectory of the start-in folder named IISLogs
REM  4. Paths to Logparser, BCP, and SQLCMD are set in the PATH env. variable
REM  5. Windows integrated authentication is enabled on specified SQL Server
REM  6. Windows user running script is a sysadmin on specified SQL Server
REM  7. IIS logs contain the following fields (in no particular order)...
REM     a. date
REM     b. time
REM     c. c-ip
REM     d. cs-uri-stem
REM     e. cs-uri-query
REM     f. cs-method
REM     g. sc-status
REM     h. sc-bytes
REM     i. cs-bytes
REM     j. time-taken
REM 
REM Postconditions:
REM  1. Specified database is created on specified server
REM  2. Report named IISLog.report is created in current directory
REM 
REM Version: 1.0 (31-March-2011)
REM - Initial creation (CDS)
REM Version: 1.1 (11-August-2011)
REM - Added additional query string parameters: vaultName, sessID, app
REM ===========================================================================

SET DROP=NO
IF (%1)==() GOTO :SHOW_USAGE
IF (%1)==("/?") GOTO :SHOW_USAGE
IF (%2)==() GOTO :SHOW_USAGE
IF (%3)==(/d) SET DROP=YES

ECHO Starting processing with SERVER = "%1", DATABASE = "%2", DROP = "%DROP%"
TIME /T
ECHO | SET /p=- Configuring directories...........
IF NOT EXIST .\IISLogsOut MKDIR .\IISLogsOut
IF NOT EXIST .\Out MKDIR .\Out
IF EXIST .\Format.xml DEL .\Format.xml
DEL /Q .\IISLogsOut\*.*
DEL /Q .\Out\*.*
ECHO [DONE]

ECHO | SET /p=- Creating database.................
sqlcmd -S %1 -E -Q "CREATE DATABASE %2"
ECHO [DONE]

ECHO | SET /p=- Setting mode to simple.................
sqlcmd -S %1 -E -Q "ALTER DATABASE %2 SET RECOVERY SIMPLE"
ECHO [DONE]

ECHO | SET /p=- Creating database objects.........
sqlcmd -S %1 -E -d %2 -i Create.sql
ECHO [DONE]

ECHO | SET /p=- Generating BCP format file........
bcp %2..IISData format nul -f Format.xml -x -T -S %1 -c -t ,
ECHO [DONE]

ECHO | SET /p=- Exporting IIS log data............
Logparser.exe "SELECT TO_STRING(date,'yyyy-MM-dd'),STRCAT(STRCAT(TO_STRING(date,'yyyy-MM-dd'), ' '),TO_STRING(time,'hh:mm:ss')) AS datetime,c-ip,IPV4_TO_INT(c-ip) AS c-ip-int,s-ip,EXTRACT_FILENAME(cs-uri-stem) AS adms-service,COALESCE(EXTRACT_VALUE(cs-uri-query, 'op'), EXTRACT_TOKEN(cs-uri-query, 0, '&')) AS adms-method,EXTRACT_VALUE(cs-uri-query, 'uid') AS adms-userid,EXTRACT_TOKEN(EXTRACT_VALUE(cs-uri-query, 'currentCommand'), -1, '.') AS adms-clientcmd,EXTRACT_VALUE(cs-uri-query, 'vaultName') AS adms-vaultname,EXTRACT_VALUE(cs-uri-query, 'sessID') AS adms-session,EXTRACT_VALUE(cs-uri-query, 'app') AS adms-app,cs-method,sc-status,sc-bytes,cs-bytes,time-taken,NULL,0,0,OUT_ROW_NUMBER() INTO .\IISLogsOut\*.txt FROM .\IISLogs\*.log WHERE cs-uri-stem <> '/AutodeskDM/Services/JobService.asmx'" -i:IISW3C -o:CSV -filemode:0 -recurse:-1 -stats:off
ECHO [DONE]

ECHO | SET /p=- Importing IIS log data into SQL...
FOR /F "usebackq delims==" %%i IN (`dir .\IISLogsOut /b /s /o:s`) DO bcp %2..IISData in %%i -e .\Out\%%~nxi.err.log -o .\Out\%%~nxi.out.log -T -f Format.xml -S %1
ECHO [DONE]

ECHO | SET /p=- Processing SQL Server data........
sqlcmd -S %1 -d %2 -E -i Process.sql
ECHO [DONE]

ECHO | SET /p=- Generating report.................
sqlcmd -S %1 -d %2 -E -i Report.sql -o VaultUsage.report -W -s "|"
ECHO [DONE]

IF %DROP%==NO GOTO :SKIP_DROP
ECHO | SET /p=- Dropping database.................
sqlcmd -S %1 -E -Q "DROP DATABASE %2"
ECHO [DONE]

:SKIP_DROP
ECHO Processing complete. Report file is "VaultUsage.report"
GOTO :EXIT

:SHOW_USAGE
ECHO.
ECHO !!!DO NOT USE YOUR VAULT DB SERVER AS THE TARGET FOR THIS SCRIPT!!!
ECHO.
ECHO USAGE:
ECHO   ProcessData.bat ^<server name^> ^<database name^> [/d]
ECHO   ^<server name^>   = name of the SQL Server instance
ECHO   ^<database name^> = name of the database to create for processing data
ECHO   /d              = (optional) drop database after generating report
ECHO.
ECHO EXAMPLES: 
ECHO  (1) Process data in DB named IISLogDb on (local) SQL Server
ECHO      ProcessData.bat (local) IISLogDb
ECHO.
ECHO  (2) Process data in DB named IISLogDb on MACHINE\Instance SQL Server
ECHO      ProcessData.bat MACHINE\Instance IISLogDb
ECHO.
ECHO NOTE: Open this batch file in a text editor for more information on what
ECHO       this script requires and produces.
ECHO.
ECHO !!!DO NOT USE YOUR VAULT DB SERVER AS THE TARGET FOR THIS SCRIPT!!!
ECHO.

:EXIT
TIME /T
