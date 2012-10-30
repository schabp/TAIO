using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAIO
{
    struct Face
    {
        //  wartość początkowa
        public int startValue;
        //  ilość kostek, które trzeba jeszcze usunąć, aby móc usunąć kostkę, do której należy ta ścianka(nieskończoność, dla active=false)
        public int currentValue;
        //  false, jeśli poprzez tą ściankę nie da się usunąć kostki
        public bool InActive;
        //  kostka do której należy ta ścianka
        //public int direction;

        //public Face(int startValue, int currentValue, bool inActive/*, int direction*/)
        //{
        //    this.startValue = startValue;
        //    this.currentValue = currentValue;
        //   this.InActive = inActive;
        //    //this.direction = direction;
        //}

        //public Face Clone()
        //{
        //    return new Face(startValue, currentValue, InActive/*, direction*/);
        //}
        public override string ToString()
        {
            return String.Format("[{0}, {1}]"/*, direction*/, startValue, currentValue);
        }
    }

}
