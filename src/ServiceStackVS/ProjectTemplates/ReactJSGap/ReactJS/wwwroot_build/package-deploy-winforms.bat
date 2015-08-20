IF EXIST staging-winforms\ (
RMDIR /S /Q .\staging-winforms
)

MD staging-winforms

SET TOOLS=.\tools
SET RELEASE=..\..\$safeprojectname$.AppWinForms\bin\x86\Release
COPY %RELEASE%\$safeprojectname$.AppWinForms.exe .\staging-winforms
COPY %RELEASE%\$safeprojectname$.AppWinForms.exe.config .\staging-winforms
COPY %RELEASE%\CefSharp.BrowserSubprocess.exe .\staging-winforms
ROBOCOPY "%RELEASE%" ".\staging-winforms" *.dll *.pak *.dat /E

IF NOT EXIST apps (
MD apps
)

IF EXIST $safeprojectname$-winforms.7z (
DEL $safeprojectname$-winforms.7z
)

IF EXIST $safeprojectname$-winforms.exe (
DEL $safeprojectname$-winforms.exe
)

cd tools && 7za a ..\$safeprojectname$-winforms.7z ..\staging-winforms\* && cd..
COPY /b .\tools\7zsd_All.sfx + config-winforms.txt + $safeprojectname$-winforms.7z .\apps\$safeprojectname$-winforms.exe

IF EXIST $safeprojectname$-winforms.7z (
DEL $safeprojectname$-winforms.7z
)