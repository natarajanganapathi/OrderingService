restore:
	dotnet restore ./src/ddd-cqrs.sln

clean:
	dotnet clean ./src/ddd-cqrs.sln

build: clean
	dotnet build ./src/ddd-cqrs.sln
	
run:
	dotnet run --project ./src/Api/Api.csproj

up:
	docker compose up -d