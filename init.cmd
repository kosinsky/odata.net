PUSHD %~dp0
set BUILDDIR=%CD%
set PACKAGES = %BUILDDIR%\sln\packages
rem set MSBuildFrameworkToolsPath
set MSBuildFrameworkToolsPath=%windir%\Microsoft.Net\Framework\v4.0.30319

rem add it to the PATH, too
set PATH=%MSBuildFrameworkToolsPath%;%PATH%

set MSBuildLocation=%PACKAGES%\Msbuild.Corext.15.5.27130\v15.0\bin\msbuild.exe