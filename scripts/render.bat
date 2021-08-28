@echo off
cd /D "%~dp0"
cd ..
dotnet run -- -f tests/sample_scene_1.txt -o images/my_images/my_sample_scene_1.png
dotnet run -- -f tests/sample_scene_2.txt -o images/my_images/my_sample_scene_2.png
