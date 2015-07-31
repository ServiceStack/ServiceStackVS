RMDIR /S /Q .\staging-winforms
MKDIR staging-winforms

SET TOOLS=.\tools

COPY %RELEASE%\$safeprojectname$.AppWinForms.exe .\staging-winforms	
XCOPY %RELEASE%\*.dll .\staging-winforms	
XCOPY %RELEASE%\*.pak .\staging-winforms
XCOPY %RELEASE%\locales .\staging-winforms