[CmdletBinding()]
Param (
    # Convention is running from a root/build folder
    # User can pass in path relative to where calling from,
    # e.g. if in repo root:
    # . build/build.ps1 package
    [String]$PackageDirectory = (Join-Path $PSScriptRoot '../package'),
    [switch]$CI, 
    [switch]$Clean,
    [switch]$NoTest,
    [switch]$TestOnly
    )

# ===== INIT =====
$StartDirectory = Get-Location
# Immediately exit on any error
$ErrorActionPreference = 'Stop'
# Ensure running from where script is located
Set-Location $PSScriptRoot

# ===== FUNCTIONS =====
. ./shared-functions.ps1 

function End() {
    $sw.Stop()
    Write-Host "=========================="
    Write-Host Total elapsed time: $sw.Elapsed.TotalMinutes.ToString("f") minutes -ForegroundColor Green
    Write-Host "=========================="
    Set-Location $StartDirectory
    Exit
}

function CheckForError(){
    if ($Error.Length -gt 0 -Or ($LASTEXITCODE -ne 0 -And -Not([string]::IsNullOrWhiteSpace($LASTEXITCODE)))) {
        End
    }
}

# ===== SETTINGS =====
$Title = "BUILD ALL"
$PackageDirectory = Convert-PathClean $PackageDirectory
if (-Not([System.IO.Path]::IsPathRooted($PackageDirectory))) {
	$PackageDirectory = Join-PathClean $StartDirectory $PackageDirectory
}

Write-H1 $Title
Write-H2 "SETTINGS"
Write-Message "CI                       $CI"
Write-Message "Clean                    $Clean"
Write-Message "NoTest                   $NoTest"
Write-Message "TestOnly                 $TestOnly"
Write-Message "StartDirectory           $StartDirectory"
Write-Message "PackageDirectory         $PackageDirectory"
Write-Message ""

# ===== MAIN =====
$sw = [System.Diagnostics.Stopwatch]::StartNew()

# Environment
./build-environment

# Test
if (-Not($NoTest)) {
    ./test-dotnet `
        -ProjectDirectory ../CFlattSampleApp.UnitTests `
        -PackageDirectory $PackageDirectory `
        -CI:$CI `
        -Clean:$Clean 
}

CheckForError

if ($TestOnly) {
    End
}

# Build and Package
./build-dotnet `
    -ProjectDirectory ../CFlattSampleApp.WebApi `
    -PackageDirectory $PackageDirectory `
    -CI:$CI `
    -Clean:$Clean

CheckForError

# Data
./build-migrations `
    -ProjectDirectory ../CFlattSampleApp.Migrations `
    -StartupProjectDirectory ../CFlattSampleApp.Migrations `
    -DbContextName CFlattSampleAppDbContext `
    -PackageDirectory $PackageDirectory `
    -CI:$CI `
    -Clean:$Clean `

# Exit with stats
End