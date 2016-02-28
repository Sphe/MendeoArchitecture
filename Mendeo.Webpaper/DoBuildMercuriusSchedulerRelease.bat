@ECHO OFF
"C:\Program Files (x86)\MSBuild\14.0\Bin\MsBuild.exe" Master.proj /target:BuildMercuriusScheduler /property:ToolsVersion=14.0
PAUSE