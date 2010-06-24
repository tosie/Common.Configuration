@ECHO OFF

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
