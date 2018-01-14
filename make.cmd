@setlocal
@set "PROMPT=$G "
@call :"%1" %2 %3 %4 %5
@endlocal
@exit /b

:devenv
    for %%J in (%*) do for %%I in (*.sln) do devenv.com "%%I" /build %%J
    @exit /b

:"release"
:""
    @call :devenv Release
    @exit /b

:"all"
    @call :devenv Debug Release

:"debug"
    @call :devenv Debug
    @exit /b

:"status"
    @dir /s /b *.exe | findstr /v obj | yShowVer.exe -
    @exit /b

:"install"
    for /F %%I in ('where yShowVer') do copy /-Y bin\Release\yShowVer.exe %%I
    @exit /b

:"package"
    for %%I in (*.sln) do set "TARGET=%%~nI"
    dir /s /b bin\Release\*.exe | findstr /v vshost | zip -j -@ %TARGET%-%DATE:/=%.zip readme.md
    exit /b

:"test"
    bin\Release\yShowVer.exe bin\Release\yShowVer.exe %*
    exit /b
