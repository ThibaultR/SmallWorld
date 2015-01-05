using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UML_SW
{
    public class Game
    {
        public Map map
        {
            get;
            set;
        }

        public Player playerOne
        {
            get;
            set;
        }

        public Player playerTwo
        {
            get;
            set;
        }

        public double currentRoundNumber
        {
            get;
            set;
        }

        public Unit currentSelectedUnit
        {
            get;
            set;
        }

        public Coordinate currentSelectedTileCoordinate
        {
            get;
            set;
        }

        public Game() {
            currentRoundNumber = 0;
        }


        public void endRound()
        {
            currentRoundNumber += 0.5;
            foreach (Unit u in this.getCurrentPlayer().units) 
            {
                u.movementPoint = Unit.MAX_MVT;
            }
            this.playerOne.playing = !this.playerOne.playing;
            this.playerTwo.playing = !this.playerTwo.playing;
            this.calculScore();
        }

        public Player getCurrentPlayer() {
            if (this.playerOne.playing) { return this.playerOne; }
            else { return this.playerTwo; }
        }

        public Player getEnemyPlayer()
        {
            if (this.playerOne.playing) { return this.playerTwo; }
            else { return this.playerOne; }
        }

        public void selectUnit(Unit u)
        {
            this.currentSelectedUnit = u;
        }

        public void selectTile(Coordinate c)
        {
            this.currentSelectedTileCoordinate = c;
        }
        public Type getTypeofTile(Coordinate c)
        {
            int tileNumber = c.x + map.strategy.size * c.y;
            return this.map.tilesList[tileNumber].GetType();
        }

        public List<Unit> selectEnemyUnitsOnCoordinates(Coordinate c)
        {
            List<Unit> list = new List<Unit>();

            foreach (Unit u in this.getEnemyPlayer().units)
            {
                if (u.isAlive && u.coordinate.Equals(c)) { list.Add(u); }
            }

            return list;
        }


        public Unit selectBestOpponent(List<Unit> list)
        {
            if (list.Count == 0) { return null; }


            Unit bestUnit = list[0];

            foreach (Unit u in list)
            {
                if (u.healthPoint > bestUnit.healthPoint) { bestUnit = u; }
            }

            return bestUnit;
        }

        public bool isMovementPossible(Unit u)
        {
            if (u.isAlive)
            {
                if (u.movementPoint == 0)
                {
                    return false;
                }

                if (u.movementPoint == 1)
                {
                    return true;
                }

                if (u.movementPoint == 0.5)
                {
                    if (getTypeofTile(u.coordinate) == typeof(Forest) && u.GetType() == typeof(UnitElf))
                    {
                        return true;
                    }

                    if (getTypeofTile(currentSelectedUnit.coordinate) == typeof(Plain))
                    {
                        if (u.GetType() == typeof(UnitDwarf) || u.GetType() == typeof(UnitOrc))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public void isTileReachable(Unit unit, Coordinate coordinate)
        {
            throw new NotImplementedException();//TODO
        }

        public bool isActionPossible()
        {
            return isMovementPossible(this.currentSelectedUnit);
            // && isTileReachable(this.currentSelectedUnit, this.currentSelectedTileCoordinate);
            //TODO verif proximity
            
        }


        public void action()
        {
            /*
             * If enemy on tile then 
             *      attack (enemy with most hp)
             * 
             * if enemy on tile then
             *      move to here
             * else 
             *      move to tile
             */

            if (isActionPossible())
            {
                List<Unit> listEnemyUnits = selectEnemyUnitsOnCoordinates(currentSelectedTileCoordinate);

                if (listEnemyUnits.Count > 0) //if there is enemies
                {
                    currentSelectedUnit.attack(selectBestOpponent(listEnemyUnits)); //then attack

                    listEnemyUnits = selectEnemyUnitsOnCoordinates(currentSelectedTileCoordinate);//update list of enemies
                }

                if (listEnemyUnits.Count > 0)//if there is stille enemies
                {
                    currentSelectedUnit.move(currentSelectedUnit.coordinate, getTypeofTile(currentSelectedUnit.coordinate));// move to same location
                }
                else// no (more) enemies
                {
                    currentSelectedUnit.move(currentSelectedTileCoordinate, getTypeofTile(currentSelectedUnit.coordinate));//move to tile
                }

            }

        }

        public int calculScoreSingleUnit(Unit u, List<Coordinate> lc)
        {
            int val = u.bonusPoint;
            if (!lc.Contains(u.coordinate))
            {
                if ((u.GetType() == typeof(UnitElf) && this.getTypeofTile(u.coordinate) == typeof(Desert)) ||
                    (u.GetType() == typeof(UnitOrc) && this.getTypeofTile(u.coordinate) == typeof(Forest)) ||
                    (u.GetType() == typeof(UnitDwarf) && this.getTypeofTile(u.coordinate) == typeof(Plain)))
                {
                    return val;
                }
                else
                {
                    val++;
                }
            }

            return val;
        }

        public void calculScore()
        {
            List<Coordinate> list = new List<Coordinate>();

            int scoreP1 = 0;
            foreach (Unit u in playerOne.units)
            {
                if (u.isAlive) 
                {
                    scoreP1 += calculScoreSingleUnit(u, list);
                    list.Add(u.coordinate);
                }
            }

            playerOne.currentScore = scoreP1;

            list.Clear();

            int scoreP2 = 0;
            foreach (Unit u in playerTwo.units)
            {
                if (u.isAlive)
                {
                    scoreP2 += calculScoreSingleUnit(u, list);
                    list.Add(u.coordinate);
                }
            }

            playerTwo.currentScore = scoreP2;

        }
    }
}
