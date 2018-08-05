Set-Location -Path $PSScriptRoot

# Step 1 - Restore packages
& cmd /c "dotnet restore" | Write-Host

# Step 2 - Build solution
if ($LASTEXITCODE -eq 0) {
  & cmd /c "dotnet build" | Write-Host
}

# Step 3 - Run tests
if ($LASTEXITCODE -eq 0) {
  & cmd /c "dotnet test ./FabricGroup.FamilyTree.Domain.Tests/FabricGroup.FamilyTree.Domain.Tests.csproj --configuration release" | Write-Host
}

# Step 3 - publish website to create the artifacts
if ($LASTEXITCODE -eq 0) {
  & cmd /c "dotnet publish ./FabricGroup.FamilyTree.UI/FabricGroup.FamilyTree.UI.csproj" | Write-Host
}