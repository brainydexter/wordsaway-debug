#!/bin/bash

cd ..
/Applications/Unity/Unity.app/Contents/MacOS/Unity -quit -logFile Builds/log.txt  -batchmode -executeMethod AutoBuilder.PerformAndroidBuild -projectpath /Users/priyank/pj/unity/wordsAwayDebug/

echo "Done Building unity package"

mv Builds/Android/*.apk ~/"Google Drive/wordsAway/Builds/Android/"
open ~/"Google Drive/wordsAway/Builds/Android"
