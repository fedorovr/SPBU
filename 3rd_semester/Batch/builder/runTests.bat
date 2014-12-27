@echo off
if "%builderStart%"=="" goto :EOF

echo Running tests...

%PathVSTest%\vstest.console.exe %testsLocation%\%testsName%.dll>nul 2>%testsErrorsLog%
::%PathVSTest%\vstest.console.exe %testsLocation%\%testsName%.dll>%testsLog%>nul 2>%testsErrorsLog% 
if ERRORLEVEL 1 goto :error

echo All tests passed successfully.
goto :EOF

:error
set errorInTesting=true
echo Tests failed.