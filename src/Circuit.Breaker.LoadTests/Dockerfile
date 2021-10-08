FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Circuit.Breaker.LoadTests/Circuit.Breaker.LoadTests.csproj", "Circuit.Breaker.LoadTests/"]
RUN dotnet restore "Circuit.Breaker.LoadTests/Circuit.Breaker.LoadTests.csproj"
COPY . .
WORKDIR "/src/Circuit.Breaker.LoadTests"
RUN dotnet build "Circuit.Breaker.LoadTests.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Circuit.Breaker.LoadTests.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Circuit.Breaker.LoadTests.dll"]
