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
            string path = @"C:\Users\Asus\Downloads\kostka.txt";
            Loader load = new Loader(path);
            IQueue<Dice> ret = szkic.start(load._cube);
        }
    }
}
