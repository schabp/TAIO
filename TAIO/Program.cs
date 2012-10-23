using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using C5;

namespace TAIO
{
    class Program
    {
        public static void Main()
        {
            const int howmany = 1;
            //string path = @"C:\Users\Asus\Downloads\SetI.txt";
            string path = @"D:\Pulpit\kostka2.txt";
            Loader load = new Loader(path);
            Console.WriteLine("load");
            double duration = 0;
            IQueue<String> ret = null;
            for (int i = 0; i < howmany; i++)
            {
                Console.WriteLine("clone");
                Cube c = load._cube.Clone();
                DateTime start = DateTime.Now;
                Console.WriteLine("start");
                ret = szkic.start(c);
                Console.WriteLine("end");
                DateTime end = DateTime.Now;
                duration += (end - start).TotalMilliseconds;
                Console.WriteLine(start);
                Console.WriteLine(end);
            }
            Console.WriteLine("out");
            foreach (var s in ret)
            {
                Console.WriteLine(s);
            }
            Console.WriteLine("total(ms):");
            Console.WriteLine(duration);
            Console.WriteLine("total(s):");
            Console.WriteLine(duration/1000.0);
            Console.WriteLine("one(ms):");
            Console.WriteLine(duration/howmany);
            Console.WriteLine("one(s):");
            Console.WriteLine(duration/howmany/1000.0);
            //Console.ReadKey();
        }
    }
}
