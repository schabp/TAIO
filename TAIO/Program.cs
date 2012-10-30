using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TAIO
{
    class Program
    {
        public static void Main()
        {
            const int howmany = 1;
            //string path = @"D:\Pobrane\SetI.txt";
            const string path = @"C:\Users\Szubster\Desktop\kostka2.txt";
            var load = new Loader(path);
            double duration = 0;
            List<String> ret = null;
            for (int i = 0; i < howmany; i++)
            {
                Cube c = load._cube.Clone();
                DateTime start = DateTime.Now;
                ret = szkic.start(c);
                DateTime end = DateTime.Now;
                duration += (end - start).TotalMilliseconds;
                Console.WriteLine(start);
                Console.WriteLine(end);
            }
            Console.WriteLine(ret.Count);
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
