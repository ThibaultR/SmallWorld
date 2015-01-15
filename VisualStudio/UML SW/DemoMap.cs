using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UML_SW
{
    [Serializable()]
    public class StrategyDemoMap : StrategyMap
    {
        
        public const int SIZE_DEMO = 6;
        public const int NB_ROUNDS_DEMO = 5;
        public const int NB_UNITS_DEMO = 4;

        public StrategyDemoMap()
        {
        }

        public int size
        {
            get
            {
                return SIZE_DEMO;
            }
        }

        public int nbRounds
        {
            get
            {
                return NB_ROUNDS_DEMO;
            }
        }

        public int nbUnits
        {
            get
            {
                return NB_UNITS_DEMO;
            }
        }
    }
}
