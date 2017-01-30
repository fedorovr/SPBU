@echo off

REM compile source
"C:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe" Sum.cs

REM decompile source to IL file
"C:\Program Files (x86)\Microsoft SDKs\Windows\v8.0A\bin\NETFX 4.0 Tools\ildasm.exe" Sum.exe /out=Sum.il

REM replace addition with subtraction
powershell "(Get-Content Sum.il).replace('add', 'sub').replace('2B', '2D').replace('.+.', '.-.')" > Sub.il

REM compile changed IL
"C:\Windows\Microsoft.NET\Framework\v4.0.30319\ilasm.exe" Sub.il