#!/bin/bash

CONTAINER_ID="8899eb8cbfa7"
DB_USER="kayord"
DB_NAME="kayord"
BACKUP_FILE=backup_`date +%Y-%m-%d"_"%H_%M_%S`.sql

docker exec $CONTAINER_ID pg_dump -U $DB_USER $DB_NAME > $BACKUP_FILE