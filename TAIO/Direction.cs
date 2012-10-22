using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Direction
{
    public static class Directions
    {
        public const int S = 1 << 1;
        public const int N = S | 1;
        public const int W = 1 << 2;
        public const int E = W | 1;
        public const int D = 1 << 3;
        public const int U = D | 1;

        public static int Opposite(int dir)
        {
            return dir ^ 1;
        }

        public static IEnumerable<int> getDirs()
        {
            yield return S;
            yield return N;
            yield return W;
            yield return E;
            yield return D;
            yield return U;
        }

        public static int Operand(int dir)
        {
            if ((dir & 1) == 1)
                return 1;
            return -1;
        }
    }
}