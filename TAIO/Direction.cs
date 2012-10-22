using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace TAIO
{
    public static class Direction
    {
        public const int S = 0;
        public const int N = 1;
        public const int W = 2;
        public const int E = 3;
        public const int D = 4;
        public const int U = 5;

        public static int[] dirs = new[] { S, U, E, S, W, N };
        public static int Opposite(this int dir)
        {
            return dir ^ 1;
        }

        public static IEnumerable<int> GetDirs()
        {
            return dirs;
        }

        public static int Operand(this int dir)
        {
            if ((dir & 1) == 1)
                return 1;
            return -1;
        }

        public static int Axis(this int dir)
        {
            return dir/2;
        }
    }
}