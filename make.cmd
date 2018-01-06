setlocal
set "PROMPT=$G "
call :"%1" %2
endlocal
exit /b

:"release"
:""
    for %%I in (*.sln) do devenv.com "%%I" /build Release
    exit /b

:"debug"
    for %%I in (*.sln) do devenv.com "%%I" /build Debug
    exit /b

:"status"
    dir /s /b *.exe | yShowVer.exe -
    exit /b

:"install"
    if not "%1" == "" set "INSTALL=%~2"
    if not "%INSTALL%" == "" copy bin\Release\yShowVer.exe "%INSTALL%\."
    exit /b

:"package"
    for %%I in (*.sln) do set "TARGET=%%~nI"
    dir /s /b bin\Release\*.exe | findstr /v vshost | zip -j -@ %TARGET%-%DATE:/=%.zip readme.md
    exit /b

:"test"
    bin\Release\yShowVer.exe bin\Release\yShowVer.exe
    exit /b
