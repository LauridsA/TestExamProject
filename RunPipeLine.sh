#!/bin/bash -x
THISLOCATION="$( cd "$( dirname "${BASH_SOURCE[0]}" )" >/dev/null 2>&1 && pwd )"

#the vars
SQLFILE=$THISLOCATION/

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
	echo " ===== ===== Building images, API first ===== ====="
	docker build -t api .
	echo " ===== ===== Building images, Testing second ===== ====="
	docker build -t testing -f "DockerfileTest" .

	echo " ===== ===== RUN DB image ===== ====="
	docker run \
		-e 'ACCEPT_EULA=Y' \
   		-e 'SA_PASSWORD=360@NoScopes!' \
   		-p 1433:1433 \
   		--name database \
		--rm \
   		-d microsoft/mssql-server-linux:latest
		
   	echo " ===== ===== Populating database ===== ====="
   	sleep 25
	echo " ===== ===== makedir /backups ===== ====="
   	docker exec -i database mkdir ./backups
   	echo " ===== ===== copy database & tables .bak file into database:/ ===== ====="
   	docker cp $SQLFILE database:/var/opt/mssql/data
	echo " ===== ===== create the docker database from bak file (with seeded data) ===== ====="
   	docker exec -i AT_DB ./opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P '360@NoScopes!' \
 	-Q "RESTORE database test_database FROM DISK = '/var/opt/mssql/data/testing.bak' WITH MOVE 'test_database' TO '/var/opt/mssql/data/migration_bordas_production.mdf', MOVE 'test_database_log' TO '/var/opt/mssql/data/migration_bordas_production_Log.ldf'"

	echo " ===== ===== Start api ===== ====="
	docker run \
	--name api \
	-e "ASPNETENVIRONMENT=Docker"
	-d \
	--rm \
	api
	sleep 25
	echo " ===== ===== check the internal docker network ===== ====="
	docker network inspect bridge
	echo " ===== ===== get the log statements from the api ===== ====="
	docker logs api
	echo " ===== ===== docker run tests against the api ===== ====="
	docker run \
	--name testing \
	--rm \
	testing
	SUCCESS_INDICATOR_Proj=$?
	echo "$SUCCESS_INDICATOR_Proj"
	# Exit with the exit code from the tests which will trigger the onExit function.
	exit "$SUCCESS_INDICATOR_Proj";
}

function stopTests() {
	echo " ===== ===== REMOVING DOCKER CONTAINERS (api) ===== ====="
	docker ps -q --filter "name=api" | grep -q . && docker stop api 
	echo "===== ===== REMOVING DOCKER CONTAINERS (testing) ===== ====="
	docker ps -q --filter "name=testing" | grep -q . && docker stop testing 
	echo " ===== ===== REMOVING DOCKER CONTAINER (DB) ===== ====="
	docker ps -q --filter "name=database" | grep -q . && docker stop database 
}

startTests