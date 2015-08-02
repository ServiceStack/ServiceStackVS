IF EXIST staging-console (
RMDIR /S /Q .\staging-console
)

MKDIR staging-console

SET TOOLS=.\tools
SET OUTPUTNAME=$safeprojectname$.Console.exe
SET ILMERGE=%TOOLS%\ILMerge.exe
SET RELEASE=..\..\$safeprojectname$.AppConsole\bin\x86\Release
SET INPUT=%RELEASE%\$safeprojectname$.AppConsole.exe
SET INPUT=%INPUT% %RELEASE%\$safeprojectname$.Resources.dll
SET INPUT=%INPUT% %RELEASE%\$safeprojectname$.ServiceInterface.dll
SET INPUT=%INPUT% %RELEASE%\$safeprojectname$.ServiceModel.dll
SET INPUT=%INPUT% %RELEASE%\ServiceStack.dll
SET INPUT=%INPUT% %RELEASE%\ServiceStack.Text.dll
SET INPUT=%INPUT% %RELEASE%\ServiceStack.Client.dll
SET INPUT=%INPUT% %RELEASE%\ServiceStack.Common.dll
SET INPUT=%INPUT% %RELEASE%\ServiceStack.Interfaces.dll
SET INPUT=%INPUT% %RELEASE%\ServiceStack.OrmLite.dll
SET INPUT=%INPUT% %RELEASE%\ServiceStack.Razor.dll
SET INPUT=%INPUT% %RELEASE%\System.Web.Razor.dll

%ILMERGE% /target:exe /targetplatform:v4,"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.5" /out:staging-console\%OUTPUTNAME% /ndebug %INPUT% 

IF EXIST $safeprojectname$-console.7z (
del $safeprojectname$-console.7z
)

IF EXIST $safeprojectname$-console.exe (
del $safeprojectname$-console.exe
)

cd tools && 7za a ..\$safeprojectname$-console.7z ..\staging-console\* && cd..
copy /b .\tools\7zsd_All.sfx + config-console.txt + $safeprojectname$-console.7z $safeprojectname$-console.exe