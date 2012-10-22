using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Direction;

namespace TAIO
{
    class Cube
    {
        private const int HELP_WSP = 2;
        private const int BLOCK_WSP = -1;

        //  Wymiary prostopadloscianu
        public int x;
        public int y;
        public int z;
        //  Kostki w prostopadloscianie
        public Dice[, ,] dices;
        //  ilosc kostek z active = true;
        public int ActiveDices;

        public Cube(int x, int y, int z)
        {
            dices = new Dice[x,y,z];
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public void Heuristic(Dice d)
        {
            int x = d.x, y = d.y, z = d.z;
            d.heuristic = Directions.GetDirs().Sum(dir => Heuristic(x, y, z, dir));
        }

        private int Heuristic(int x, int y, int z, int dir)
        {
            int ret = 0;
            int op = Directions.Operand(dir);
            int sec = Directions.Opposite(dir);
            for(int i = x + op; i >= 0 && i < this.x; i += op)
            {
                var d = dices[i, y, z];
                var f = d.faces[sec];
                if (Math.Abs(i - x) == f.startValue + 1)
                    ret += BLOCK_WSP * (6 - d.activeFaces);
                else if (Math.Abs(i - x) <= f.startValue)
                    ret += HELP_WSP;
            }
            return ret;
        }
    }

}
