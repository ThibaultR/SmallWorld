using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UML_SW
{
    [Serializable()]
    public class StrategySmallMap : StrategyMap
    {
        public const int SIZE_SMALL = 10;
        public const int NB_ROUNDS_SMALL = 20;
        public const int NB_UNITS_SMALL = 6;

        public StrategySmallMap()
        {
        }

        public int size
        {
            get
            {
                return SIZE_SMALL;
            }
        }

        public int nbRounds
        {
            get
            {
                return NB_ROUNDS_SMALL;
            }
        }

        public int nbUnits
        {
            get
            {
                return NB_UNITS_SMALL;
            }
        }
    }
}
