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
        public const int COSTMVT = 1;

        public bool isAlive { get; set; }

        public double movementPoint { get; set; }

        public int healthPoint { get; set; }

        public Coordinate coordinate { get; set; }

        public int bonusPoint { get; set; }

        public Unit()
        {
            isAlive = true;
            bonusPoint = 0;
            healthPoint = MAX_HEALTH;
            movementPoint = MAX_MVT;
            coordinate = new Coordinate();
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
                        if (enemy.GetType() == typeof(UnitElf)) {
                            int survive = random.Next(0, 2);
                            if (survive == 0) { enemy.die(); }
                            else { 
                                enemy.healthPoint = 1;
                                break;
                            }
                        }
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
                        if (this.GetType() == typeof(UnitElf))
                        {
                            int survive = random.Next(0, 2);
                            if (survive == 0) { this.die(); }
                            else
                            {
                                this.healthPoint = 1;
                                break;
                            }
                        }
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

        public virtual void move(Coordinate c, Type currentTileType)
        {
            this.coordinate.x = c.x;
            this.coordinate.y = c.y;
        }

        public void die()
        {
            isAlive = false;
        }
    }


    public class UnitElf : Unit
    {
        public override void move(Coordinate c, Type currentTileType)
        {
            base.move(c, currentTileType);
            if (currentTileType == typeof(Forest))
            {
                this.movementPoint -= (double)COSTMVT / 2;
            }
            else
            {
                this.movementPoint -= COSTMVT;
            }
        }
    }

    public class UnitDwarf : Unit
    {
        public override void move(Coordinate c, Type currentTileType)
        {
            base.move(c, currentTileType);
            if (currentTileType == typeof(Plain))
            {
                this.movementPoint -= (double) COSTMVT / 2;
            }
            else
            {
                this.movementPoint -= COSTMVT;
            }
        }
    }

    public class UnitOrc : Unit
    {
        public override void move(Coordinate c, Type currentTileType)
        {
            base.move(c, currentTileType);
            if (currentTileType == typeof(Plain))
            {
                this.movementPoint -= (double)COSTMVT / 2;
            }
            else
            {
                this.movementPoint -= COSTMVT;
            }
        }
    }
}
