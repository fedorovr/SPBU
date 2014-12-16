@echo off
if "%builderStart%"=="" goto :EOF

echo Sending eMail with results...

if %errorInCloning%==true (
  set emailBody=An error occurred during cloning repository.
  set emailSubject=%emailSubject% [Error in cloning repo]
  set emailFile=%RepoCloneErrors%
 )
 
if %errorInBuilding%==true (
  set emailBody=An error occurred during building solution.
  set emailSubject=%emailSubject% [Error in build solution]
 ) 
 
if %errorInChecking%==true (
  set emailBody=File %missingFile% not found during building solution.
  set emailSubject=%emailSubject% [File not found]
 )  

blat -to "%eMailGetter%" -subject "%emailSubject%" -body "%emailBody%" -attach %emailFile%>nul 2>%sendingErrors%

if errorlevel 1 goto :error

echo Sending succeeded.

goto :EOF

:error

set errorInSending=true
echo Error sending.