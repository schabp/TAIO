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
        //public int bestValue = int.MaxValue;
        //  ilosc ścianek z active=true
        public int activeFaces = 6;
        //  false, gdy activeFaces = 0
        //public bool active = true;
        //  obliczona heurystyka(wartość ważna, tylko gdy bestValue=0)
        public int heuristic;
        //  Prostopadloscian do ktorego nalezy kostka
        //public Cube cube;
        //  pozycja z kostki
        public int x;
        public int y;
        public int z;

        public Dice()
        {
        }

        public Dice(int activeFaces, int heuristic, int x, int y, int z)
        {
            this.activeFaces = activeFaces;
            this.heuristic = heuristic;
            this.x = x;
            this.y = y;
            this.z = z;
        }


        public int this[int pos]
        {
            get
            {
                switch (pos)
                {
                    case 0:
                        return x;
                    case 1:
                        return y;
                    case 2:
                        return z;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        } 

        public int CompareTo(Dice other)
        {
            return heuristic.CompareTo(other.heuristic);
        }

        public IEnumerator<Face> GetEnumerator()
        {
            return faces.Cast<Face>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Dice Clone()
        {
            var ret = new Dice(activeFaces, heuristic, x, y, z);
            faces.CopyTo(ret.faces, 0);
            //for (int i = 0; i < 6; i++)
            //    ret.faces[i] = faces[i];
            return ret;
        }

        //public static bool operator==(Dice a, Dice b)
        //{
        //    if(ReferenceEquals(a,b))
        //        return true;
        //    if (((object)a == null) || ((object)b == null))
        //        return false;
        //    return a.x == b.x && a.y == b.y && a.z == b.z;
        //}

        //public static bool operator !=(Dice a, Dice b)
        //{
        //    return !(a == b);
        //}

        public override string ToString()
        {
            return String.Format("[{0},{1},{2}]", x, y, z);
        }

        public override bool Equals(object obj)
        {
            var d = (Dice) obj;
            return x == d.x && y == d.y && z == d.z;
        }

        public override int GetHashCode()
        {
            return x ^ y ^ z;
        }
    }

}
