@ECHO OFF

set releasedir=Release

echo Building new release ...
cmd /c build.cmd
if errorlevel 1 goto error

echo Adding updated files to git ...
cmd /c git add %releasedir%
if errorlevel 1 goto error

echo Commiting ...
cmd /c git commit -m "New Release"
if errorlevel 1 goto error

echo Pushing ...
cmd /c git push
if errorlevel 1 goto error

echo Done.

:error
pause
exit 1

:end
pause
exit 0
