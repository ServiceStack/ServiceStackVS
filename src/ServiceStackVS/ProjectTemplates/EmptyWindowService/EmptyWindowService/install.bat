REM INSTALL THIS WINDOWS SERVICE:
REM 1. Build in Release mode

SET INSTALL_UTL="%SYSTEMROOT%\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe"

%INSTALL_UTL% bin\Release\$safeprojectname$.exe