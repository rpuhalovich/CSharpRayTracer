@echo off
cd /D "%~dp0"
cd ..

python ./tests/final_scene.py
dotnet run -- -f tests/final_scene.txt -o images/final_scene.png
