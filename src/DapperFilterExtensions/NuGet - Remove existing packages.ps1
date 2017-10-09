# Define some parameters
$TargetPath = Split-Path $MyInvocation.MyCommand.Path

# Find all existing .nupkg files (not in packages folder) ...
$specs = @(Get-ChildItem $TargetPath -Filter "*.nupkg" -Recurse | `
  ? { $PSItem.FullName -inotmatch "\\packages\\" } | `
  % { $PSItem.FullName } `
)

[string]$message = "Found " + ($specs.Count).ToString() + " .nupkg file(s)..."
Write-Verbose -Message $message -Verbose

# ... and remove them
foreach ($spec in $specs) {
	Remove-Item $spec
}
