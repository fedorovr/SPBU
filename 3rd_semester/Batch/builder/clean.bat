@echo off
if "%builderStart%"=="" goto :EOF

echo Deleting old solution folder...

if not "%repoName%"=="" if exist %repoName% rmdir /q /s %repoName%

echo Deleting completed.