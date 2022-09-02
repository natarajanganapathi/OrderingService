restore:
	dotnet restore ./src/OrderingService.sln

clean:
	dotnet clean ./src/OrderingService.sln

build:
	dotnet build ./src/OrderingService.sln
	
run:
	dotnet run --project ./src/Api/Api.csproj

up:
	docker compose up -d