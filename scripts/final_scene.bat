@echo off
cd /D "%~dp0"
cd ..

python ./tests/final_scene.py
rem dotnet run -- -f tests/final_scene.txt -o images/final_scene.png --aperture-radius 0.1 --focal-length 6.5 -w 200 -h 200
dotnet run -- -f tests/final_scene.txt -o images/final_scene.png -w 200 -h 200
rem dotnet run -- -f tests/final_scene.txt -o images/final_scene.png -w 1000 -h 1000 -x 4
rem dotnet run -- -f tests/final_scene.txt -o images/final_scene.png -w 2000 -h 2000 -x 8
