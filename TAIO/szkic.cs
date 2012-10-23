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
        //  Tutaj zapisujemy najlepsze rozwiązanie(kolejka kostek)
            best = new C5.LinkedList<Dice>();
        //  Kostka priorytetowa, gdzie priorytetem jest pole heuristic kostki
            IPriorityQueue<Dice> pqueue = new IntervalHeap<Dice>();
        //  Tymczasowa kolejka na rozwiązanie(być może przepiszemy do best
            IQueue<Dice> ret = new C5.LinkedList<Dice>();
        //  sprawdzi, które kostki i ścianki są aktywne
        //  dla ścianek z bestValue = 0 doda je do kolejki p i obliczy heurystyke
            prepare(c, pqueue);
        //  Pojedyńczy krok algorytmu
            iteration(c, pqueue, ret);
            return best;
        }

        public static void prepare(Cube c, IPriorityQueue<Dice> q)
        {
        //  Dla każdej kostki
            foreach (var dice in c)
            {
            //  Dla każdej ścianki na kostce
                foreach (var face in dice)
                {
                //  Sprawdzamy w którą stronę kieruje się ścianka(malejąco, czy rosnąco z x)
                    int op = Direction.Operand(face.direction);
                //  Sprawdzamy na której osi leży kostka(0-y, 1-x, 2-z)
                    int ax = face.direction.Axis();
                //  Pobieramy wspórzędną kostki od interesujacej nas osi
                    int ind = dice[ax];
                //  Jeśli kostka ma zero, nie ma co więcej liczyć, zawsze można ją usunąć
                    if(face.startValue == 0)
                    {
                    //  poprawiamy najlpeszą wartość na kostce
                        dice.bestValue = 0;
                    }
                //  Kostki nie da się usunąć(wymiary wykraczają poza sześcian)
                    else if(ind + face.startValue*op > c[ax] || ind + face.startValue*op < 0)
                    {
                    //  ustawiamy ściankę na nieaktywną, kostkę na nieaktywną.
                        face.active = false;
                        face.currentValue = int.MaxValue;
                        dice.activeFaces --;
                    }
                //  Być może obliczona wartosć jest najlepsza dla kostki
                    else if(face.currentValue < dice.bestValue)
                    {
                        dice.bestValue = face.currentValue;
                    }
                }
            //  Jeśli nie mamy aktywnych ścianek, to nie mamy aktywnej kostki
                if(dice.activeFaces == 0)
                {
                    dice.active = false;
                    c.ActiveDices --;
                }
            //  Możemy usunąć tą kostkę, obliczmy jej heurystykę i dodajmy do kolejki.
                if(dice.bestValue == 0)
                {
                    c.Heuristic(dice);
                    q.Add(dice);
                }
            }
        }

        static void iteration(Cube c, IPriorityQueue<Dice> p, IQueue<Dice> ret)
        {
        //  jesli p puste, to nie mamy co zdejmować
            if(p.IsEmpty) 
            {
            // Jeśli ilość kostek, które zdjęliśmy jest lepsza od najlepszego wyniku to go zapisujemy.
                if(ret.Count > best.Count) 
                    best = ret;
                return;
            }
        //  Będziemy sprawdzać każdą kostkę, którą możemy zdjąć
            foreach (Dice d in p)
            {
            //  Klonujemy kostkę i kolejki, bo się wszystko pochrzani
                Cube cn = c.Clone();
                IPriorityQueue<Dice> np = new IntervalHeap<Dice>(p.Count);
                foreach (var dice in p)
                    if(dice != d)
                        np.Add(cn.dices[dice.x, dice.y, dice.z]);
            //  Usuwa kostke d, poprawia heurystyki i inne wartosci pol
                cn.remove(d, np);
            //  Dzieki temu napewno nie uzyskamy lepszego rozwiazania, jesli if zwroci false)
            //  Jeśli ilość aktywnych kostek + ilość kostek jakie już usunęliśmy + 1(usuwana właśnie kostka)
            //  Jest mniejsza lub równa best.Count to nie uda nam się poprawić best
            //  Zatem następna iterację robi tylko, gdy ma to sens
                if(cn.ActiveDices + ret.Count + 1 > best.Count)
                {
                    IQueue<Dice> nret = new C5.LinkedList<Dice>();
                    foreach (var dice in ret)
                        nret.Enqueue(cn.dices[dice.x, dice.y, dice.z]);
                    nret.Enqueue(d);
                    iteration(cn, np, nret);
                }
            }
        }
    }
}
