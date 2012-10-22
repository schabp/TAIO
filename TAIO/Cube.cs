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

        public Cube(int x, int y, int z)
        {
            dices = new Dice[x,y,z];
            this.x = x;
            this.y = y;
            this.z = z;
            ActiveDices = x*y*z;
        }

        public void Heuristic(Dice d)
        {
            int x = d.x, y = d.y, z = d.z;
            d.heuristic = Direction.GetDirs().Sum(dir => Heuristic(x, y, z, dir));
        }

        private int Heuristic(int x, int y, int z, int dir)
        {
            int ret = 0;
            int op = dir.Operand();
            int sec = dir.Opposite();
            for(int i = x + op; i >= 0 && i < this.x; i += op)
            {
                var d = dices[i, y, z];
                if (d == null)
                    continue;
                var f = d.faces[sec];
                if (Math.Abs(i - x) == f.startValue + 1)
                    ret += BLOCK_WSP * (6 - d.activeFaces);
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

        private void remove(int x, int y, int z, int dir, IPriorityQueue<Dice> pq)
        {
            int op = dir.Operand();
            int sec = dir.Opposite();
            for (int i = x + op; i >= 0 && i < this.x; i += op)
            {
                var d = dices[i, y, z];
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
