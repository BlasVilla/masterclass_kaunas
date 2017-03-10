cd ..
dotnet publish -c Release -o Publishing/published
cd Publishing
docker build -t masterclasslocal/results .
RMDIR "published" /S /Q
pause
