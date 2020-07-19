npx fkill-cli Ghoul.Presentation.Web --force
cd $PSScriptRoot/../Build
Start-Process ./Ghoul.Presentation.Web -RedirectStandardOutput '.\log.out' -RedirectStandardError '.\log.err'