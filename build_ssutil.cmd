@echo off

%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild .\src\ssutil-cli\ssutil-cli.csproj /t:Build /p:Configuration=Release /p:TargetFramework=v4.5 /v:M /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false

start /D "src\ssutil-cli\package\" /wait merge.bat

xcopy "src\ssutil-cli\package\ssutil.exe" "dist" /b/v/y