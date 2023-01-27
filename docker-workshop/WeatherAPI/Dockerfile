# The base image we will be using, this is an image based on Alpine Linux and has aspnet 6 installed
FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app
# Lets docker know the app uses port 8080 which will need to be exposed
EXPOSE 8080

# Environment variables passed in to the app
ENV ASPNETCORE_URLS=http://*:8080

# Our build image used for building our app, has the dotnet 6 sdk installed
FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR "/src/WeatherAPI"
COPY "WeatherAPI.csproj" .
RUN dotnet restore "WeatherAPI.csproj"

# Copy from the source folder into the build image's working folder
COPY . .
RUN dotnet build "WeatherAPI.csproj" -c Release -o /app/build

# Publish image
FROM build AS publish
RUN dotnet publish "WeatherAPI.csproj" -c Release -o /app/publish

# Copy from the publish image into our final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WeatherAPI.dll"]