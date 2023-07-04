FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Kwetter Security API/Kwetter Security API.csproj", "Kwetter Security API/"]
RUN dotnet restore "Kwetter Security API/Kwetter Security API.csproj"
COPY . .
WORKDIR "/src/Kwetter Security API"
RUN dotnet build "Kwetter Security API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Kwetter Security API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kwetter Security API.dll"]
