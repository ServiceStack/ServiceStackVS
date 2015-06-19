set VSToolsPath=C:\Program Files (x86)\MSBuild\Microsoft\VisualStudio\v12.0

%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild .\src\ServiceStackVS\ServiceStackVS.csproj /t:Build /p:Configuration=Release /p:TargetFramework=v4.5 /v:M /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false

xcopy "src\ServiceStackVS\\bin\Release\ServiceStackVS.vsix" "dist"