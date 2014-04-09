build :
	MSBuild /p:Configuration=Release

ver:
	yShowVer bin\Debug\yShowVer.exe bin\Release\yShowVer.exe

up :
	copy bin\Release\yShowVer.exe $(HOME)\bin\.
