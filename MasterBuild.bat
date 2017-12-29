@ECHO OFF
CLS

REM Use the earliest version of MSBuild available
IF EXIST "%ProgramFiles(x86)%\MSBuild\14.0" SET "MSBUILD=%ProgramFiles(x86)%\MSBuild\14.0\bin\MSBuild.exe"
IF EXIST "%ProgramFiles(x86)%\MSBuild\12.0" SET "MSBUILD=%ProgramFiles(x86)%\MSBuild\12.0\bin\MSBuild.exe"

"%MSBUILD%" /nologo /v:m /m "Source\EWSListControls.sln" /t:Clean;Build "/p:Configuration=Release;Platform=Any CPU"

"%MSBUILD%" /nologo /v:m /m "Demos\ListControlDemo.sln" /t:Clean;Build "/p:Configuration=Release;Platform=x86"

IF NOT "%SHFBROOT%"=="" "%MSBUILD%" /nologo /v:m "Doc\EWSoftwareListControls.sln" /t:Clean;Build "/p:Configuration=Release;Platform=Any CPU"

IF "%SHFBROOT%"=="" ECHO **** Sandcastle help file builder not installed.  Skipping help build. ****

CD .\NuGet

NuGet Pack EWSoftware.ListControls.nuspec -NoDefaultExcludes -NoPackageAnalysis -OutputDirectory ..\Deployment

CD ..
