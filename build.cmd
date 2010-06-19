@ECHO OFF

set framework_version=3.5
set msb=%windir%\Microsoft.NET\Framework\v%framework_version%\MSBuild.exe
set project=Common.Configuration\Common.Configuration.csproj
set outdir=Common.Configuration\bin\Release
set releasedir=Release

if not exist %msb% goto msbuild_not_found
goto do_build

:do_build
echo Building the project ...
%msb% /property:Configuration=Release "%project%"
if errorlevel 1 goto build_error
goto build_ok

:build_ok
echo Copying the release files ...
xcopy "%outdir%\*" "%releasedir%" /S /Y > nul
if errorlevel 1 goto copy_error
goto copy_ok

:copy_ok
echo Done.
goto end

:msbuild_not_found
echo MSBuild not found (exptected version of .NET Framework: %framework_version%)
pause
exit 1

:build_error
echo Error while building the project.
pause
exit 2

:copy_error
echo Error while copying the output files to the release directory.
pause
exit 3

:end
exit 0