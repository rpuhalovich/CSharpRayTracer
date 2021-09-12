@echo off
cd /D "%~dp0"
cd ..

dotnet run -- -f tests/sample_scene_1.txt -o images/my_images/my_sample_scene_1.png
dotnet run -- -f tests/sample_scene_1.txt -o images/my_images/my_sample_scene_1_x4.png -x 4
dotnet run -- -f tests/sample_scene_2.txt -o images/my_images/my_sample_scene_2.png
dotnet run -- -f tests/sample_scene_2.txt -o images/my_images/my_sample_scene_2_x4.png -x 4

dotnet run -- -f tests/sample_scene_2_gloss.txt -o images/my_images/my_sample_scene_2_gloss.png
dotnet run -- -f tests/sample_scene_2_gloss.txt -o images/my_images/my_sample_scene_2_gloss_dof.png --aperture-radius 0.05 --focal-length 1.5

dotnet run -- -f tests/final_scene.txt -o images/my_images/final_scene_test.png -w 200 -h 200 --aperture-radius 0.1 --focal-length 7.5
