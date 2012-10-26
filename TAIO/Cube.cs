using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TAIO
{
    internal class Cube : IEnumerable<Dice>
    {
        private const int HELP_WSP = 4;
        private static readonly int[] BLOCK_WSP = new [] {-40, -15, -10, -5, 0, 0, 0, 0};
        public readonly int StartDices;
        public int ActiveDices;
        public readonly Dice[,,] dices;

        //  Wymiary prostopadloscianu
        private readonly int x;
        private readonly int y;
        private readonly int z;

        public Cube(int x, int y, int z)
        {
            dices = new Dice[x,y,z];
            this.x = x;
            this.y = y;
            this.z = z;
            StartDices = ActiveDices = x*y*z;
        }

        //  Kostki w prostopadloscianie

        public int this[int pos]
        {
            get
            {
                switch (pos)
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

        #region IEnumerable<Dice> Members

        public IEnumerator<Dice> GetEnumerator()
        {
            return dices.Cast<Dice>().Where(current => current != null).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        private Func<int, Dice> toOneDim(int a, int b, int dir)
        {
            switch (dir)
            {
                case 0:
                    return c => (Dice) dices.GetValue(a, c, b);
                case 1:
                    return c => (Dice) dices.GetValue(c, a, b);
                case 2:
                    return c => (Dice) dices.GetValue(a, b, c);
                default:
                    throw new IndexOutOfRangeException();
            }
        }

        // ustawia heurystyke dla kostki d.
        public void Heuristic(Dice d)
        {
            d.willBlock = 0;
            int x = d.x, y = d.y, z = d.z;
            //  heurystyka jest heurystyką we wszystkich sześciu kierunkach wychodzących z kostki
            d.heuristic = Direction.GetDirs().Sum(dir => Heuristic(d, x, y, z, dir));
        }

        private int Heuristic(Dice which, int x, int y, int z, int dir)
        {
            int ret = 0;
            //  zgodnie z osią, czy w przeciwna stronę
            int op = dir.Operand();
            //  Zwraca numer ścianki naprzeciwległej
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
            //  Sprawdzamy wszystkie kostki leżace na osi, którą badamy, w kierunku, który badamy
            int max = this[ax];
            for (int i = start + op; i >= 0 && i < max; i += op)
            {
                Dice d = func(i);
                //  kostka została usunięta
                if (d == null || !d.active)
                    continue;
                //  pobieramy ściankę zwróconą przodem do naszje kostki
                Face f = d.faces[sec];
                //  Usunięcie kostki dla której badamy heurystykę spowoduje zablokowanie usunięcia kostki d poprzez ściankę f
                int chk = Math.Abs(i - x);
                if (chk == f.startValue + 1)
                {
                    ret += BLOCK_WSP[d.activeFaces];
                    which.willBlock++;
                }
                    //  Usunięcie kostki, dla której badamy heurystykę pomoże kostce d dojść do stanu możliwego do usunięcia poprzez ściankę f
                else if (chk <= f.startValue)
                    ret += HELP_WSP;
            }
            return ret;
        }

        public Cube Clone()
        {
            var ret = new Cube(x, y, z) {ActiveDices = ActiveDices};
            foreach (Dice dice in this)
                ret.dices[dice.x, dice.y, dice.z] = dice.Clone(ret);
            return ret;
        }

        public void remove(Dice d, SortedDiceMultiList pq)
        {
            int x = d.x, y = d.y, z = d.z;
            dices[x, y, z] = null;
            //ActiveDices--;
            foreach (int dir in Direction.GetDirs())
                remove(d, x, y, z, dir, pq);
        }


        //Pomieszanie prepare z heurystyką.
        //Zachowuje się tak samo jak heurystyka, ale poprawia wartości, a nie tylko liczy jakieś liczby.
        //Czyli po usunięciu kostki zapisuje w kostkach zależnych informacje, czy są zblokowane
        //czy zmienił sie ich status dojścia do usunięcia itp.
        private void remove(Dice removed, int x, int y, int z, int dir, SortedDiceMultiList pq)
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
            int max = this[ax];
            for (int i = start + op; i >= 0 && i < max; i += op)
            {
                Dice d = func(i);
                if (d == null || !d.active)
                    continue;
                Face f = d.faces[sec];
                if (!f.active)
                    continue;
                int chk = Math.Abs(i - removed[ax]);
                if (chk <= f.startValue)
                {
                    f.currentValue--;
                    if (d.bestValue > f.currentValue)
                    {
                        d.bestValue = f.currentValue;
                        if (d.bestValue == 0)
                        {
                            Heuristic(d);
                            pq.Add(d);
                        }
                    }
                }
                else if (chk == f.startValue + 1)
                {
                    f.active = false;
                    d.activeFaces--;
                    if (d.activeFaces == 0)
                    {
                        d.active = false;
                        d.cube.ActiveDices--;
                    }
                }
            }
        }
    }
}