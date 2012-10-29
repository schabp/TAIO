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
            int best = 0;
            for (int i = 0; i < 15; i++)
            {
                Cube.HELP_WSP = i;
                for (int k1 = 0; k1 > -15; k1--)
                {
                    Cube.BLOCK_WSP[1] = k1;
                    for(int k2 = 0; k2 >= k1; k2--)
                    {
                        Cube.BLOCK_WSP[2] = k2;
                        for(int k3 = 0; k3 >= k2; k3--)
                        {
                            Cube.BLOCK_WSP[3] = k3;
                            for(int k4 = 0; k4>=k3; k4--)
                            {
                                Cube.BLOCK_WSP[4] = k4;
                                for(int k5 = 0; k5>=k4; k5--)
                                {
                                    Cube.BLOCK_WSP[5] = k5;
                                    for(int k6 = 0; k6>=k5; k6--)
                                    {
                                        Cube.BLOCK_WSP[6] = k6;
                                        
                                        Cube c = load._cube.Clone();
                                        //DateTime start = DateTime.Now;
                                        ret = szkic.start(c);
                                        if(ret.Count >= best)
                                        {
                                            Console.WriteLine("{0},{1},{2},{3},{4},{5},{6}",i,k1,k2,k3,k4,k5,k6);
                                            Console.WriteLine(ret.Count);
                                            //foreach (var s in ret)
                                            //{
                                            //    Console.WriteLine(s);
                                            //}
                                            best = ret.Count;
                                        }
                                        //DateTime end = DateTime.Now;
                                        //duration += (end - start).TotalMilliseconds;
                                        //Console.WriteLine(start);
                                        //Console.WriteLine(end);
                                    }
                                }
                            }
                        }
                    }
                    
                }
            }
            //Console.WriteLine(ret.Count);
            //foreach (var s in ret)
            //{
            //    Console.WriteLine(s);
            //}
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
