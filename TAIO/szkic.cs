﻿using System;
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


        Queue best;
        public static Queue start(Cube c)
        {
        //  Kostka priorytetowa, gdzie priorytetem jest pole heuristic kostki
            IPriorityQueue<Dice> pqueue = new IntervalHeap<Dice>();
            IQueue<Dice> ret = new C5.LinkedList<Dice>();
        //  sprawdzi, które kostki i ścianki są aktywne
        //  dla ścianek z bestValue = 0 doda je do kolejki p i obliczy heurystyke
            prepare(c, pqueue);
            iteartion(c, p, ret);
            return best;
        }

        public static void prepare(Cube c, IPriorityQueue<Dice> q)
        {
            foreach (var dice in c)
                foreach (var face in dice.SelectMany((f, i) => new {f, i}))
                {
                    
                }
        }

        iteration(Cube c, PQueue p, Queue ret)
        {
            if(p.size == 0) 
            {
                if(ret.size > best.size) 
                    best = ret;
                return;
            }
            foreach (Dice d in p)
            {
                Dice cn = c.Clone();
                PQueue pn = p.clone();
                pn.remove(d);
            //  Usuwa kostke d, poprawia heurystyki i inne wartosci pol
                cn.remove(d, pn);
            //  Dzieki temu napewno nie uzyskamy lepszego rozwiazania, jesli if zwroci false)
                if(cn.activeDices + ret.size + 1 > best.size())
                {
                    Queue nret = ret.clone();
                    nret.add(d);
                    iteration(cn, pn, nret);
                }
            }
        }

        // wspolczynniki HELP_WSP i BLOCK_WSP obliczymy metoda prob i bledow
        int heuristic(Cube c, Dice d) {
            int ret = 0;
            foreach(Dice dd in c.Where(x => /*dwie wspolrzedne x zgadzaja sie z dwoma wspolrzednymi d*/))
            {
                if(/*usuniecie d "pomoze" dd, czyli zmniejszy value jednej ze scianek*/)
                    ret += HELP_WSP;
                else if(/*usuniecie d "zablokuje" usuniecie dd przez jedna ze scianek*/)
                    ret += BLOCK_WSP * (6 - dd.activeFaces);
            }
            return ret;
        }

    }
}
