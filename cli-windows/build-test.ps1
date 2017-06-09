Param([string] $rootPath)
$scriptPath = Split-Path $script:MyInvocation.MyCommand.Path

Write-Host "Current script directory is $scriptPath" -ForegroundColor Yellow

if ([string]::IsNullOrEmpty($rootPath)) {
    $rootPath = "$scriptPath\.." #��ǰĿǰ����һ��Ŀ¼
}
Write-Host "Root path used is $rootPath" -ForegroundColor Yellow

$SolutionFilePath = [IO.Path]::Combine($rootPath, "LindCore.sln")

dotnet restore $SolutionFilePath
Write-Host "$SolutionFilePath restore succcess"
dotnet publish $SolutionFilePath -c Release -o .\obj\publish
