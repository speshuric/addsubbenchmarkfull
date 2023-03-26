
Build:

```
dotnet.sh build -c Release
```

Launch:

```
dotnet run -c Release -- -f "*" -h Toolchain --allStats --coreRun $path_to_artifacts_baseline_corerun $path_to_artifacts_testing_corerun
```

Used options:
- `-f "*"` - all tests
- `-h Toolchain` - hide "Toolchain" column which contains long path. Use "Job" column to determine toolchain.
- `--coreRun $path_to_artifacts_baseline_corerun $path_to_artifacts_testing_corerun` - point to baseline and testing corerun from local CLR build. The first has to point artifacts from `main` branch and used as baseline. The second points to testing bbranch. 

**NOTE:** Test are quite long. There was about 11 hrs on my test machine. Test cases can be adjusted by edit `int[] sizes` in `CommonFull.cs`
