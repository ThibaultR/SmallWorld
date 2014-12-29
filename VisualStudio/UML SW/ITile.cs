using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UML_SW
{
    public interface ITile
    {
    }


    public class Plain : ITile
    {
        public Plain() { }
    }


    public class Desert : ITile
    {
        public Desert() { }
    }


    public class Mountain : ITile
    {
        public Mountain() { }
    }


    public class Forest : ITile
    {
        public Forest() { }
    }
}
