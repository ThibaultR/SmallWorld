using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UML_SW
{
    public class FactoryPopulation
    {
        public enum populationType { Elf, Orc, Dwarf}

        public Unit createUnit(populationType type) {
            switch (type) { 
                case populationType.Elf:
                    return createElf();
                case populationType.Orc:
                    return createOrc();
                case populationType.Dwarf:
                    return createDwarf();
                default:
                    return null; //TODO ERROR
            }
        }

        public UnitElf createElf()
        {
            return new UnitElf();
        }

        public UnitOrc createOrc()
        {
            return new UnitOrc();
        }

        public UnitDwarf createDwarf()
        {
            return new UnitDwarf();
        }
    }
}
