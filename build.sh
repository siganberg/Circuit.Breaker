dotnet publish src/ -c Release
docker build -t dependencyone:latest -f src/Circuit.Breaker.DependencyOne/Dockerfile .