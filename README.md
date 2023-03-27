
Build:

```
dotnet.sh build -c Release
```

Launch:

```
dotnet run -c Release -- -f "*" -h Toolchain --coreRun $path_to_artifacts_baseline_corerun $path_to_artifacts_testing_corerun --iterationTime 300 --minIterationCount 6 --maxIterationCount 12 --minWarmupCount 6 --maxWarmupCount 12 --unrollFactor 1
```

Used options:
- `-f "*"` - all tests
- `-h Toolchain` - hide "Toolchain" column which contains long path. Use "Job" column to determine toolchain.
- `--coreRun $path_to_artifacts_baseline_corerun $path_to_artifacts_testing_corerun` - point to baseline and testing corerun from local CLR build. The first has to point artifacts from `main` branch and used as baseline. The second points to testing bbranch. 
- `--iterationTime 300 --minIterationCount 6 --maxIterationCount 12 --minWarmupCount 6 --maxWarmupCount 12 --unrollFactor 1` -my tunes for optimal speed/accuracy

**NOTE:** Test are quite long. There was about 2 hrs on my test machine. Test cases can be adjusted by edit `CommonFull.cs`
