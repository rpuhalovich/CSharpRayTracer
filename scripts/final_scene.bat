@echo off
cd /D "%~dp0"
cd ..
dotnet run -- -f tests/final_scene.txt -o images/final_scene.png
rem dotnet run -- -f tests/final_scene.txt -o images/final_scene.png -w 2000 -h 2000 -x 8
