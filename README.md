# Run tests with code-coverage
dotnet test --filter TestCategory=unit /p:CollectCoverage=true /p:IncludeTestAssembly=true /p:ExcludeByFile="**/*Migrations/*.cs"

# More detailed coverage
dotnet test --filter TestCategory=unit /p:CollectCoverage=true /p:IncludeTestAssembly=true /p:CoverletOutputFormat=lcov /p:CoverletOutput=./lcov.info /p:ExcludeByFile="**/*Migrations/*.cs"
dotnet tool install -g dotnet-reportgenerator-globaltool
reportgenerator -reports:lcov.info -targetdir:.coverage
Open .coverage/index.html in your browser