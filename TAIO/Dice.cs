using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAIO
{
    class Dice : IComparable<Dice>, IEnumerable<Face>
    {
        //  Ścianki kostki(0 = N, 1 = E, 2 = W, 3 = S, 4 = U, 5 = D)
        public readonly Face[] faces = new Face[6];
        //  Najlepsza wartość na ściankach(nieskończoność, dla active = false)
        public int bestValue;
        //  ilosc ścianek z active=true
        public int activeFaces = 6;
        //  false, gdy activeFaces = 0
        public bool active=true;
        //  obliczona heurystyka(wartość ważna, tylko gdy bestValue=0)
        public int heuristic;
        //  Prostopadloscian do ktorego nalezy kostka
        public Cube cube;
        //  pozycja z kostki
        public int x, y, z;
        public int CompareTo(Dice other)
        {
            return heuristic.CompareTo(other.heuristic);
        }

        public IEnumerator<Face> GetEnumerator()
        {
            return (IEnumerator<Face>) faces.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

}
