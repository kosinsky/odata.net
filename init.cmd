PUSHD %~dp0
set BUILDDIR=%CD%

rem set toolsets next to where we are
call :SetupToolsDir %BUILDDIR%\sln\packages

rem set MSBuildFrameworkToolsPath
rem set MSBuildFrameworkToolsPath=%windir%\Microsoft.Net\Framework\v4.0.30319
set MSBuildFrameworkToolsPath=%TOOLSDIR%\Msbuild.Corext.15.5.27130\v15.0\sdk


rem add it to the PATH, too
set PATH=%MSBuildFrameworkToolsPath%;%PATH%

rem set MSBuildLocation
set MSBuildLocation=%TOOLSDIR%\Msbuild.Corext.15.5.27130\v15.0\bin\msbuild.exe

rem now add msbuild dir
set PATH=%TOOLSDIR%\Msbuild.Corext.15.5.27130\v15.0\bin;%PATH%

rem add csc.exe dir
set PATH=%TOOLSDIR%\Msbuild.Corext.15.5.27130\v15.0\bin\Roslyn;%PATH%

exit

:SetupToolsDir
set TOOLSDIR=%~f1
if not exist %TOOLSDIR% mkdir %TOOLSDIR%
exit /b 0