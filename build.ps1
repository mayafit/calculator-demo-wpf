# Calculator Demo Build Script
# This script helps build and run the Calculator Demo application

param(
    [switch]$Clean,
    [switch]$Test,
    [switch]$Release,
    [switch]$Help
)

if ($Help) {
    Write-Host @"
Calculator Demo Build Script

Usage: .\build.ps1 [options]

Options:
    -Clean      Clean the build output before building
    -Test       Run tests after building
    -Release    Build in Release configuration
    -Help       Show this help message

Examples:
    .\build.ps1                    # Build in Debug mode
    .\build.ps1 -Clean             # Clean and build
    .\build.ps1 -Test              # Build and run tests
    .\build.ps1 -Release -Test     # Build in Release mode and run tests
"@
    exit 0
}

Write-Host "Calculator Demo - Build Script" -ForegroundColor Green
Write-Host "=================================" -ForegroundColor Green

# Set configuration
$Configuration = if ($Release) { "Release" } else { "Debug" }
Write-Host "Configuration: $Configuration" -ForegroundColor Yellow

# Clean if requested
if ($Clean) {
    Write-Host "Cleaning build output..." -ForegroundColor Yellow
    dotnet clean --configuration $Configuration
    if ($LASTEXITCODE -ne 0) {
        Write-Host "Clean failed!" -ForegroundColor Red
        exit $LASTEXITCODE
    }
}

# Restore packages
Write-Host "Restoring packages..." -ForegroundColor Yellow
dotnet restore
if ($LASTEXITCODE -ne 0) {
    Write-Host "Package restore failed!" -ForegroundColor Red
    exit $LASTEXITCODE
}

# Build the project
Write-Host "Building project..." -ForegroundColor Yellow
dotnet build --configuration $Configuration --no-restore
if ($LASTEXITCODE -ne 0) {
    Write-Host "Build failed!" -ForegroundColor Red
    exit $LASTEXITCODE
}

Write-Host "Build completed successfully!" -ForegroundColor Green

# Run tests if requested
if ($Test) {
    Write-Host "Running tests..." -ForegroundColor Yellow
    dotnet test --configuration $Configuration --no-build
    if ($LASTEXITCODE -ne 0) {
        Write-Host "Tests failed!" -ForegroundColor Red
        exit $LASTEXITCODE
    }
    Write-Host "Tests completed successfully!" -ForegroundColor Green
}

# Ask if user wants to run the application
Write-Host ""
$runApp = Read-Host "Do you want to run the Calculator Demo application? (y/n)"
if ($runApp -eq "y" -or $runApp -eq "Y") {
    Write-Host "Starting Calculator Demo..." -ForegroundColor Yellow
    dotnet run --configuration $Configuration --no-build
}

Write-Host "Build script completed!" -ForegroundColor Green 