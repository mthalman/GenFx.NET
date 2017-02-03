param(
	[string]$AssemblyFilePath
)

$env:GenFxVersion = (Get-Item $AssemblyFilePath).VersionInfo.FileVersion
