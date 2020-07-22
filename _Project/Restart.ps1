npx fkill-cli Aio.Presentation.Web --force
cd $PSScriptRoot/../Build/current
Start-Process ./Aio.Presentation.Web -RedirectStandardOutput '.\log.out' -RedirectStandardError '.\log.err'