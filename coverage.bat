echo off
rem ===========================================================================
rem coverage.bat
rem ===========================================================================
echo "Start unit test"

rem ===========================================================================
rem Tool path
set REPORTGEN="%USERPROFILE%\.nuget\packages\ReportGenerator\4.8.7\tools\net5.0\ReportGenerator.exe"

rem ===========================================================================
rem target informations

rem output ReportGenerator result directory
set OUTPUT_DIR="Coverage"
set HISTORY_DIR="Coverage/History"
set REPORT_FILE_FILTERS="-*.Designer.cs;-*Program.cs"

rem test option
set TEST_FILTERS="TestCategory!=GUI"
set TEST_TIMEOUT_MSEC=60000

rem ===========================================================================
rem Run test
echo on
dotnet test --collect:"XPlat Code Coverage" --filter %TEST_FILTERS% -- RunConfiguration.TestSessionTimeout=%TEST_TIMEOUT_MSEC% > test.log  2>&1
echo off

type test.log

rem Set COVERAGE_RESULT variable from Output of the dotnet test.
type test.log | find "coverage.cobertura.xml" > coverageCoberturaPath.txt
for /f %%a in (coverageCoberturaPath.txt) do @set COVERAGE_RESULT=%%a&goto :exit_for
:exit_for
del coverageCoberturaPath.txt

rem ===========================================================================
rem Report test result
echo on

%REPORTGEN% -filefilters:%REPORT_FILE_FILTERS% -targetdir:%OUTPUT_DIR% -historydir:%HISTORY_DIR% -reports:%COVERAGE_RESULT%
%OUTPUT_DIR%\index.htm
