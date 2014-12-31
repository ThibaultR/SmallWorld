using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UML_SW
{
    public class Unit
    {
        public const int MAX_HEALTH = 5;
        public const int MAX_MVT = 1;
        public const int ATTACK = 2;
        public const int DEFENCE = 1;

        public Unit()
        {
            isAlive = true;
            bonusPoint = 0;
            healthPoint = MAX_HEALTH;
            movementPoint = MAX_MVT;
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

        public void attack(Unit enemy)
        {
            int nbFightMin = 3;
            int nbFightMax = Math.Max(this.healthPoint, enemy.healthPoint) + 2;
            Random random = new Random();
            int nbFight = random.Next(nbFightMin, nbFightMax + 1);

            int i = 0;
            while (i < nbFight && this.isAlive && enemy.isAlive)
            {
                double attackUnit = ATTACK * this.healthPoint / (double) MAX_HEALTH;
                double defenceEnemy = DEFENCE * enemy.healthPoint / (double) MAX_HEALTH;

                double ecart = (attackUnit - defenceEnemy) / Math.Max(attackUnit, defenceEnemy);

                double victoryProbability = 0.5 + 0.5 * ecart;

                int rd = random.Next(0, 100);

                if (rd < victoryProbability*100)
                {
                    enemy.healthPoint--;
                    
                    if (enemy.healthPoint == 0)
                    {
                        enemy.die();
                        if (this.GetType() == typeof(UnitOrc))
                        {
                            this.bonusPoint++;
                        }
                    }
                }
                else {
                    this.healthPoint--;

                    if (this.healthPoint == 0)
                    {
                        this.die();
                        if (enemy.GetType() == typeof(UnitOrc))
                        {
                            enemy.bonusPoint++;
                        }
                    }
                }

                i++;
            }
           
        }

        public void move(Coordinate c, Type currentTileType)
        {
            this.coordinate.x = c.x;
            this.coordinate.y = c.y;
        }

        public void die()
        {
            isAlive = false;
            //TODO : remove from tile ...
        }
    }


    public class UnitElf : Unit
    {
        public void move(Coordinate c, Type currentTileType) 
            : base( c , c)
    { fds
    }
    }

    public class UnitDwarf : Unit
    {
    }

    public class UnitOrc : Unit
    {
    }
}
