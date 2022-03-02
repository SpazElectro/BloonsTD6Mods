@echo off

echo Building...
dotnet build

echo Moving...
move "./bin\Debug\net4.7.2\MoreKillMoreSpeed.dll" "D:\SteamLibrary\steamapps\common\BloonsTD6\Mods"
move "./bin\Debug\net4.7.2\MoreKillMoreSpeed.pdb" "D:\SteamLibrary\steamapps\common\BloonsTD6\Mods"

echo Starting...
"D:\SteamLibrary\steamapps\common\BloonsTD6\BloonsTD6.exe"

@echo on