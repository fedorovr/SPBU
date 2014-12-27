@echo off
if "%builderStart%"=="" goto :EOF

echo Deleting temporary files...

if exist %cloneErrors% del %cloneErrors%
if exist %MSBuildlog% del %MSBuildlog%
if exist %sendingErrors% del %sendingErrors%
if exist %testsErrorsLog% del %testsErrorsLog%

if not "%testResults%"=="" if exist %testResults% rmdir /q /s %testResults%

echo Deleting completed.