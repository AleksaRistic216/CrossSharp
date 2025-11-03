minimal=false
for arg in "$@"; do
  if [ "$arg" = "--minimal" ]; then
    minimal=true
  fi
done
mkdir -p ../bin/demos
rm -rf ../bin/demos/*

mkdir -p ../bin/demos
rm -rf ../bin/demos/*
  
for dir in ../demos/*/; do
  if [ -f "$dir"/*.csproj ]; then
    dotnet build "$dir"/*.csproj -c Release
    all_runtimes=(
          linux-arm linux-arm64 linux-loongarch64 linux-musl-arm linux-musl-arm64 linux-musl-loongarch64
          linux-musl-riscv64 linux-musl-x64 linux-riscv64 linux-x64 linux-x86
          maccatalyst-arm64 maccatalyst-x64 osx osx-arm64 osx-x64 win-arm64 win-x64 win-x86
        )
    minimally_supported_runtimes=(linux-x64 win-x64)
    if [ "$minimal" = true ]; then
      runtimes=($minimally_supported_runtimes)
    else
      runtimes=("${all_runtimes[@]}")
    fi
    for rt in "${runtimes[@]}"; do
      mkdir -p ../bin/demos/$(basename "$dir")-$rt
      
      cp -r "$dir"/bin/${CONFIG:-Debug}/* ../bin/demos/$(basename "$dir")-$rt/
      find ../bin/demos/$(basename "$dir")-$rt/net9.0/runtimes/ -mindepth 1 -maxdepth 1 ! -name "$rt" -exec rm -rf {} +
      
      zip -r ../bin/demos/$(basename "$dir")-$rt.zip ../bin/demos/$(basename "$dir")-$rt &
    done
  fi
done

echo "Waiting for all zipping processes to complete..."
wait
echo "All zipping processes completed."