cd ..
dotnet publish -c Release -o Publishing/published
cd Publishing
docker build -t masterclasslocal/requests .
RMDIR "published" /S /Q
pause
