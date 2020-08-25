FROM node:12.18.3-alpine3.9 as spa-builder
WORKDIR /usr/src/app

COPY Neon.Client/package*.json ./

RUN npm install
COPY Neon.Client/ .

RUN npm run-script build:prod


FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY Neon.Server/*.csproj ./
RUN dotnet restore

COPY --from=spa-builder /usr/src/app/dist ./wwwroot

# Copy everything else and build
COPY Neon.Server/ ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Neon.Server.dll"]
