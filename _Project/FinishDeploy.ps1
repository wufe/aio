npx fkill-cli Aio.Presentation.Web --force
cd $PSScriptRoot/../
Remove-Item -Recurse -Force ./Build/current
Move-Item -Force ./Build/ongoing ./Build/current
cd ./Build/current
Start-Process ./Aio.Presentation.Web -RedirectStandardOutput '.\log.out' -RedirectStandardError '.\log.err'