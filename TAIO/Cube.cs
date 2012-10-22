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
        //  Wymiary prostopadloscianu
        public int x, y, z;
        //  Kostki w prostopadloscianie
        public Dice[, ,] dices = new Dice[x, y, z];
        //  ilosc kostek z active = true;
        public int activeDices;

        public int heuristic(Dice d)
        {
            int x = d.x, y = d.y, z = d.z;
            return Directions.getDirs().Sum(dir => heuristic(x, y, z, d.faces[dir], dir));
        }

        public int heuristic(int x, int y, int z, Face face, int dir)
        {
            int ret = 0;
            int op = Directions.Operand(dir);
            for(int i = x; i )
        }
    }

}
