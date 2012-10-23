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
        public bool active = true;
        //  kostka do której należy ta ścianka
        public Dice dice;
        public int direction;

        public Face Clone(Dice d)
        {
            return new Face { active = active, currentValue = currentValue, dice = d, direction = direction, startValue = startValue };
        }
        public override string ToString()
        {
            return String.Format("[{0}, {1}, {2}]", direction, startValue, currentValue);
        }
    }

}
