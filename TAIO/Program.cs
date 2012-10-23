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
            //string path = @"C:\Users\Asus\Downloads\SetI.txt";
            string path = @"C:\Users\Szubster\Desktop\kostka2.txt";
            Loader load = new Loader(path);
            long duration = 0;
            for (int i = 0; i < 100; i++)
            {
                Cube c = load._cube.Clone();
                DateTime start = DateTime.Now;
                IQueue<Dice> ret = szkic.start(c);
                DateTime end = DateTime.Now;
                duration += (end - start).Milliseconds;
            }
            Console.WriteLine("total(ms):");
            Console.WriteLine(duration);
            Console.WriteLine("total(s):");
            Console.WriteLine(duration/1000.0);
            Console.WriteLine("one(ms):");
            Console.WriteLine(duration/100.0);
            Console.WriteLine("one(s):");
            Console.WriteLine(duration/100.0/1000.0);
            Console.ReadKey();
        }
    }
}
