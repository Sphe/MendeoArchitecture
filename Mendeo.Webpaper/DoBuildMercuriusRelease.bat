@ECHO OFF
"C:\Program Files (x86)\MSBuild\14.0\Bin\MsBuild.exe" Master.proj /target:BuildMercuriusWeb /property:Build_Type=Release;ToolsVersion=14.0
PAUSE