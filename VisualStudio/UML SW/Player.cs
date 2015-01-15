using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UML_SW
{
    public class Player
    {

        public List<Unit> units { get; set; }

        public String name { get; set; }

        public int currentScore { get; set; }

        public UML_SW.FactoryPopulation.populationType populationType { get; set; }

        public bool playing { get; set; }

        public Player()
        {
            name = "Defaultplayer";
            currentScore = 0;
            units = new List<Unit>();
            playing = false;
        }

        public Player(String playerName, FactoryPopulation.populationType playerPopulationType) {
            name = playerName;
            populationType = playerPopulationType;
            currentScore = 0;
            units = new List<Unit>();
            playing = false;
        }

        public int nbUnitAlive()
        {
            int res = 0;
            foreach (Unit u in units) {
                if (u.isAlive)
                    res++;
            }

            return res;
        }
    }
}
