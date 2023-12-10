# build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source

COPY . .
RUN dotnet restore .
RUN dotnet publish -c debug -o /app --no-restore
# RUN dotnet publish -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app ./

EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "SalesService.dll"]

