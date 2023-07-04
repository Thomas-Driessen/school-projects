FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
# EXPOSE 80
# EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Kwetter Post API/Kwetter Post API.csproj", "Kwetter Post API/"]
RUN dotnet restore "Kwetter Post API/Kwetter Post API.csproj"
COPY . .
WORKDIR "/src/Kwetter Post API"
RUN dotnet build "Kwetter Post API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Kwetter Post API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kwetter Post API.dll"]
