@ECHO OFF

set framework_version=3.5
set msb=%windir%\Microsoft.NET\Framework\v%framework_version%\MSBuild.exe
set project=Common.Configuration\Common.Configuration.csproj
set outdir=Common.Configuration\obj\Push
set basename=Common.Configuration
set releasedir=Release

if not exist %msb% goto msbuild_not_found
goto do_build

:do_build
echo Building the project ...
if not exist %outdir% mkdir %outdir%
%msb% /target:Build /property:Configuration=Push /property:OutDir=..\%releasedir%\ "%project%"
if errorlevel 1 goto build_error
goto build_ok

:build_ok
goto end

:msbuild_not_found
echo MSBuild not found (exptected version of .NET Framework: %framework_version%)
pause
exit 1

:build_error
echo Error while building the project.
pause
exit 2

:end
pause
exit 0