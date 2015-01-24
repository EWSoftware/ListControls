@ECHO OFF
CLS

DEL /Q .\Deployment\*.*

"%WINDIR%\Microsoft.Net\Framework\v4.0.30319\msbuild.exe" /nologo /v:m /m "Source\EWSListControls.sln" /t:Clean;Build "/p:Configuration=Release;Platform=Any CPU"

"%WINDIR%\Microsoft.Net\Framework\v4.0.30319\msbuild.exe" /nologo /v:m /m "Demos\ListControlDemo.sln" /t:Clean;Build "/p:Configuration=Release;Platform=x86"

IF NOT "%SHFBROOT%"=="" "%WINDIR%\Microsoft.Net\Framework\v4.0.30319\msbuild.exe" /nologo /v:m "Doc\EWSoftwareListControls.sln" /t:Clean;Build "/p:Configuration=Release;Platform=Any CPU"

IF "%SHFBROOT%"=="" ECHO **** Sandcastle help file builder not installed.  Skipping help build. ****

CD .\NuGet

NuGet Pack EWSoftware.ListControls.nuspec -NoDefaultExcludes -NoPackageAnalysis -OutputDirectory ..\Deployment

CD ..
