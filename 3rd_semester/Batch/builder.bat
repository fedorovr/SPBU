@echo off
set builderStart=true

set folder=builder

call %folder%\settings.bat

call %folder%\clean.bat

call %folder%\cloneRepo.bat
if "%errorInCloning%"=="true" goto :end

call %folder%\buildSolution.bat
if "%errorInBuilding%"=="true" goto :end

call %folder%\checkBuild.bat

call %folder%\runTests.bat

:end

call %folder%\mailSender.bat

call %folder%\finalClean.bat

echo Build Finished.