Param
(

    [parameter(Mandatory=$true, ValueFromPipeline=$true)]
    $ProductBinPath
)
$ErrorActionPreference='Stop'

Remove-Item $ProductBinPath\* -Recurse