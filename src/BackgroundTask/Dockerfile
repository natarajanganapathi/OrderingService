FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app
COPY "./BackgroundTask/BackgroundTask.csproj" "./BackgroundTask/BackgroundTask.csproj" 
# Restore as distinct layers
RUN dotnet restore ./BackgroundTask/BackgroundTask.csproj

# Copy everything
COPY . ./
# Build and publish a release
RUN dotnet publish ./BackgroundTask/BackgroundTask.csproj -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
EXPOSE 80
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "BackgroundTask.dll"]