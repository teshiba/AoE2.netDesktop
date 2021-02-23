echo off
rem ===========================================================================
rem coverage.bat
rem ===========================================================================
echo "Start unit test"

rem ===========================================================================
rem Tool path
set REPORTGEN="%USERPROFILE%\.nuget\packages\ReportGenerator\4.8.6\tools\net5.0\ReportGenerator.exe"

rem ===========================================================================
rem target informations

rem output ReportGenerator result directory
set OUTPUT_DIR="Coverage"

rem target filter
set FILTERS="TestCategory!=GUI"

rem ===========================================================================
rem Run test

dotnet test --collect:"XPlat Code Coverage" --filter %FILTERS% | find "coverage.cobertura.xml" > coverageCoberturaPath.txt

rem Set COVERAGE_RESULT variable from Output of the dotnet test.
for /f %%a in (coverageCoberturaPath.txt) do @set COVERAGE_RESULT=%%a&goto :exit_for
:exit_for
del coverageCoberturaPath.txt

rem ===========================================================================
rem Report test result

%REPORTGEN% -reports:%COVERAGE_RESULT% -targetdir:%OUTPUT_DIR%
%OUTPUT_DIR%\index.htm
