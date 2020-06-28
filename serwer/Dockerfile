FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY EkudosAPI.sln ./
COPY EkudosAPI/EkudosAPI.csproj ./EkudosAPI/
COPY KudosSystemRepositories/KudosSystemRepositories.csproj ./KudosSystemRepositories/
COPY AssignKudosContext/AssignKudosContext.csproj ./AssignKudosContext/
COPY DatabaseContext/DatabaseContext.csproj ./DatabaseContext/
COPY EmailGateway/EmailGateway.csproj ./EmailGateway/
COPY docker-compose.dcproj ./
RUN dotnet restore EkudosAPI.sln
COPY . .

WORKDIR /src/AssignKudosContext
RUN dotnet build AssignKudosContext.csproj -c Release

WORKDIR /src/DatabaseContext
RUN dotnet build DatabaseContext.csproj -c Release

WORKDIR /src/KudosSystemRepositories
RUN dotnet build KudosSystemRepositories.csproj -c Release

WORKDIR /src/EmailGateway
RUN dotnet build EmailGateway.csproj -c Release

WORKDIR /src/EkudosAPI
RUN dotnet build EkudosAPI.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "EkudosAPI.dll"]