using BenchmarkDotNet.Attributes;
using System.Numerics;
using System.Diagnostics;

namespace Benchmarks;

public struct Entry
{
    public string Name;
    public int Size;
    public int Delta;
    public BigInteger Value;
    public override string ToString()
    {
        return Name;
    }
}

//[InvocationCount(20000, 1)] // fast for debug
public class BigintBenchmark
{
    public static IEnumerable<object[]> GetSizes() => ParamsBigint.AllParams();

    [Benchmark]
    [ArgumentsSource(nameof(GetSizes))]
    public BigInteger Add(Entry left, Entry right)
    {
        return right.Value + left.Value;
    }

    [Benchmark]
    [ArgumentsSource(nameof(GetSizes))]
    public BigInteger Sub(Entry left, Entry right)
    {
        return left.Value - right.Value;
    }
}
