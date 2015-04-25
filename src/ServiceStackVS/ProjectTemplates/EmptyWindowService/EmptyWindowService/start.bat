@echo off

REM Script thanks to Ben Gripka and his StackOverflow answer http://stackoverflow.com/a/10052222/670151

:: BatchGotAdmin
:-------------------------------------
REM  --> Check for permissions
>nul 2>&1 "%SYSTEMROOT%\system32\cacls.exe" "%SYSTEMROOT%\system32\config\system"

REM --> If error flag set, we do not have admin.
if '%errorlevel%' NEQ '0' (
    if exist "%temp%\tempCheckSSVS" (
		del "%temp%\tempCheckSSVS"
		echo Failed to run with admin rights.
		exit /B
	)
    echo Requesting administrative privileges...
    goto UACPrompt
) else ( goto gotAdmin )

:UACPrompt
    start http://localhost:8088/
    echo Set UAC = CreateObject^("Shell.Application"^) > "%temp%\getadmin.vbs"
    set params = %*:"=""
    echo UAC.ShellExecute "cmd.exe", "/c %~s0 %params%", "", "runas", 1 >> "%temp%\getadmin.vbs"
	if '%errorlevel%' NEQ '0' (
		echo Failed to request admin rights
		exit /B
	)
	echo tmpCheckSSVS >> "%temp%\tempCheckSSVS"
	if '%errorlevel%' NEQ '0' (
		echo Failed to request admin rights
		exit /B
	)
    "%temp%\getadmin.vbs"
    del "%temp%\getadmin.vbs"
    exit /B

:gotAdmin
    pushd "%CD%"
    CD /D "%~dp0"
:--------------------------------------

SC start $safeprojectname$
PAUSE