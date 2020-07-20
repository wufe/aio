# npx fkill-cli Ghoul.Presentation.Web --force
# cd $PSScriptRoot/../Build
# Start-Process ./Ghoul.Presentation.Web -RedirectStandardOutput '.\log.out' -RedirectStandardError '.\log.err'

npx fkill-cli Ghoul.Presentation.Web --force
cd $PSScriptRoot/../
Remove-Item -Recurse -Force ./Build/current
Move-Item -Force ./Build/ongoing ./Build/current
cd ./Build/current
Start-Process ./Ghoul.Presentation.Web -RedirectStandardOutput '.\log.out' -RedirectStandardError '.\log.err'