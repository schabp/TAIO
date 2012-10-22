using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAIO
{
    class Face
    {
        //  wartość początkowa
        public int startValue;
        //  ilość kostek, które trzeba jeszcze usunąć, aby móc usunąć kostkę, do której należy ta ścianka(nieskończoność, dla active=false)
        public int currentValue;
        //  false, jeśli poprzez tą ściankę nie da się usunąć kostki
        public bool active;
        //  kostka do której należy ta ścianka
        public Dice dice;
    }

}
