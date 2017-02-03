param(
	[string]$AssemblyFilePath
)

$fileVersion = (Get-Item $AssemblyFilePath).VersionInfo.FileVersion

Write-Host ("##vso[task.setvariable variable=GenFxVersion;]$fileVersion");

