#!/bin/bash -x
THISLOCATION="$( cd "$( dirname "${BASH_SOURCE[0]}" )" >/dev/null 2>&1 && pwd )"

#the vars

DOCKER_COMPOSE_BASE_FILE=$THISLOCATION/../../docker-compose.yml
DOCKER_COMPOSE_FILE=$THISLOCATION/../../docker-compose.override.AT.yml
DOCKER_COMPOSE_NAME=$TIM_AT

# Stop on first error
set -e;

function onExit {
	if [ "$?" != "0" ]; then
		echo ""; #Newline
        echo "===== TESTS FAILED! =====";
		echo ""; #Newline
		stopTests
        # Tests failed, return an error code
        exit 1;
    else
        echo ""; #Newline
		echo "===== TESTS PASSED =====";
		echo ""; #Newline
		stopTests
        # Return 0 to indicate success
    fi
}

# call onExit when the script exits
trap onExit EXIT;

function startTests() {
	echo "Building images, API first"
	docker build -t API -f "Dockerfile"
	echo "Building images, Testing second"
	docker build -t TESTING -f "DockerfileTest"

	echo "RUN DB image"
	docker run \
		-e 'ACCEPT_EULA=Y' \
   		-e 'SA_PASSWORD=360@NoScopes!' \
   		-p 1433:1433 \
   		--name Database \
		--rm \
   		-d microsoft/mssql-server-linux:latest
		
   	echo "Populating Database"
   	sleep 25
	echo "makedir /backups"
   	docker exec -i Database mkdir /backups
   	echo "copy database & tables .bak files into Database:/"
   	docker cp $SQLFILE Database:/var/opt/mssql/data
	docker cp $SQLINSERTFILE Database:/var/opt/mssql/data
	echo "create the docker database"
   	docker exec -i AT_DB ./opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P '360@NoScopes!' \
 	-Q "RESTORE DATABASE migration_bordas_production FROM DISK = '/var/opt/mssql/data/testing.bak' WITH MOVE 'migration_bordas_production' TO '/var/opt/mssql/data/migration_bordas_production.mdf', MOVE 'migration_bordas_production_log' TO '/var/opt/mssql/data/migration_bordas_production_Log.ldf'"
	# docker exec -i Database /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P '360@NoScopes!' \
   	# -i tableCreationTIM.sql
	echo "load the static data into the docker database"

   	# docker exec -i Database /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P '360@NoScopes!' \
   	# -i InsertTestTimData.sql

	echo "Start API"
	docker run \
	--name API \
	-d \
	--rm \
	API
	sleep 25
	echo "check the internal docker network"
	docker network inspect bridge
	echo "get the log statements from the API"
	docker logs API
	echo "docker run tests against the API"
	docker run \
	--name TESTING \
	--rm \
	TESTING
	SUCCESS_INDICATOR_Proj=$?
	echo "$SUCCESS_INDICATOR_Proj"
	# Exit with the exit code from the tests which will trigger the onExit function.
	exit "$SUCCESS_INDICATOR_Proj";
}

function stopTests() {
	echo "== REMOVING DOCKER CONTAINERS (API) =="
	docker ps -q --filter "name=API" | grep -q . && docker stop API 
	echo "== REMOVING DOCKER CONTAINERS (TESTING) =="
	docker ps -q --filter "name=TESTING" | grep -q . && docker stop TIM_AT 
	echo "== REMOVING DOCKER CONTAINER (DB) =="
	docker ps -q --filter "name=Database" | grep -q . && docker stop Database 
}

startTests