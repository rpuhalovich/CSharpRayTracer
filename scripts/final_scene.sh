cd $(dirname $0)
cd ..

python ./tests/final_scene.py
dotnet run -- -f tests/final_scene.txt -o images/final_scene.png
# dotnet run -- -f tests/final_scene.txt -o images/final_scene.png -w 2000 -h 2000 -x 8
