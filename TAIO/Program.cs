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
            DateTime start = DateTime.Now;
            IQueue<Dice> ret = szkic.start(load._cube);
            DateTime end = DateTime.Now;
            Console.WriteLine(end - start);
            Console.ReadKey();
        }
    }
}
