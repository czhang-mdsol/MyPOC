#!/bin/bash

apt-get install -y jq

cat ./web/appsettings.json | \
jq '.MAuth.PrivateKey=env.APP_UUID' | \
jq '.MAuth.ApplicationUuid=env.PRIVATE_KEY' | \
jq '.MAuth.ExceptionPaths=""' \
> ./web/appsettings.json

cat ./web/appsettings.json