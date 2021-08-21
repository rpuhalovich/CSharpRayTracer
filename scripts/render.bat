@echo off
cd /D "%~dp0"
cd ..
dotnet run -- -f tests/sample_scene_1.txt -o images/_sample_scene_1.png -x 4
dotnet run -- -f tests/sample_scene_2.txt -o images/_sample_scene_2.png -x 4
