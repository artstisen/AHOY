<#
.SYNOPSIS
    Builds the AHOY! release files in .\publish:

      Ahoy-<version>-portable-win-x64.zip   Unzip and run. Needs the .NET 10 Desktop Runtime.
      Ahoy-<version>-setup-win-x64.exe      Installer. .NET is included: nothing else to install.
      SHA256SUMS.txt                        Checksums of the files above.

.PARAMETER SkipTests
    Leave out the tests (they run first by default, and a failure stops the build).

.NOTES
    Requires the .NET 10 SDK and Inno Setup 6 (winget install JRSoftware.InnoSetup).
    The files are not code signed: see "Download and run" in README.md.
#>
param([switch]$SkipTests)

$ErrorActionPreference = 'Stop'
$root = $PSScriptRoot
$project = Join-Path $root 'AhoyBrowser\AhoyBrowser.csproj'
$publish = Join-Path $root 'publish'

$version = ([xml](Get-Content $project)).Project.PropertyGroup.Version | Where-Object { $_ } | Select-Object -First 1
if (-not $version) { throw 'No <Version> in AhoyBrowser.csproj' }
Write-Host "AHOY! $version" -ForegroundColor Cyan

if (-not $SkipTests) {
    Write-Host 'Running tests...'
    dotnet test (Join-Path $root 'AhoyBrowser.Tests') -c Release --nologo -v q
    if ($LASTEXITCODE -ne 0) { throw 'Tests failed' }
}

# Find the Inno Setup compiler (per-user or all-users install)
$iscc = @(
    "$env:LOCALAPPDATA\Programs\Inno Setup 6\ISCC.exe",
    "${env:ProgramFiles(x86)}\Inno Setup 6\ISCC.exe",
    "$env:ProgramFiles\Inno Setup 6\ISCC.exe"
) | Where-Object { Test-Path $_ } | Select-Object -First 1
if (-not $iscc) { throw 'Inno Setup 6 not found (winget install JRSoftware.InnoSetup)' }

if (Test-Path $publish) { Remove-Item $publish -Recurse -Force }
New-Item -ItemType Directory $publish | Out-Null

# Common publish settings: no debug symbols, English resources only
$common = @('-c', 'Release', '-r', 'win-x64', '--nologo', '-v', 'q', '-p:DebugType=None', '-p:DebugSymbols=false', '-p:SatelliteResourceLanguages=en')

# 1. Portable: framework-dependent, zipped
Write-Host 'Publishing portable version...'
$portableDir = Join-Path $publish 'portable\AHOY'
dotnet publish $project @common --self-contained false -o $portableDir
if ($LASTEXITCODE -ne 0) { throw 'Publish (portable) failed' }
Copy-Item (Join-Path $root 'LICENSE') (Join-Path $portableDir 'LICENSE.txt')
$portableZip = Join-Path $publish "Ahoy-$version-portable-win-x64.zip"
Compress-Archive -Path $portableDir -DestinationPath $portableZip -CompressionLevel Optimal

# 2. Self-contained (.NET included), packed by the installer
Write-Host 'Publishing self-contained version...'
$selfContainedDir = Join-Path $publish 'selfcontained'
dotnet publish $project @common --self-contained true -o $selfContainedDir
if ($LASTEXITCODE -ne 0) { throw 'Publish (self-contained) failed' }

Write-Host 'Building installer...'
& $iscc /Q "/DAppVersion=$version" "/DSourceDir=$selfContainedDir" "/DOutputDir=$publish" (Join-Path $root 'installer\Ahoy.iss')
if ($LASTEXITCODE -ne 0) { throw 'Inno Setup failed' }
$setup = Join-Path $publish "Ahoy-$version-setup-win-x64.exe"

# 3. Checksums
$sums = foreach ($file in @($portableZip, $setup)) {
    '{0}  {1}' -f (Get-FileHash $file -Algorithm SHA256).Hash.ToLowerInvariant(), (Split-Path $file -Leaf)
}
$sums | Set-Content (Join-Path $publish 'SHA256SUMS.txt') -Encoding ascii

Write-Host ''
Get-Item $portableZip, $setup | ForEach-Object { '{0,-40} {1,8:N1} MB' -f $_.Name, ($_.Length / 1MB) }
$sums
