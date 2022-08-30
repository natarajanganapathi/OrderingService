FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app
COPY "./Api/Api.csproj" "./Api/Api.csproj" 
# Restore as distinct layers
RUN dotnet restore ./Api/Api.csproj

# Copy everything
COPY . ./
# Build and publish a release
RUN dotnet publish ./Api/Api.csproj -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
EXPOSE 80
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Api.dll"]