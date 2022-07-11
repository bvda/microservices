FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG USERNAME
ARG PASSWORD

WORKDIR /src
RUN dotnet nuget add source --username $USERNAME --password $PASSWORD --store-password-in-clear-text --name github "https://nuget.pkg.github.com/bvda/index.json"
COPY ["ApiGateway/ApiGateway.csproj*", "ApiGateway/"]
RUN dotnet restore "ApiGateway/ApiGateway.csproj"
COPY . .
WORKDIR "/src/ApiGateway"
RUN dotnet build "ApiGateway.csproj" -c Release -o /app/publish

FROM build as publish
RUN dotnet publish "ApiGateway.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiGateway.dll"]