$folders = Get-ChildItem -Directory;

for ($i=0; $i -lt $folders.length; $i++) {
	$f = $folders[$i];
	
	if ($i -eq 0) {
		dotnet test $f/$f.csproj /p:CollectCoverage=true /p:CoverletOutput=../TestResults/ 
	} else {
		dotnet test $f/$f.csproj /p:CollectCoverage=true /p:CoverletOutput=../TestResults/ /p:MergeWith=../TestResults/coverage.json /p:CoverletOutputFormat="opencover"
	}
}
$currentFolder = Get-Location;
$toolsFolder = "$f\tools";
if(!(Test-Path $toolsFolder)){
	dotnet tool install dotnet-reportgenerator-globaltool --tool-path tools
}
.\tools\reportgenerator.exe -reports:.\TestResults\coverage.opencover.xml -targetdir:.\TestResults\
start .\TestResults\index.htm