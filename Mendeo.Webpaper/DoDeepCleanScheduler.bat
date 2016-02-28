@ECHO OFF
%WINDIR%\Microsoft.NET\Framework64\v4.0.30319\msbuild.exe Master.proj /target:DeepCleanScheduler
PAUSE