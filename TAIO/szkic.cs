using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAIO
{
    class szkic
    {
        static List<String> best;
        private const int WSP_DELTA = 10;
        private static bool end;
        private static DateTime startTime;
        private static int cnt;
        private static int branch;
        private static int StartDices;
        public static List<String> start(Cube c)
        {
            StartDices = c[0]*c[1]*c[2];
            c.ActiveDices = StartDices;
            startTime = DateTime.Now;
        //  Tutaj zapisujemy najlepsze rozwiązanie(kolejka kostek)
            end = false;
            best = new List<String>();
        //  Kostka priorytetowa, gdzie priorytetem jest pole heuristic kostki
            var pqueue = new SortedDiceMultiList();
        //  Tymczasowa kolejka na rozwiązanie(być może przepiszemy do best
            var ret = new List<String>();
        //  sprawdzi, które kostki i ścianki są aktywne
        //  dla ścianek z bestValue = 0 doda je do kolejki p i obliczy heurystyke
            prepare(c, pqueue);
        //  Pojedyńczy krok algorytmu
<<<<<<< HEAD
            iteration(c, pqueue, ret);
=======
            beamedDeltaGreedy(c, pqueue, ret);
>>>>>>> a7b117243bb4e3b3a8dde0890c6bf241b65d54fe
            return best;
        }

        private static void prepare(Cube c, SortedDiceMultiList q)
        {
        //  Dla każdej kostki
            LinkedList<Dice> toHeur = new LinkedList<Dice>();
            foreach (var dice in c)
            {
            //  Dla każdej ścianki na kostce
                bool hasZero = false;
                for (int dir = 0; dir < 6;dir++ )
                {
                    Face face = dice.faces[dir];
                    //  Sprawdzamy w którą stronę kieruje się ścianka(malejąco, czy rosnąco z x)
                    int op = dir.Operand();
                    //  Sprawdzamy na której osi leży kostka(0-y, 1-x, 2-z)
                    int ax = dir.Axis();
                    //  Pobieramy wspórzędną kostki od interesujacej nas osi
                    int ind = dice[ax];
                    //  Jeśli kostka ma zero, nie ma co więcej liczyć, zawsze można ją usunąć
                    if (face.startValue == 0)
                    {
                        //  poprawiamy najlpeszą wartość na kostce
                        hasZero = true;
                    }
                    //  Kostki nie da się usunąć(wymiary wykraczają poza sześcian)
                    else if (ind + face.startValue * op >= c[ax] || ind + face.startValue * op < 0)
                    {
                        //  ustawiamy ściankę na nieaktywną, kostkę na nieaktywną.
                        face.InActive = false;
                        face.currentValue = int.MaxValue;
                        dice.activeFaces--;
                    }
                    //  Być może obliczona wartosć jest najlepsza dla kostki
                    //else if(face.currentValue < dice.bestValue)
                    //{
                    //    dice.bestValue = face.currentValue;
                    //}
                }
            //  Jeśli nie mamy aktywnych ścianek, to nie mamy aktywnej kostki
                if(dice.activeFaces == 0)
                {
                    //dice.active = false;
                    c.ActiveDices --;
                }
            //  Możemy usunąć tą kostkę, obliczmy jej heurystykę i dodajmy do kolejki.
                if(hasZero)
                {
                    //c.Heuristic(dice);
                    //q.Add(dice);
                    toHeur.AddLast(dice);
                }
            }
            foreach (var dice in toHeur)
            {
                c.Heuristic(dice, 0);
                q.Add(dice);
            }
        }

        static void iteration(Cube c, SortedDiceMultiList p, List<String> ret)
        {
        //  jesli p puste, to nie mamy co zdejmować
            if(p.Count == 0) 
            {
            // Jeśli ilość kostek, które zdjęliśmy jest lepsza od najlepszego wyniku to go zapisujemy.
                if (ret.Count > best.Count)
                {
                    best = ret;
                    Console.WriteLine(DateTime.Now-startTime);
                    Console.WriteLine(best.Count);
                    foreach (var d in c.Where(x=>x!=null))
                        Console.WriteLine(d);
                    if (best.Count == StartDices)
                        end = true;
                }
                return;

            }
            if(p.DiceCount == 1)
            {
                Dice d = p.Values[0][0];
                p.Clear();
                c.remove(d, p, best.Count);
                if (c.ActiveDices + 1 > best.Count)
                {
                    ret.Add(d.ToString());
                    iteration(c, p, ret);
                }
                return;
            }
        //  Będziemy sprawdzać każdą kostkę, którą możemy zdjąć
            foreach (Dice d in p.Values.Reverse().SelectMany(x=>x))
            {
            //  Klonujemy kostkę i kolejki, bo się wszystko pochrzani
                Cube cn = c.Clone();
                var np = new SortedDiceMultiList();
            //  Usuwa kostke d, poprawia heurystyki i inne wartosci pol
                cn.remove(d, np, best.Count);
                
            //  Dzieki temu napewno nie uzyskamy lepszego rozwiazania, jesli if zwroci false)
            //  Jeśli ilość aktywnych kostek + ilość kostek jakie już usunęliśmy + 1(usuwana właśnie kostka)
            //  Jest mniejsza lub równa best.Count to nie uda nam się poprawić best
            //  Zatem następna iterację robi tylko, gdy ma to sens
                if(cn.ActiveDices + 1 > best.Count)
                {
                    var nret = new List<string>(ret) {d.ToString()};
                    foreach (var dice in p.Values.SelectMany(x => x))
                        if (dice!=d)
                        {
                            Dice dc = cn.dices[dice.x, dice.y, dice.z];
                            if (dc.activeFaces != 0)
                                np.Add(dc);
                        }
                    iteration(cn, np, nret);
                    if (end) return;
                }
            }
        }

        static void iterationGreedy(Cube c, SortedDiceMultiList p, List<String> ret)
        {
            //  jesli p puste, to nie mamy co zdejmować
            if (p.Count == 0)
            {
                // Jeśli ilość kostek, które zdjęliśmy jest lepsza od najlepszego wyniku to go zapisujemy.
                if (ret.Count > best.Count)
                {
                    best = ret;
                    //Console.WriteLine(DateTime.Now - startTime);
                    //Console.WriteLine(best.Count);
                    //foreach (var d in c.Where(x => x != null))
                    //    Console.WriteLine(d);
                    if (best.Count == StartDices)
                        end = true;
                }
                return;

            }
            if (p.DiceCount == 1)
            {
                Dice d = p.Values[0][0];
                p.Clear();
                c.remove(d, p, best.Count);
                if (c.ActiveDices + 1 > best.Count)
                {
                    ret.Add(d.ToString());
                    iterationGreedy(c, p, ret);
                }
                return;
            }
            //  Klonujemy kostkę i kolejki, bo się wszystko pochrzani
            Cube cn = c;
            var np = new SortedDiceMultiList();
            Dice dd = p.Values[p.Count-1][0];
            //  Usuwa kostke d, poprawia heurystyki i inne wartosci pol
            cn.remove(dd, np, best.Count);

            //  Dzieki temu napewno nie uzyskamy lepszego rozwiazania, jesli if zwroci false)
            //  Jeśli ilość aktywnych kostek + ilość kostek jakie już usunęliśmy + 1(usuwana właśnie kostka)
            //  Jest mniejsza lub równa best.Count to nie uda nam się poprawić best
            //  Zatem następna iterację robi tylko, gdy ma to sens
            if (cn.ActiveDices + 1 > best.Count)
            {
                ret.Add(dd.ToString());
                foreach (var dice in p.Values.SelectMany(x => x))
                    if (!dice.Equals(dd))
                    {
                        Dice dc = cn.dices[dice.x, dice.y, dice.z];
                        if (dc.activeFaces != 0)
                            np.Add(dc);
                    }
                iterationGreedy(cn, np, ret);
            }
           
        }

        static void beamedGreedy(Cube c, SortedDiceMultiList p, List<String> ret)
        {
            //  jesli p puste, to nie mamy co zdejmować
            if (p.Count == 0)
            {
                // Jeśli ilość kostek, które zdjęliśmy jest lepsza od najlepszego wyniku to go zapisujemy.
                if (ret.Count > best.Count)
                {
                    best = ret;
                    Console.WriteLine(DateTime.Now - startTime);
                    Console.WriteLine(best.Count);
                    foreach (var d in c.Where(x => x != null))
                        Console.WriteLine(d);
                    if (best.Count == c.StartDices)
                        end = true;
                }
                return;

            }
            if (p.DiceCount == 1)
            {
                Dice d = p.Values[0][0];
                p.Clear();
                c.remove(d, p);
                if (c.ActiveDices + ret.Count - d.willBlock + 1 > best.Count)
                {
                    ret.Add(d.ToString());
                    beamedGreedy(c, p, ret);
                }
                return;
            }
            //  Będziemy sprawdzać każdą kostkę, którą możemy zdjąć
            foreach (Dice d in p.Values.Reverse().SelectMany(x => x).Take(3))
            {
                //  Klonujemy kostkę i kolejki, bo się wszystko pochrzani
                Cube cn = c.Clone();
                var np = new SortedDiceMultiList();
                //  Usuwa kostke d, poprawia heurystyki i inne wartosci pol
                cn.remove(d, np);

                //  Dzieki temu napewno nie uzyskamy lepszego rozwiazania, jesli if zwroci false)
                //  Jeśli ilość aktywnych kostek + ilość kostek jakie już usunęliśmy + 1(usuwana właśnie kostka)
                //  Jest mniejsza lub równa best.Count to nie uda nam się poprawić best
                //  Zatem następna iterację robi tylko, gdy ma to sens
                if (cn.ActiveDices + ret.Count - d.willBlock + 1 > best.Count)
                {
                    var nret = new List<string>(ret) { d.ToString() };
                    foreach (var dice in p.Values.SelectMany(x => x))
                        if (!dice.Equals(d))
                        {
                            Dice dc = cn.dices[dice.x, dice.y, dice.z];
                            if (dc.active)
                                np.Add(dc);
                        }
                    beamedGreedy(cn, np, nret);
                    if (end) return;
                }
            }
        }

        static void beamedDeltaGreedy(Cube c, SortedDiceMultiList p, List<String> ret)
        {
            //  jesli p puste, to nie mamy co zdejmować
            if (p.Count == 0)
            {
                // Jeśli ilość kostek, które zdjęliśmy jest lepsza od najlepszego wyniku to go zapisujemy.
                if (ret.Count > best.Count)
                {
                    best = ret;
                    Console.WriteLine(DateTime.Now - startTime);
                    Console.WriteLine(best.Count);
                    foreach (var d in c.Where(x => x != null))
                        Console.WriteLine(d);
                    if (best.Count == c.StartDices)
                        end = true;
                }
                return;

            }
            if (p.DiceCount == 1)
            {
                Dice d = p.Values[0][0];
                p.Clear();
                c.remove(d, p);
                if (c.ActiveDices + ret.Count - d.willBlock + 1 > best.Count)
                {
                    ret.Add(d.ToString());
                    beamedDeltaGreedy(c, p, ret);
                }
                return;
            }
            //  Będziemy sprawdzać każdą kostkę, którą możemy zdjąć
            int heur = p.Values[p.Count - 1][0].heuristic;
            Console.WriteLine(p.Values[p.Count - 1].Count);
            foreach (Dice d in p.Values.Reverse().SelectMany(x => x).TakeWhile(x => heur - x.heuristic <= WSP_DELTA))
            {
                //  Klonujemy kostkę i kolejki, bo się wszystko pochrzani
                Cube cn = c.Clone();
                var np = new SortedDiceMultiList();
                //  Usuwa kostke d, poprawia heurystyki i inne wartosci pol
                cn.remove(d, np);

                //  Dzieki temu napewno nie uzyskamy lepszego rozwiazania, jesli if zwroci false)
                //  Jeśli ilość aktywnych kostek + ilość kostek jakie już usunęliśmy + 1(usuwana właśnie kostka)
                //  Jest mniejsza lub równa best.Count to nie uda nam się poprawić best
                //  Zatem następna iterację robi tylko, gdy ma to sens
                if (cn.ActiveDices + ret.Count - d.willBlock + 1 > best.Count)
                {
                    var nret = new List<string>(ret) { d.ToString() };
                    foreach (var dice in p.Values.SelectMany(x => x))
                        if (!dice.Equals(d))
                        {
                            Dice dc = cn.dices[dice.x, dice.y, dice.z];
                            if (dc.active)
                                np.Add(dc);
                        }
                    //Console.WriteLine("The best huristic: {0}", heur.ToString());
                    //Console.WriteLine("The huristic: {0}", d.heuristic.ToString());
                    beamedDeltaGreedy(cn, np, nret);
                    if (end) return;
                }
            }
        }
    }
}
