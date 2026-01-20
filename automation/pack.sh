find .. -name "*.nupkg" -type f -delete
for dir in ../src/CrossSharp*/; do
  if [ -f "$dir"/*.csproj ]; then
    dotnet pack "$dir"/*.csproj -c "${CONFIG:-Debug}"
  fi
done

# Pack templates
dotnet pack ../templates/CrossSharp.Templates.csproj -c "${CONFIG:-Debug}" -o ../bin