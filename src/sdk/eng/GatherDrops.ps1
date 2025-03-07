[CmdletBinding()]
param(
  [Parameter(Mandatory=$true)]
  [String]$filePath,
  [Parameter(Mandatory=$true)]
  [String]$outputPath,
  [Parameter(Mandatory=$true)]
  [String]$darcPath,
  [String]$githubPat,
  [String]$azdevPat
)
$jsonContent = Get-Content -Path $filePath -Raw | ConvertFrom-Json
foreach ($repo in $jsonContent.repositories) {
    $remoteUri = $repo.remoteUri
    $commitSha = $repo.commitSha
    $path = "$outputPath$($repo.path)"
    $darcCommand = "$darcPath gather-drop -c $commitSha -r $remoteUri --non-shipping --skip-existing --continue-on-error --use-azure-credential-for-blobs -o $path --verbose --ci"
    Write-Output "Gathering drop for $remoteUri"
    Invoke-Expression $darcCommand
}
exit 0