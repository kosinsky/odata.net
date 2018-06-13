Param
(
    [parameter(Mandatory=$true, ValueFromPipeline=$true)]
    $RootFolder,
    [parameter(Mandatory=$true, ValueFromPipeline=$true)]
    $ProductBinPath
)
$ErrorActionPreference='Stop'

$LSBuild = "$RootFolder\tools\Localization\LSBuild\lsbuild.exe"
$Flavor = ".NETPortable\v4.5"
$LCIPath = "$RootFolder\tools\Localization\loc\lci\$Flavor"

$BinariesPath = "$ProductBinPath\$Flavor"

$ProductDlls = "Microsoft.OData.Client.dll",
    "Microsoft.TeamFoundation.OData.Core.dll",
    "Microsoft.TeamFoundation.OData.Edm.dll",
    "Microsoft.TeamFoundation.Spatial.dll"

$Locales = @{1031="de-DE";
             1049="ru-RU"}

ForEach($dll in $ProductDlls)
{
    Write-Host "Localizing $dll ..."
    $originalDll = $dll.Replace("TeamFoundation.","")
    $resourceDll = $dll.Replace(".dll",".resource.dll")
    & $LSBuild parse /l en-US /w 2 /we /vc $LCIPath\$originalDll.lci $BinariesPath\$dll

    ForEach($key in $Locales.keys)
    {
        $loc = $Locales[$key]
        
        & $LSBuild generate /d $key /t "$RootFolder\tools\Localization\loc\lcl\$loc\$Flavor\$originalDll.lcl" /o $BinariesPath\$loc\$resourceDll $BinariesPath\$dll
    }
}


