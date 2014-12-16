if "%builderStart%"=="" goto :EOF

echo Cloning repo...

git clone %gitURL%>nul 2>%cloneErrors%

if errorlevel 1 goto :error

echo Cloning completed.

goto :EOF

:error
set errorInCloning=true
echo Error cloning repo.