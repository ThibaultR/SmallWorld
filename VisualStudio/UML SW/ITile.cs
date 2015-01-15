using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UML_SW
{
    public interface ITile
    {
    }

    [Serializable()]
    public class Plain : ITile
    {
        public Plain() { }
    }

    [Serializable()]
    public class Desert : ITile
    {
        public Desert() { }
    }

    [Serializable()]
    public class Mountain : ITile
    {
        public Mountain() { }
    }

    [Serializable()]
    public class Forest : ITile
    {
        public Forest() { }
    }
}
