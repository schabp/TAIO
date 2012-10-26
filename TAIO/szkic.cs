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
        private static bool end;
        private static DateTime startTime;
        public static List<String> start(Cube c)
        {
            startTime = DateTime.Now;
        //  Tutaj zapisujemy najlepsze rozwiązanie(kolejka kostek)
            end = false;
            best = new List<String>();
        //  Kostka priorytetowa, gdzie priorytetem jest pole heuristic kostki
            SortedDiceMultiList pqueue = new SortedDiceMultiList();
        //  Tymczasowa kolejka na rozwiązanie(być może przepiszemy do best
            List<String> ret = new List<String>();
        //  sprawdzi, które kostki i ścianki są aktywne
        //  dla ścianek z bestValue = 0 doda je do kolejki p i obliczy heurystyke
            prepare(c, pqueue);
        //  Pojedyńczy krok algorytmu
            iteration(c, pqueue, ret);
            return best;
        }

        public static void prepare(Cube c, SortedDiceMultiList q)
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
                    else if(ind + face.startValue*op >= c[ax] || ind + face.startValue*op < 0)
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
                    if (best.Count == c.StartDices)
                        end = true;
                }
                return;

            }
            if(p.DiceCount == 1)
            {
                Dice d = p.Values[0][0];
                p.Clear();
                c.remove(d, p);
                if (c.ActiveDices + ret.Count - d.willBlock + 1 > best.Count)
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
                SortedDiceMultiList np = new SortedDiceMultiList();
            //  Usuwa kostke d, poprawia heurystyki i inne wartosci pol
                cn.remove(d, np);
                
            //  Dzieki temu napewno nie uzyskamy lepszego rozwiazania, jesli if zwroci false)
            //  Jeśli ilość aktywnych kostek + ilość kostek jakie już usunęliśmy + 1(usuwana właśnie kostka)
            //  Jest mniejsza lub równa best.Count to nie uda nam się poprawić best
            //  Zatem następna iterację robi tylko, gdy ma to sens
                if(cn.ActiveDices + ret.Count - d.willBlock + 1 > best.Count)
                {
                    List<String> nret = new List<string>(ret) {d.ToString()};
                    foreach (var dice in p.Values.SelectMany(x => x))
                        if (!dice.Equals(d))
                        {
                            Dice dc = cn.dices[dice.x, dice.y, dice.z];
                            if (dc.active)
                                np.Add(dc);
                        }
                    iteration(cn, np, nret);
                    if (end) return;
                }
            }
        }
    }
}
