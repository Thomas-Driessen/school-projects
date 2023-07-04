FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Kwetter Interaction API/Kwetter Interaction API.csproj", "Kwetter Interaction API/"]
RUN dotnet restore "Kwetter Interaction API/Kwetter Interaction API.csproj"
COPY . .
WORKDIR "/src/Kwetter Interaction API"
RUN dotnet build "Kwetter Interaction API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Kwetter Interaction API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kwetter Interaction API.dll"]
