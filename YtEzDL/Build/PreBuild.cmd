@echo off
setlocal EnableDelayedExpansion

cd %1

for /F "tokens=* USEBACKQ" %%F in (`git rev-parse --short HEAD`) do (
set revision=%%F
)

set template=Properties\AssemblyInfo.template.cs
set target=Properties\AssemblyInfo.cs

echo Template: %template%
echo Target: %target%
echo Revision: %revision%

if exist %target% del /f %target%
for /f "tokens=* delims= USEBACKQ" %%L in (`type %template%`) do (
set line=%%L
call set "line=%%line:$revision$=%revision%%%"
echo !line! >> %target%
)