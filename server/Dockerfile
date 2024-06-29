# Use the official .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Set the working directory inside the container
WORKDIR /app

# Copy csproj and restore dependencies
COPY ./API/API.csproj ./API/
RUN dotnet restore ./API/API.csproj

# Copy the remaining source code
COPY . .

# Build the application
RUN dotnet publish ./API/API.csproj -c Release -o out

# Use the official .NET runtime image as a base for production
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime

# Set the working directory inside the container
WORKDIR /app

# Copy the published application from the build stage
COPY --from=build /app/out .

# Expose port 80 and 443 to the outside world
EXPOSE 80
EXPOSE 443

# Start the .NET application with HTTPS
ENTRYPOINT ["dotnet", "API.dll"]
