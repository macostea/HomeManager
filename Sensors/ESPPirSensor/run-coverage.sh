#!/bin/bash

pio test -e native_cov
lcov -d . -c -o coverage.info
lcov -r "coverage.info" "*ArduinoJson.h" "*test*" "/root*" "/usr*" "8/*" -o "coverage-filtered.info"

genhtml -o coverage.html coverage-filtered.info

