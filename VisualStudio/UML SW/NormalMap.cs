﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UML_SW
{
    [Serializable()]
    public class StrategyNormalMap : StrategyMap
    {

        public const int SIZE_NORMAL = 14;
        public const int NB_ROUNDS_NORMAL = 30;
        public const int NB_UNITS_NORMAL = 8;

        public StrategyNormalMap()
        {
        }

        public int size
        {
            get
            {
                return SIZE_NORMAL;
            }
        }

        public int nbRounds
        {
            get
            {
                return NB_ROUNDS_NORMAL;
            }
        }

        public int nbUnits
        {
            get
            {
                return NB_UNITS_NORMAL;
            }
        }
    }
}
