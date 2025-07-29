FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["AppMonitoringService.API.csproj", "."]
RUN dotnet restore "AppMonitoringService.API.csproj"
COPY . .
RUN dotnet build "AppMonitoringService.API.csproj" -c Release -o /app/build
RUN dotnet publish "AppMonitoringService.API.csproj" -c Release -o /app/publish
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 7164
ENTRYPOINT ["dotnet", "AppMonitoringService.API.dll"]