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

        public static int[] dirs = new[] { S, U, E, D, W, N };

    //  Zwraca ściankę przeciwną do zadanej
        public static int Opposite(this int dir)
        {
            return dir ^ 1;
        }

        public static IEnumerable<int> GetDirs()
        {
            return dirs;
        }

    //  Zwraca kierunek ścianki(+1, gdy rosnąca do osi, -1 gdy malejąca)
        public static int Operand(this int dir)
        {
            if ((dir & 1) == 1)
                return 1;
            return -1;
        }

    //  Zwraca oś kostki
        public static int Axis(this int dir)
        {
            return dir/2;
        }
    }
}