# Build environment:
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as build-environment
WORKDIR /build

# Build project and library layer
COPY NoodleFacts/NoodleFacts.csproj ./
RUN dotnet restore

# Build application layer
COPY NoodleFacts/* ./
RUN dotnet publish -c Release -o out

# Runtime environment:
FROM mcr.microsoft.com/dotnet/core/runtime:3.1
WORKDIR /build

COPY --from=build-environment /build/out .
ENTRYPOINT ["dotnet", "NoodleFacts.dll"]
