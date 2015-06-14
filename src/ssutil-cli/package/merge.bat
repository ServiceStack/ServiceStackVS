SET TOOLS=..\tools
SET ILMERGE=%TOOLS%\ILMerge.exe
SET RELEASE=..\bin\Release
SET INPUT=%RELEASE%\ssutil-cli.exe
SET INPUT=%INPUT% %RELEASE%\ServiceStack.Text.dll
SET INPUT=%INPUT% %RELEASE%\ServiceStackVS.NativeTypes.dll

%ILMERGE% /target:exe /targetplatform:v4,"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.5" /out:ssutil-cli.exe /ndebug %INPUT% 