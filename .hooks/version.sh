#!/bin/sh

VERSION_REGEX='[0-9]\+\.[0-9]\+\.[0-9]\+' # seemver
CURRENT_VERSION=$(cat '.hooks/version' | head -n 1)
