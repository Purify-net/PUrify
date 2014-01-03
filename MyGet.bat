@echo Off
set config=%1
if "%config%" == "" (
   set config=Release
)

set version=
if not "%PackageVersion%" == "" (
   set version=-Version %PackageVersion%
)

REM Package Restore
%NuGet% install test\PUrify.Tests\packages.config -o %cd%\packages -NonInteractive
if not "%errorlevel%"=="0" goto failure

REM Build
%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild test\PUrify.Tests\PUrify.Tests.csproj /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false
if not "%errorlevel%"=="0" goto failure

REM Unit tests
"%GallioEcho%" test\PUrify.Tests\bin\%config%\PUrify.Tests.dll
if not "%errorlevel%"=="0" goto failure

REM Package
mkdir Build
cmd /c %nuget% pack src\PUrify\PUrify.csproj -symbols -o Build -p Configuration=%config% %version%
if not "%errorlevel%"=="0" goto failure

:success
exit 0

:failure
exit -1
