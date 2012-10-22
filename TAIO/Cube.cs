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

        public void heuristic(Dice d)
        {
            int x = d.x, y = d.y, z = d.z;
            d.heuristic = Directions.getDirs().Sum(dir => heuristic(x, y, z, dir));
        }

        public int heuristic(int x, int y, int z, int dir)
        {
            int ret = 0;
            int op = Directions.Operand(dir);
            int sec = Directions.Opposite(dir);
            for(int i = x + op; i >= 0 && i <= this.x; i+=op)
            {
                Dice d = dices[i, y, z];
                Face f = d.faces[sec];

            }
        }
    }

}
