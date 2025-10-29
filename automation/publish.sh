if \[ -z "$NUGET_KEY" \]; then
  echo "Error: NUGET_KEY is not set."
  exit 1
fi

dotnet nuget push ../bin/*.nupkg --api-key "$NUGET_KEY" --source https://api.nuget.org/v3/index.json --skip-duplicate