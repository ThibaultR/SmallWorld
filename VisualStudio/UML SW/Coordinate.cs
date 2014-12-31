using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UML_SW
{
    public class Coordinate
    {
        public Coordinate() {
            x = 0;
            y = 0;
        }

        public Coordinate(int posx, int posy) {
            x = posx;
            y = posy;
        }


        public int x
        {
            get;
            set;
        }

        public int y
        {
            get;
            set;
        }

        public bool equals(Coordinate c){
            return this.x == c.x && this.y == c.y;
        }
    }

}
