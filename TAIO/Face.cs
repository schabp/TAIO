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
        int startValue;
        //  ilość kostek, które trzeba jeszcze usunąć, aby móc usunąć kostkę, do której należy ta ścianka(nieskończoność, dla active=false)
        int currentValue;
        //  false, jeśli poprzez tą ściankę nie da się usunąć kostki
        bool active;
        //  kostka do której należy ta ścianka
        Dice dice;
    }

}
