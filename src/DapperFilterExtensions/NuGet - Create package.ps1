# Define some parameters
$TargetPath = (Split-Path $MyInvocation.MyCommand.Path) + "\Output"
$BuildConfiguration = "Release"

# Create target folder if not existing
if((Test-Path $TargetPath) -eq 0) {
	New-Item -ItemType Directory -Force -Path $TargetPath | Out-Null 
}

# Remove all existing .nupkg files
Remove-Item $TargetPath\*.nupkg -recurse

# Find all the *.nuspec files and deliberately omit NuGet packages
$specs = @(Get-ChildItem ".\" -Filter "*.nuspec" -Recurse | `
  ? { $PSItem.FullName -inotmatch "\\packages\\" } | `
  % { $PSItem.FullName } `
)

# Find relevant *.csproj files
foreach ($spec in $specs) {
  $folder = $(Split-Path $spec -Parent)
  $prjs = @(Get-ChildItem $folder -Filter "*.csproj" | % { $PSItem.FullName } )
  if ($prjs.Count -gt 0) {
    $prj = @($prjs)[0]
    ..\.nuget\NuGet pack "$prj" `
      -Properties "Configuration=$BuildConfiguration" `
      -o "$TargetPath"
  }
}