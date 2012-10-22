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
            Face curFace = d.faces[Direction.Direction.D];
        }

        public int heuristic(int x, int y, int z)
        {
            
        }
    }

}
