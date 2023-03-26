using System.Collections.Generic;
using System.Numerics;

namespace Benchmarks;

public class ParamsBigint 
{
    //public static readonly int[] sizes = {16, 31, 32, 128, 256, 1024, 4096, 16384, 65536, 262144}; // huge
    public static readonly int[] sizes = {16, 31, 32, 128, 256, 1024, 4096, 16384, 65536}; // normal
    //public static readonly int[] sizes = {16, 31, 256, 4096, 16384}; // short
    //public static readonly int[] sizes = {4096}; // debug-only
    public static List<Entry> AllParamsList = null;

    public static IEnumerable<object[]> AllParams()
    {
        if (AllParamsList == null) {
            List<Entry> newParams = new();

            newParams.Add(new Entry(){
                            Value = BigInteger.Zero,
                            Size = 0,
                            Delta = 0,
                            Name = $"0"
                        });
            newParams.Add(new Entry(){
                            Value = BigInteger.One,
                            Size = 0,
                            Delta = 1,
                            Name = $"1"
                        });

            foreach (int i in sizes)
            {
                var current = BigInteger.One << i;
                for (int j=-1; j<=1; j++)
                {
                    newParams.Add(new Entry(){
                            Value = current + j,
                            Size = i,
                            Delta = j,
                            Name = $"2^({i,7})+({j,2})"
                        }
                    );
                }
            }
            newParams = newParams.OrderBy(o=>o.Value).ToList();

            AllParamsList = newParams;
        }

        for (int i = AllParamsList.Count - 1; i>=0; i--) {
            for (int j = i; j>=0; j--) {
                yield return new object[] {AllParamsList[i], AllParamsList[j]};
            }
        }
       
    }    
}
