@ECHO OFF
%WINDIR%\Microsoft.NET\Framework64\v4.0.30319\msbuild.exe Master.proj /target:BuildIrlWeb /property:Build_Type=Release
PAUSE