using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C5;

namespace TAIO
{
    class Cube: IEnumerable<Dice>
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
        public readonly int StartDices;
        public int this[int pos]
        {
            get
            {
                switch(pos)
                {
                    case 0:
                        return y;
                    case 1:
                        return x;
                    case 2:
                        return z;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        }

        public Func<int, Dice> toOneDim(int a, int b, int dir)
        {
            switch (dir)
            {
                case 0:
                    return c => (Dice)dices.GetValue(a, c, b);
                case 1:
                    return c => (Dice)dices.GetValue(c, a, b);
                case 2:
                    return c => (Dice)dices.GetValue(a, b, c);
                default:
                    throw new IndexOutOfRangeException();
            }
        } 

        public Cube(int x, int y, int z)
        {
            dices = new Dice[x,y,z];
            this.x = x;
            this.y = y;
            this.z = z;
            StartDices = ActiveDices = x*y*z;
        }

    // ustawia heurystyke dla kostki d.
        public void Heuristic(Dice d)
        {
            int x = d.x, y = d.y, z = d.z;
        //  heurystyka jest heurystyką we wszystkich sześciu kierunkach wychodzących z kostki
            d.heuristic = Direction.GetDirs().Sum(dir => Heuristic(x, y, z, dir));
        }

        private int Heuristic(int x, int y, int z, int dir)
        {
            int ret = 0;
        //  zgodnie z osią, czy w przeciwna stronę
            int op = dir.Operand();
        //  Zwraca numer ścianki naprzeciwległej
            int sec = dir.Opposite();
            Func<int, Dice> func;
            int ax = dir.Axis();
            int start;
            switch(ax)
            {
                case 0:
                    start = y;
                    func = toOneDim(x, z, ax);
                    break;
                case 1:
                    start = x;
                    func = toOneDim(y, z, ax);
                    break;
                case 2:
                    start = z;
                    func = toOneDim(x, y, ax);
                    break;
                default:
                    throw new ArgumentException();
            }
        //  Sprawdzamy wszystkie kostki leżace na osi, którą badamy, w kierunku, który badamy
            for(int i = start + op; i >= 0 && i < this[ax]; i += op)
            {
                var d = func(i);
            //  kostka została usunięta
                if (d == null)
                    continue;
            //  pobieramy ściankę zwróconą przodem do naszje kostki
                var f = d.faces[sec];
            //  Usunięcie kostki dla której badamy heurystykę spowoduje zablokowanie usunięcia kostki d poprzez ściankę f
                if (Math.Abs(i - x) == f.startValue + 1)
                    ret += BLOCK_WSP * (6 - d.activeFaces);
            //  Usunięcie kostki, dla której badamy heurystykę pomoże kostce d dojść do stanu możliwego do usunięcia poprzez ściankę f
                else if (Math.Abs(i - x) <= f.startValue)
                    ret += HELP_WSP;
            }
            return ret;
        }

        public IEnumerator<Dice> GetEnumerator()
        {
            return dices.Cast<Dice>().Where(current=>current!=null).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Cube Clone()
        {
            var ret = new Cube(x, y, z){ActiveDices = ActiveDices};
            foreach (var dice in this)
                ret.dices[dice.x, dice.y, dice.z] = dice.Clone(ret);
            return ret;
        }

        public void remove(Dice d, IPriorityQueue<Dice> pq)
        {
            int x = d.x, y = d.y, z = d.z;
            dices[x, y, z] = null;
            foreach(int dir in Direction.GetDirs())
                remove(x, y, z, dir, pq);
        }


        //Pomieszanie prepare z heurystyką.
        //Zachowuje się tak samo jak heurystyka, ale poprawia wartości, a nie tylko liczy jakieś liczby.
        //Czyli po usunięciu kostki zapisuje w kostkach zależnych informacje, czy są zblokowane
        //czy zmienił sie ich status dojścia do usunięcia itp.
        private void remove(int x, int y, int z, int dir, IPriorityQueue<Dice> pq)
        {
            int op = dir.Operand();
            int sec = dir.Opposite();
            Func<int, Dice> func;
            int ax = dir.Axis();
            int start;
            switch (ax)
            {
                case 0:
                    start = y;
                    func = toOneDim(x, z, ax);
                    break;
                case 1:
                    start = x;
                    func = toOneDim(y, z, ax);
                    break;
                case 2:
                    start = z;
                    func = toOneDim(x, y, ax);
                    break;
                default:
                    throw new ArgumentException();
            }
            for (int i = start + op; i >= 0 && i < this[ax]; i += op)
            {
                var d = func(i);
                if(d==null)
                    continue;
                var f = d.faces[sec];
                if (Math.Abs(i - x) == f.startValue + 1)
                {
                    f.active = false;
                    d.activeFaces--;
                    if (d.activeFaces == 0)
                        d.active = false;
                }
                else if (Math.Abs(i - x) <= f.startValue)
                {
                    f.currentValue--;
                    if(d.bestValue > f.currentValue)
                    {
                        d.bestValue = f.currentValue;
                        if(d.bestValue == 0)
                        {
                            Heuristic(d);
                            pq.Add(d);
                        }
                    } 
                }
            }
        }
    }

}
