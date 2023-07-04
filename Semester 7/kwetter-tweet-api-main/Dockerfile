FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Kwetter Post API/Kwetter Post API.csproj", "Kwetter Post API/"]
COPY ["Kwetter Post API.Core/Kwetter Post API.Core.csproj", "Kwetter Post API.Core/"]
COPY ["Kwetter Post API.DAL/Kwetter Post API.DAL.csproj", "Kwetter Post API.DAL/"]
RUN dotnet restore "Kwetter Post API/Kwetter Post API.csproj"
RUN dotnet restore "Kwetter Post API.Core/Kwetter Post API.Core.csproj"
RUN dotnet restore "Kwetter Post API.DAL/Kwetter Post API.DAL.csproj"
COPY . .
WORKDIR "/src/Kwetter Post API"
RUN dotnet build "Kwetter Post API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Kwetter Post API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kwetter Post API.dll"]
