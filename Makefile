.PHONY: build

RELEASE=Debug
PROJ_FILE=src/Fable.H3.fsproj

clean:
	dotnet clean ${PROJ_FILE}

restore:
	dotnet restore ${PROJ_FILE}

build: 
	dotnet build -c ${RELEASE} ${PROJ_FILE}

build_release: 
	dotnet pack -c ${RELEASE} ${PROJ_FILE} --no-restore

test:
	dotnet test fable.test/fable.test.fsproj --no-restore

release:
	./src/release.sh
