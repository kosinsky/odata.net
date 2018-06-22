set Root = %~dp0
set Packages = %Root%\sln\packages
rem set MSBuildFrameworkToolsPath
set MSBuildFrameworkToolsPath=%windir%\Microsoft.Net\Framework\v4.0.30319

rem add it to the PATH, too
set PATH=%MSBuildFrameworkToolsPath%;%PATH%

move %Packages%\Msbuild.Corext.*\ %Packages%\Msbuild.Corext\
set MSBuildLocation=%Packages%\Msbuild.Corext\v15.0\bin\msbuild.exe