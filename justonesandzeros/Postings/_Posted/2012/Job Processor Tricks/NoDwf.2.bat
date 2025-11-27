
cd C:\Program Files\Autodesk\Vault Professional 2013\Explorer

REM Send the stop command to Job Processor
JobProcessor /stop

REM Wait for Job Processor to exit
:RunCheck

tasklist /FI "IMAGENAME eq JobProcessor.exe" 2>NUL | find /I /N "JobProcessor.exe">NUL

if "%ERRORLEVEL%"=="0" goto RunCheck

REM swap in the non-DWF config file
xcopy JobProcessor.exe.noDwf.config JobProcessor.exe.config /Y

REM restart job processor
start JobProcessor

REM Job Processor should be running now and should be ignoring DWF jobs
