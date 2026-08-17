FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

EXPOSE 8080
EXPOSE 8081


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# для запуска из docker-compose берется относительно него путь
COPY ["ChemicalLaboratory.csproj", "ChemicalLaboratory.API/"] 
RUN dotnet restore "ChemicalLaboratory.API/ChemicalLaboratory.csproj"

COPY . .
WORKDIR "/src/ChemicalLaboratory.API"
RUN dotnet build "ChemicalLaboratory.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ChemicalLaboratory.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChemicalLaboratory.dll"]
