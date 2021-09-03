@echo off
cd /D "%~dp0"
cd ..
rem dotnet run -- -f tests/sample_scene_1.txt -o images/my_images/my_sample_scene_1.png
rem dotnet run -- -f tests/sample_scene_2.txt -o images/my_images/my_sample_scene_2.png

dotnet run -- -f tests/sample_scene_1.txt -o images/my_images/my_sample_scene_1.png -x 4
dotnet run -- -f tests/sample_scene_2.txt -o images/my_images/my_sample_scene_2.png -x 4
