@echo off
if "%builderStart%"=="" goto :EOF

set repoName=SPBU
set solutionName=3rd_semester/C#/Geometry5

set gitURL=https://github.com/fedorovr/%repoName%

set buildFolder=%repoName%\%solutionName%\bin\Debug
set solution=%repoName%\%solutionName%.sln

set checkList=%folder%\checkList.txt

set cloneErrors=CloneErrors.log
set MSBuildlog=MSBuildlog.log
set sendingErrors=SendingErrors.log

set errorInCloning=false
set errorInBuilding=false
set errorInChecking=false
set errorInSending=false

set missingFile=

set emailBody=Successful build the solution.
set emailFile=%logMSBuild%
set emailSubject=Auto-building solution: %solutionName%
set eMailGetter=Roman2x@yandex.ru