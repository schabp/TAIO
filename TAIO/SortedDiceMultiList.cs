using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TAIO
{
    class SortedDiceMultiList : SortedList<int, List<Dice>>
    {
        public int DiceCount;

        public void Add(Dice item)
        {
            List<Dice> list;
            DiceCount++;
            if (!TryGetValue(item.heuristic, out list))
            {
                list = new List<Dice>{item};
                Add(item.heuristic, list);
                return;
            }
            list.Add(item);

        }
    }
}
