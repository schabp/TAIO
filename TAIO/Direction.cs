using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Direction
{
    public static class Directions
    {
        public const int S = 0;
        public const int N = 1;
        public const int W = 2;
        public const int E = 3;
        public const int D = 4;
        public const int U = 5;

        public static int Opposite(int dir)
        {
            return dir ^ 1;
        }

        public static IEnumerable<int> GetDirs()
        {
            return new []{S,N,W,E,D,U};
        }

        public static int Operand(int dir)
        {
            if ((dir & 1) == 1)
                return 1;
            return -1;
        }
    }
}