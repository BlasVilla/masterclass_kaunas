$scriptpath = Split-Path $script:MyInvocation.MyCommand.path -parent

cd $scriptpath\..

cmd /c ng build -prod -op "$scriptPath\published"

cd $scriptpath

docker build -t masterclasslocal/ui .

Pause
