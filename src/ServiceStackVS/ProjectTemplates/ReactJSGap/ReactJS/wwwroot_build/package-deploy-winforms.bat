IF EXIST staging-winforms\ (
RMDIR /S /Q .\staging-winforms
)

MKDIR staging-winforms

SET TOOLS=.\tools
SET RELEASE=..\..\$safeprojectname$.AppWinForms\bin\x86\Release
COPY %RELEASE%\$safeprojectname$.AppWinForms.exe .\staging-winforms
COPY %RELEASE%\CefSharp.BrowserSubprocess.exe .\staging-winforms
ROBOCOPY "%RELEASE%" ".\staging-winforms" *.dll *.pak *.dat /E

IF EXIST $safeprojectname$-winforms.7z (
del $safeprojectname$-winforms.7z
)

IF EXIST $safeprojectname$-winforms.exe (
del $safeprojectname$-winforms.exe
)

cd tools && 7za a ..\$safeprojectname$-winforms.7z ..\staging-winforms\* && cd..
copy /b .\tools\7zsd_All.sfx + config-winforms.txt + $safeprojectname$-winforms.7z $safeprojectname$-winforms.exe