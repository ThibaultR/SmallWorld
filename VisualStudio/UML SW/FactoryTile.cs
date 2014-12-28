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
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public Desert desert
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public Mountain mountain
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public Forest forest
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public Plain getOrCreatePlain()
        {
            throw new System.NotImplementedException();
        }

        public Desert getOrCreateDesert()
        {
            throw new System.NotImplementedException();
        }

        public Mountain getOrCreateMountain()
        {
            throw new System.NotImplementedException();
        }

        public Forest getOrCreateForest()
        {
            throw new System.NotImplementedException();
        }
    }
}
