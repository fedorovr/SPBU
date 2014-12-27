@echo off
if "%builderStart%"=="" goto :EOF

set repoName=Geometry
set solutionName=Geometry
set testsName=GeometrySolverTests

set PathMSBuild="C:\Windows\Microsoft.NET\Framework\v4.0.30319"
set PathVSTest="E:\Program Files (x86)\Microsoft Visual Studio 12.0\Common7\IDE\CommonExtensions\Microsoft\TestWindow"

set gitURL=https://github.com/fedorovr/%repoName%

set buildFolder=%repoName%\%solutionName%\bin\Debug
set solution=%repoName%\%solutionName%.sln
set testsLocation=%repoName%\%testsName%\bin\Debug

set checkList=%folder%\checkList.txt

set cloneErrors=CloneErrors.log
set MSBuildlog=MSBuildlog.log
set testsErrorsLog=TestsErrors.log
set sendingErrors=SendingErrors.log

set testResults=TestResults

set errorInCloning=false
set errorInBuilding=false
set errorInChecking=false
set errorInTesting=false
set errorInSending=false

set missingFile=

set emailBody=Successful build the solution.
set emailFile=%logMSBuild%
set emailSubject=Auto-building solution: %solutionName%
set eMailGetter=Roman2x@yandex.ru