@echo off
set outdir=%~1%~2
copy /Y "%~1External\*.dll" "%outdir%"
if not "%~3" == "Release-external" exit
set tools=%~1%~2Tools
if not exist "%tools%" mkdir "%tools%"
del /F /Q "%tools%\*.*"
echo Downloading yt-dlp ...
powershell Invoke-WebRequest -Uri https://github.com/yt-dlp/yt-dlp/releases/latest/download/yt-dlp.exe -OutFile "%tools%\yt-dlp.exe"
echo Downloading FFmpeg ...
powershell Invoke-WebRequest -Uri https://github.com/BtbN/FFmpeg-Builds/releases/latest/download/ffmpeg-master-latest-win64-gpl-shared.zip -OutFile "%tools%\ffmpeg-master-latest-win64-gpl-shared.zip"
mkdir "%tools%\Temp"
echo Extracting FFmpeg ...
powershell Expand-Archive -LiteralPath "%tools%\ffmpeg-master-latest-win64-gpl-shared.zip" -DestinationPath "%tools%\Temp"
move /Y "%tools%\Temp\ffmpeg-master-latest-win64-gpl-shared\bin\*.*" "%tools%"
rmdir /S /Q "%tools%\Temp"
del /F /Q "%tools%\ffplay.exe"
del /F /Q "%tools%\ffmpeg-master-latest-win64-gpl-shared.zip"