find .. -name "*.nupkg" -type f -delete
for dir in ../src/CrossSharp*/; do
  if [ -f "$dir"/*.csproj ]; then
    dotnet pack "$dir"/*.csproj -c Release
  fi
done