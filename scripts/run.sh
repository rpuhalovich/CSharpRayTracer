cd $(dirname $0)
cd ..

dotnet run -- -f tests/sample_scene_1.txt -o images/_sample_scene_1.png
dotnet run -- -f tests/sample_scene_1.txt -o images/_sample_scene_2.png
