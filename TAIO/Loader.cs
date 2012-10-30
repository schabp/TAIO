using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TAIO
{
    class Loader
    {
        private int x, y, z;
        public Cube _cube;
        public Loader(string path)
        {
            using (TextReader reader = File.OpenText(@path))
            {
                string line = reader.ReadLine();
                var string_table = line.Split(' ');
                x = int.Parse(string_table[0]);
                y = int.Parse(string_table[1]);
                z = int.Parse(string_table[2]);
                int _x, _y, _z;
                Cube c = new Cube(x, y, z);
                line = reader.ReadLine();
                do
                {
                    string_table = line.Split(' ');
                    _x = int.Parse(string_table[0]);
                    _y = int.Parse(string_table[1]);
                    _z = int.Parse(string_table[2].Trim(':'));
                    Dice d = new Dice();
                    d.x = _x;
                    d.z = _z;
                    d.y = _y;
                    //d.cube = c;
                    for (int i = 3; i < 9; i++)
                    {
                        Face f = new Face();
                        f.startValue = f.currentValue = int.Parse(string_table[i]);
                        //f.direction = Direction.dirs[i - 3];
                        d.faces[Direction.dirs[i-3]] = f;
                    }
                    c.dices[_x, _y, _z] = d;
                    line = reader.ReadLine();
                } while (line != null);

                _cube = c;
            }
        }
    }
}
