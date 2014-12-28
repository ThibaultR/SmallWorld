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
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public Player playerTwo
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public int currentRoundNumber
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
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
        }

        public void calculScore()
        {
            throw new System.NotImplementedException();
        }
    }
}
