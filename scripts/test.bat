@echo off
cd /D "%~dp0"
cd ..
dotnet run -- -f tests/test01.txt -o images/my_images/test01.png -x 4
dotnet run -- -f tests/test02.txt -o images/my_images/test02.png -x 4
dotnet run -- -f tests/test03.txt -o images/my_images/test03.png -x 4
rem dotnet run -- -f tests/test03.txt -o images/my_images/test03_hires.png -x 4 -w 1920 -h 1080
dotnet run -- -f tests/test04.txt -o images/my_images/test04.png -x 4
