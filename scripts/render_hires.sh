cd $(dirname $0)
cd ..
dotnet run -- -f tests/sample_scene_1.txt -o images/my_images/my_sample_scene_1_hires_x8.png -x 8 -w 1000 -h 1000
# dotnet run -- -f tests/sample_scene_2.txt -o images/my_images/my_sample_scene_2_hires_x8.png -x 8 -w 1000 -h 1000

# dotnet run -- -f tests/sample_scene_1.txt -o images/my_images/my_sample_scene_1_hires_x8.png -x 8 -w 2000 -h 2000
# dotnet run -- -f tests/sample_scene_2.txt -o images/my_images/my_sample_scene_2_hires_x8.png -x 8 -w 2000 -h 2000
