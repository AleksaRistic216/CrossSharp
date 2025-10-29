#!/bin/bash
cd ../tools/version-upgrade
chmod +x ./version-upgrade-commit.sh
npm i
node ./version-upgrade.js