CONFIG=Release
mkdir -p ../bin/demos
rm -rf ../bin/demos/*

mkdir -p ../bin/demos
rm -rf ../bin/demos/*
  
#all_runtimes=(
#      linux-arm linux-arm64 linux-loongarch64 linux-musl-arm linux-musl-arm64 linux-musl-loongarch64
#      linux-musl-riscv64 linux-musl-x64 linux-riscv64 linux-x64 linux-x86
#      maccatalyst-arm64 maccatalyst-x64 osx osx-arm64 osx-x64 win-arm64 win-x64 win-x86
#    )
for dir in ../demos/*/; do
  if [ -f "$dir"/*.csproj ]; then
    demo_name=$(basename "${dir%/}")
    for os in Windows_NT Linux; do
      if [ "$os" == "Windows_NT" ]; then
        runtimes=(win-x64)
      else
        runtimes=(linux-x64)
      fi
      # Build for the target OS
      dotnet build "$dir"/*.csproj -c "${CONFIG:-Debug}" -p:OS=$os
      for rt in "${runtimes[@]}"; do
        mkdir -p ../bin/demos/${demo_name}-$rt
        cp -r "$dir"/bin/${CONFIG:-Debug}/* ../bin/demos/${demo_name}-$rt/
        # Clean up unneeded runtimes to reduce zip size
        find ../bin/demos/${demo_name}-$rt/net9.0/runtimes/ -mindepth 1 -maxdepth 1 ! -name "$rt" -exec rm -rf {} +
        # Clean up unnecessary libraries from the ./lib folder
        if [[ "$rt" == win-* ]]; then
          find ../bin/demos/${demo_name}-$rt/net9.0/lib -mindepth 1 -maxdepth 1 ! -name '*.dll' -exec rm -f {} +
        else
          find ../bin/demos/${demo_name}-$rt/net9.0/lib -mindepth 1 -maxdepth 1 -name '*.dll' -exec rm -f {} +
        fi
        # Clean all PDB files
        find ../bin/demos/${demo_name}-$rt/ -name '*.pdb' -exec rm -f {} +
        # create run script within the demo folder
        if [[ "$rt" == win-* ]]; then
          echo "@echo off" > ../bin/demos/${demo_name}-$rt/run.bat
          echo "dotnet ./net9.0/${demo_name}.dll \"\$@\"" >> ../bin/demos/${demo_name}-$rt/run.bat
        else
          echo "#!/bin/bash" > ../bin/demos/${demo_name}-$rt/run.sh
          echo "dotnet ./net9.0/${demo_name}.dll \"\$@\"" >> ../bin/demos/${demo_name}-$rt/run.sh
          chmod +x ../bin/demos/${demo_name}-$rt/run.sh
        fi
        ( cd ../bin/demos && zip -r ${demo_name}-$rt.zip ${demo_name}-$rt ) &
      done
    done
  fi
done

echo "Waiting for all zipping processes to complete..."
wait
echo "All zipping processes completed."