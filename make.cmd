@echo off

for %%I in (*.sln) do set TARGET=%%~nI

if not "%1" == "" goto %1
        devenv "%TARGET%.sln" /build Release
        goto end

:debug
        devenv "%TARGET%.sln" /build Debug
        goto end
        
:clean
	devenv "%SLNPATH%" /clean
	goto end

:status
	start yShowVer.exe +c +m +s /rawpath ^
            bin\Debug\yShowVer.exe ^
            bin\Release\yShowVer.exe
        goto end
:end
