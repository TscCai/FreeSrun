@echo off

set fdir=%WINDIR%\Microsoft.NET\Framework64
set platform=x64
if not exist %fdir% (
	set fdir=%WINDIR%\Microsoft.NET\Framework
	set platform=anycpu
)

set csc=%fdir%\v3.5\csc.exe

if not exist bin (
	mkdir bin
)

%csc% /out:bin/FreeSrun.exe /o /target:winexe /define:RELEASE /platform:%platform% /filealign:512 FreeSrun\Util\*.cs FreeSrun\Properties\*.cs FreeSrun\Forms\*.cs FreeSrun\*.cs
