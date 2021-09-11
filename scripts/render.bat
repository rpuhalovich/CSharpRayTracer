@echo off
cd /D "%~dp0"
cd ..
rem dotnet run -- -f tests/sample_scene_1.txt -o images/my_images/my_sample_scene_1.png
rem dotnet run -- -f tests/sample_scene_2.txt -o images/my_images/my_sample_scene_2.png
rem dotnet run -- -f tests/sample_scene_2.txt -o images/my_images/my_sample_scene_2_dof.png --aperture-radius 0.05 --focal-length 1.5 -w 200 -h 200
dotnet run -- -f tests/sample_scene_2_gloss.txt -o images/my_images/my_sample_scene_2_gloss.png
rem dotnet run -- -f tests/sample_scene_2_emissive1.txt -o images/my_images/sample_scene_2_emissive1.png

rem dotnet run -- -f tests/sample_scene_1.txt -o images/my_images/my_sample_scene_1.png -x 4
rem dotnet run -- -f tests/sample_scene_2.txt -o images/my_images/my_sample_scene_2.png -x 4
