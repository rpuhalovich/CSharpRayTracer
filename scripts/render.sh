cd $(dirname $0)
cd ..
dotnet run -- -f tests/sample_scene_1.txt -o images/my_images/my_sample_scene_1.png -x 4
dotnet run -- -f tests/sample_scene_2.txt -o images/my_images/my_sample_scene_2.png -x 4

# dotnet run -- -f tests/sample_scene_1.txt -o images/my_images/my_sample_scene_1_hires.png -x 4 -w 1920 -h 1080
# dotnet run -- -f tests/sample_scene_2.txt -o images/my_images/my_sample_scene_2_hires.png -x 4 -w 1920 -h 1080

# dotnet run -- -f tests/sample_scene_1.txt -o images/my_images/hires2.png -x 4 -w 1080 -h 1920