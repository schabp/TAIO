using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAIO
{
    class Cube
    {
        //  Wymiary prostopadloscianu
        int x, y, z;
        //  Kostki w prostopadloscianie
        Dice[, ,] dices = new Dice[x, y, z];
        //  ilosc kostek z active = true;
        int activeDices;
    }

}
