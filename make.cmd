setlocal
if not "%1" == "" goto %1
:release
    for %%I in (*.sln) do devenv %%I /build Release
    goto end

:debug
    for %%I in (*.sln) do devenv %%I /build Debug
    goto end

:status:
    dir /s /b *.exe | yShowVer.exe -
    goto end

:package:
    for %%I in (*.sln) do set "TARGET=%%~nI"
    dir /s /b bin\Release\*.exe | findstr /v vshost | zip -j -@ %TARGET%-%DATE:/=%.zip readme.md
    goto end
:end
endlocal
