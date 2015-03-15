REM This batch is for developers, we do not recomment End User to use it.

@echo off

REM For .NET 3.5

set fdir=%WINDIR%\Microsoft.NET\Framework64
set platform=x64
if not exist %fdir% (
	set fdir=%WINDIR%\Microsoft.NET\Framework
	set playform = x86
)

set csc=%fdir%\v3.5\csc.exe

if not exist bin (mkdir bin)
if not exist "bin\.NET 3.5" (mkdir "bin\.NET 3.5")

if not exist "bin\.NET 3.5\x86" (mkdir "bin\.NET 3.5\x86")
echo Compiling...
%csc% /out:"bin\.NET 3.5\x86\FreeSrun.exe" /o /target:winexe /define:RELEASE /platform:x86 /filealign:512 /nowarn:1607 FreeSrun\Util\*.cs FreeSrun\Properties\*.cs FreeSrun\Forms\*.cs FreeSrun\*.cs

if %platform%==x64 (
	if not exist "bin\.NET 3.5\x64" (mkdir "bin\.NET 3.5\x64")
	echo Compiling...
	%csc% /out:"bin\.NET 3.5\x64\FreeSrun.exe" /o /target:winexe /define:RELEASE /platform:x64 /filealign:512 /nowarn:1607 FreeSrun\Util\*.cs FreeSrun\Properties\*.cs FreeSrun\Forms\*.cs FreeSrun\*.cs
	
)

set csc=%fdir%\v4.0.30319\csc.exe

if not exist "bin\.NET 4.0" (mkdir "bin\.NET 4.0")
if not exist "bin\.NET 4.0\x86" (mkdir "bin\.NET 4.0\x86")

echo Compiling...
%csc% /out:"bin\.NET 4.0\x86\FreeSrun.exe" /o /target:winexe /define:RELEASE /platform:x86 /filealign:512 /nowarn:1607 FreeSrun\Util\*.cs FreeSrun\Properties\*.cs FreeSrun\Forms\*.cs FreeSrun\*.cs

if %platform%==x64 (
	if not exist "bin\.NET 4.0\x64" (mkdir "bin\.NET 4.0\x64")
	%csc% /out:"bin\.NET 4.0\x64\FreeSrun.exe" /o /target:winexe /define:RELEASE /platform:x64 /filealign:512 /nowarn:1607 FreeSrun\Util\*.cs FreeSrun\Properties\*.cs FreeSrun\Forms\*.cs FreeSrun\*.cs
)

echo Compile finish.
pause
exit