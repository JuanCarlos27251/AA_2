FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar y restaurar dependencias
COPY ["AA2.csproj", "./"]
RUN dotnet restore "AA2.csproj"

# Copiar el resto del código y publicar
COPY . .
RUN dotnet publish "AA2.csproj" -c Release -o /app

FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /app
COPY --from=build /app ./
COPY --from=build /src ./src

# Exponer el puerto
EXPOSE 7251

ENTRYPOINT ["dotnet", "AA2.dll"]