using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UML_SW
{
    public class Unit
    {
        public Unit()
        {
            isAlive = true;
            bonusPoint = 0;
            healthPoint = 5;
            movementPoint = 1;
            coordinate = new Coordinate();
        }

        public bool isAlive
        {
            get;
            set;
        }

        public double movementPoint
        {
            get;
            set;
        }

        public int healthPoint
        {
            get;
            set;
        }

        public Coordinate coordinate
        {
            get;
            set;
        }

        public int bonusPoint
        {
            get;
            set;
        }

        public void attack()
        {
            throw new System.NotImplementedException();
            //TODO
        }

        public void move()
        {
            throw new System.NotImplementedException();
            //TODO
        }

        public void die()
        {
            isAlive = false;
            //TODO : remove from tile ...
        }
    }


    public class UnitElf : Unit
    {
    }

    public class UnitDwarf : Unit
    {
    }

    public class UnitOrc : Unit
    {
    }
}
