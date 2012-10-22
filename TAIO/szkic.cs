using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C5;

namespace TAIO
{
    class szkic
    {
        static IQueue<Dice> best;
        public static IQueue<Dice> start(Cube c)
        {
        //  Kostka priorytetowa, gdzie priorytetem jest pole heuristic kostki
            IPriorityQueue<Dice> pqueue = new IntervalHeap<Dice>();
            IQueue<Dice> ret = new C5.LinkedList<Dice>();
        //  sprawdzi, które kostki i ścianki są aktywne
        //  dla ścianek z bestValue = 0 doda je do kolejki p i obliczy heurystyke
            prepare(c, pqueue);
            iteration(c, pqueue, ret);
            return best;
        }

        public static void prepare(Cube c, IPriorityQueue<Dice> q)
        {
            foreach (var dice in c)
            {
                foreach (var face in dice)
                {
                    int op = Direction.Operand(face.direction);
                    int ax = face.direction.Axis();
                    int ind = dice[ax];
                    if(face.startValue == 0)
                    {
                        dice.bestValue = 0;
                    }
                    else if(ind + face.startValue*op > c[ax])
                    {
                        face.active = false;
                        face.currentValue = int.MaxValue;
                        dice.activeFaces --;
                    }
                    else if(face.currentValue < dice.bestValue)
                    {
                        dice.bestValue = face.currentValue;
                    }
                }
                if(dice.activeFaces == 0)
                {
                    dice.active = false;
                    c.ActiveDices --;
                }
                if(dice.bestValue == 0)
                {
                    c.Heuristic(dice);
                    q.Add(dice);
                }
            }
        }

        static void iteration(Cube c, IPriorityQueue<Dice> p, IQueue<Dice> ret)
        {
            if(p.IsEmpty) 
            {
                if(ret.Count > best.Count) 
                    best = ret;
                return;
            }
            foreach (Dice d in p)
            {
                Cube cn = c.Clone();
                IPriorityQueue<Dice> np = new IntervalHeap<Dice>(p.Count);
                foreach (var dice in p)
                    if(dice != d)
                        np.Add(cn.dices[dice.x, dice.y, dice.z]);
            //  Usuwa kostke d, poprawia heurystyki i inne wartosci pol
                cn.remove(d, np);
            //  Dzieki temu napewno nie uzyskamy lepszego rozwiazania, jesli if zwroci false)
                if(cn.ActiveDices + ret.Count + 1 > best.Count)
                {
                    IQueue<Dice> nret = new C5.LinkedList<Dice>();
                    foreach (var dice in ret)
                        nret.Enqueue(cn.dices[dice.x, dice.y, dice.z]);
                    iteration(cn, np, nret);
                }
            }
        }
    }
}
