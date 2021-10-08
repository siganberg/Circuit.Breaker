FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Circuit.Beaker.Api/Circuit.Beaker.Api.csproj", "Circuit.Beaker.Api/"]
RUN dotnet restore "Circuit.Beaker.Api/Circuit.Beaker.Api.csproj"
COPY . .
WORKDIR "/src/Circuit.Beaker.Api"
RUN dotnet build "Circuit.Beaker.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Circuit.Beaker.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Circuit.Beaker.Api.dll"]
