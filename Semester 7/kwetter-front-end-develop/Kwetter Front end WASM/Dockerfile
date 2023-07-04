FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["Kwetter Front end WASM.csproj", "Kwetter Front end WASM/"]
RUN dotnet restore "Kwetter Front end WASM/Kwetter Front end WASM.csproj"
COPY . .
RUN dotnet build "Kwetter Front end WASM/Kwetter Front end WASM.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Kwetter Front end WASM/Kwetter Front end WASM.csproj" -c Release -o /app/publish

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
RUN ls /app/publish/wwwroot
COPY --from=publish /app/publish/wwwroot .
COPY nginx.conf /etc/nginx/nginx.conf