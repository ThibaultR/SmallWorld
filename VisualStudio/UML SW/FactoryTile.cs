using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UML_SW
{
    public class FactoryTile
    {

        public Plain plain
        {
            get;
            set;
        }

        public Desert desert
        {
            get;
            set;
        }

        public Mountain mountain
        {
            get;
            set;
        }

        public Forest forest
        {
            get;
            set;
        }

        public FactoryTile() { }

        public Plain getOrCreatePlain()
        {
            if (plain == null) {
                plain = new Plain();
            }

            return plain;
        }

        public Desert getOrCreateDesert()
        {
            if (desert == null)
            {
                desert = new Desert();
            }

            return desert;
        }

        public Mountain getOrCreateMountain()
        {
            if (mountain == null)
            {
                mountain = new Mountain();
            }

            return mountain;
        }

        public Forest getOrCreateForest()
        {
            if (forest == null)
            {
                forest = new Forest();
            }

            return forest;
        }
    }
}
