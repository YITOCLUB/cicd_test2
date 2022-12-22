#!/bin/bash
##MMMMMMMM
###1219f11
rm -f -r ./utestresults ./coveragereport
dotnet test ./TestA.Tests/TestA.Tests.csproj --logger trx --results-directory ./utestresults  --collect:"XPlat Code Coverage"
if [ $? -ne 0 ]; then
    echo "◆◆単体試験エラー◆◆"
    exit 1
fi
reportgenerator -reports:"./utestresults/*/coverage.cobertura.xml" -targetdir:"./coveragereport" -reporttypes:Html
open -a Google\ Chrome ./coveragereport/index.html
echo "◆◆単体試験完了◆◆"

