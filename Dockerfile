# Use the official .NET runtime as a base image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Use the SDK image for building the project
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TicketMasterAPI/TicketMasterAPI.csproj", "TicketMasterAPI/"]
RUN dotnet restore "TicketMasterAPI/TicketMasterAPI.csproj"
COPY . .
WORKDIR "/src/TicketMasterAPI"
RUN dotnet build "TicketMasterAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TicketMasterAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TicketMasterAPI.dll"]
