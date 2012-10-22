
using System;

namespace Direction
{
    public static class DirectionExtension
    {
        public static Direction Opposite(this Direction d)
        {
            return d ^ Direction.O;
        }
    }

    [Flags]
    public enum Direction : byte
    {
        N = 1 << 1,
        S = (1 << 1) | 1,
        E = 1 << 2,
        W = (1 << 2) | 1,
        U = 1 << 3,
        D = (1 << 3) | 1,
        O = 1
    }
}