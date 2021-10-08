FROM mcr.microsoft.com/dotnet/aspnet:5.0
EXPOSE 80
WORKDIR /app
COPY "src/Circuit.Breaker.DependencyOne/bin/Release/net5.0/publish/" .

ENTRYPOINT ["dotnet", "Circuit.Breaker.DependencyOne.dll"]