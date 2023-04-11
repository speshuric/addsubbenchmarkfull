using System.Collections.Generic;
using System.Numerics;

namespace Benchmarks;

public class ParamsBigint 
{

    private static Entry CreateParameterEntry(int n, int delta, bool showZero)
    {
        // hard-coded small values:
        if (n == 0 && delta == -1)
        {
            return new Entry(){
                        Value = BigInteger.Zero, // 2^0-1
                        Size = n,
                        Delta = delta,
                        Name = "0"
                    };
        }

        if (n == 0 && delta == 0)
        {
            return new Entry(){
                        Value = BigInteger.One, // 2^0-1
                        Size = n,
                        Delta = delta,
                        Name = "1"
                    };
        }

        // usual values 2^n+delta :

        var zero = showZero ? "+0" : "";

        var str_delta = delta switch
                {
                    < 0 => $"{delta}",
                    > 0 => $"+{delta}",
                      0 => zero
                };

        var pot = BigInteger.One << n; // Power Of Two, fast implementation

        return new Entry(){
                        Value = pot + delta,
                        Size = n,
                        Delta = delta,
                        Name = $"2^({n,7}){str_delta}"
                    };

    }

    private static Entry[] GetLeftParams()
    {

        //int[] sizes = {31, 32, 64, 128, 256, 512, 1024, 4096, 16384, 65536, 262144};
        int[] sizes = {31, 32, 64, 128, 256, 512, 2048, 4096, 8192, 32768, 131072, 524288, 2097152};

        List<Entry> res = new();

        // hard-coded small values:

        //res.Add(CreateParameterEntry(0, -1, true));
        //res.Add(CreateParameterEntry(0, 0, true));
        //res.Add(CreateParameterEntry(16, 0, true));

        // 2^i+j, i from sizes, j hardcoded :

        foreach (int i in sizes)
        {
            for (int j=-1; j<=0; j++)
            {
                res.Add(CreateParameterEntry(i, j, true));
            }
        }

        return res.ToArray();

    }

    private static Entry[] GetRightParams(Entry left)
    {

        int[] sizes = {31};

        List<Entry> res = new();

        // hard-coded small values:

        //res.Add(CreateParameterEntry(0, -1, false));
        if (left.Value > BigInteger.Zero)
        {
            res.Add(CreateParameterEntry(0, 0, false));
        }
        if (left.Size > 16)
        {
            res.Add(CreateParameterEntry(16, 0, false));
        }


        foreach (int i in sizes)
        {
            if (i < left.Size)
            {
                var entry = CreateParameterEntry(i, 0, false);
                res.Add(entry);
            }
        }

        // half of left length
        var lastSize = sizes[sizes.Length - 1];
        if (left.Size > lastSize * 2)
        {
            var entry = CreateParameterEntry(left.Size / 2, 0, false);
            res.Add(entry);
        }

        // near the left
        if (left.Size > 0)
        {
            for (int j=-1; j<=left.Delta; j++)
            {
                res.Add(CreateParameterEntry(left.Size, j, false));
            }
        }

        return res.ToArray();

    }

    public static IEnumerable<object[]> AllParams()
    {

        var leftParams = GetLeftParams();

        foreach (var left in leftParams)
        {
            var rightParams = GetRightParams(left);

            foreach (var right in rightParams)
            {
                yield return new object[] {left, right};
            }
        }
       
    }    
}
