using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UML_SW
{
    [Serializable()]
    public class Coordinate
    {
        public int x { get; set; }

        public int y { get; set; }

        public Coordinate() {
            x = 0;
            y = 0;
        }

        public Coordinate(int posx, int posy) {
            x = posx;
            y = posy;
        }

        public override bool Equals(System.Object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Point return false.
            Coordinate c = obj as Coordinate;
            if ((System.Object) c == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (x == c.x) && (y == c.y);
        }

        public bool Equals(Coordinate c)
        {
            // If parameter is null return false:
            if ((object)c == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (x == c.x) && (y == c.y);
        }

        public override int GetHashCode()
        {
            return x ^ y;
        }

    }

}
