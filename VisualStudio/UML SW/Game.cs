﻿using System;
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

        public int currentRoundNumber
        {
            get;
            set;
        }

        public Game() {
            currentRoundNumber = 0;
        }


        public void endRound(Player player)
        {
            throw new System.NotImplementedException();
        }

        public Unit selectUnit()
        {
            throw new System.NotImplementedException();
        }

        public ITile selectTile()
        {
            throw new System.NotImplementedException();
        }

        public void action()
        {
            throw new System.NotImplementedException();
            //TODO : select enemy unit with the most HP on defined tile
            //then attack
            // if enemy on tile then attack, else move to tile

        }

        public void calculScore()
        {
            throw new System.NotImplementedException();
        }
    }
}
