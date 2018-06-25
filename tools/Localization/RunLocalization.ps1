Param
(
    [parameter(Mandatory=$true, ValueFromPipeline=$true)]
    $RootFolder,
    [parameter(Mandatory=$true, ValueFromPipeline=$true)]
    $ProductBinPath
)
$ErrorActionPreference='Stop'

$LSBuild = "$RootFolder\tools\Localization\LSBuild\lsbuild.exe"
$Flavor = "Desktop"
$LCIPath = "$RootFolder\tools\Localization\loc\lci\$Flavor"
$LSSPATH = "$RootFolder\tools\Localization\loc\lss\default.lss"

$BinariesPath = "$ProductBinPath\$Flavor"

$ProductDlls = # "Microsoft.TeamFoundation.OData.Client.dll", We aren't localizing Client. it used only for E2E test
    "Microsoft.TeamFoundation.OData.Core.dll",
    "Microsoft.TeamFoundation.OData.Edm.dll",
    "Microsoft.TeamFoundation.Spatial.dll"

$Locales = @{1041="ja-JP";
             2052="zh-CN";
             1028="zh-TW";
             1031="de-DE";
             3082="es-ES";
             1036="fr-FR";
             1040="it-IT";
             1042="ko-KR";
             1049="ru-RU";
            }

ForEach($dll in $ProductDlls)
{
    Write-Host "Localizing $dll ..."
    $originalDll = $dll.Replace("TeamFoundation.","")
    $resourceDll = $dll.Replace(".dll",".resource.dll")
    & $LSBuild parse /l en-US /w 2 /p 211 /we /s $LSSPATH /vc $LCIPath\$originalDll.lci /o $BinariesPath\en-US\$dll.lcg  $BinariesPath\$dll

    ForEach($key in $Locales.keys)
    {
        $loc = $Locales[$key]
        
        & $LSBuild generate /d $key /s $LSSPATH /t "$RootFolder\tools\Localization\loc\lcl\$loc\$Flavor\$originalDll.lcl" /ol $BinariesPath\$loc\$dll.lct  /o $BinariesPath\$loc\$resourceDll $BinariesPath\$dll
    }
}


