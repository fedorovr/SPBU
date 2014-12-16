@echo off
if "%builderStart%"=="" goto :EOF

if exist %cloneErrors% del %cloneErrors%
if exist %MSBuildlog% del %MSBuildlog%
if exist %sendingErrors% del %sendingErrors%