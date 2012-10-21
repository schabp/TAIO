using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAIO
{
    class Dice
    {
        //  Ścianki kostki(0 = N, 1 = E, 2 = W, 3 = S, 4 = U, 5 = D)
        Face[] faces = new Face[6];
        //  Najlepsza wartość na ściankach(nieskończoność, dla active = false)
        int bestValue;
        //  ilosc ścianek z active=true
        int activeFaces;
        //  false, gdy activeFaces = 0
        bool active;
        //  obliczona heurystyka(wartość ważna, tylko gdy bestValue=0)
        int heuristic;
        //  Prostopadloscian do ktorego nalezy kostka
        Cube cube;
        //  pozycja z kostki
        int x, y, z;
    }

}
