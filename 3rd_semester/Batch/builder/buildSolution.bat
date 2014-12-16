@echo off
if "%builderStart%"=="" goto :EOF

echo Building solution...

MSBuild %solution%>%MSBuildlog%

if errorlevel 1 goto :error

echo Building completed.

goto :EOF

:error
set errorInBuilding=true
echo Error build solution.