# 作者：仓储大叔
# 功能：发布项目到指定的地方
Param([string] $rootPath)
$scriptPath = Split-Path $script:MyInvocation.MyCommand.Path

Write-Host "Current script directory is $scriptPath" -ForegroundColor Yellow

if ([string]::IsNullOrEmpty($rootPath)) {
    $rootPath = "$scriptPath\.."
}
 
Write-Host "Root path used is $rootPath" -ForegroundColor Yellow

$projectPaths = 
    @{Path="$rootPath\src\LindCore.Manager";Prj="LindCore.Manager.csproj";Name="web"},
    @{Path="$rootPath\src\LindCore.Test";Prj="LindCore.Test.csproj";Name="console"}
 
$projectPaths | foreach {
    $projectPath = $_.Path
    $projectFile = $_.Prj
	$name=$_.Name
    # $outPath = $_.Path + "\obj\publish"
    # $outPath = "d:\publish\"+$name

	# $outPath = "\\192.168.2.71\发布\"+$name
    $projectPathAndFile = "$projectPath\$projectFile"
    Write-Host "Deleting old publish files in $outPath" -ForegroundColor Yellow
    remove-item -path $outPath -Force -Recurse -ErrorAction SilentlyContinue
	Write-Host "Publishing $projectPath to $outPath" -ForegroundColor Yellow
    dotnet restore $projectPathAndFile
    dotnet build $projectPath
    dotnet publish $projectPath -o $outPath
}
