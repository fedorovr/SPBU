@echo off
if "%builderStart%"=="" goto :EOF

echo Checking for correct building...

for /F "tokens=*" %%i in (%checkList%) do if not exist "%%i" set missingFile=%%i & goto :error

echo Build is correct.
goto :EOF  

:error
echo Not found: %missingFile%
set errorInChecking=true