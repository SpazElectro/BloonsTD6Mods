@echo off

echo Building...
dotnet build

echo Moving...
move "D:\AAAASteven\btd6 modding\mods\MoreKillMoreSpeed\bin\Debug\net4.7.2\MoreKillMoreSpeed.dll" "D:\SteamLibrary\steamapps\common\BloonsTD6\Mods"
move "D:\AAAASteven\btd6 modding\mods\MoreKillMoreSpeed\bin\Debug\net4.7.2\MoreKillMoreSpeed.pdb" "D:\SteamLibrary\steamapps\common\BloonsTD6\Mods"

echo Starting...
"D:\SteamLibrary\steamapps\common\BloonsTD6\BloonsTD6.exe"

@echo on