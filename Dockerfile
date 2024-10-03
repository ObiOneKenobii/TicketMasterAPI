# Use the latest .NET 8.0 SDK image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src
COPY ["TicketMasterAPI/TicketMasterAPI.csproj", "TicketMasterAPI/"]
RUN dotnet restore "TicketMasterAPI/TicketMasterAPI.csproj"
COPY . .
WORKDIR "/src/TicketMasterAPI"
RUN dotnet build "TicketMasterAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TicketMasterAPI.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TicketMasterAPI.dll"]
