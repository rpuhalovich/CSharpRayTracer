@echo off
cd /D "%~dp0"
cd ..

python ./tests/final_scene.py
dotnet run -- -f tests/final_scene.txt -o images/final_scene.png
rem dotnet run -- -f tests/final_scene.txt -o images/final_scene.png -w 1000 -h 1000 --aperture-radius 0.1 --focal-length 7.5
