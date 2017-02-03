param(
	[string]$VersionFilePath
)

# Finds the version number from the product version file and sets it to an environment variable.

$versionContent = Get-Content $VersionFilePath
$result = $versionContent -match "AssemblyVersion\(""((\d+|\.)+)""\)"
$env:GenFxVersion = $matches[1]