cd $PSScriptRoot/../Presentation/Ghoul.Presentation.Web/Frontend
yarn
yarn build
cd ..
dotnet build -c Release -o ../../Build/ -r linux-x64