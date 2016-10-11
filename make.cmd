setlocal
set "SETTING=%~dp0setting.cmd"
if exist "%SETTING%" call "%SETTING%"

if not "%1" == "" goto %1
:release
    for %%I in (*.sln) do devenv %%I /build Release
    goto end

:debug
    for %%I in (*.sln) do devenv %%I /build Debug
    goto end

:status
    dir /s /b *.exe | yShowVer.exe -
    goto end

:install
    if not "%2" == "" set "INSTALL=%~2"
    copy bin\Release\yShowVer.exe "%INSTALL%\."
    goto end

:package
    for %%I in (*.sln) do set "TARGET=%%~nI"
    dir /s /b bin\Release\*.exe | findstr /v vshost | zip -j -@ %TARGET%-%DATE:/=%.zip readme.md
    goto end
:install


:test
    bin\Release\yShowVer.exe bin\Release\yShowVer.exe
:end

echo set "INSTALL=%2" > "%SETTING%"
endlocal
