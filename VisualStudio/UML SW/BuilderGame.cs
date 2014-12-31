using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wrapper;

namespace UML_SW
{
    public abstract class BuilderGame
    {
        public Game game
        {
            get;
            set;
        }

        public FactoryPopulation.populationType playerOnePopulationType
        {
            get;
            set;
        }

        public FactoryPopulation.populationType playerTwoPopulationType
        {
            get;
            set;
        }

        public string playerOneName
        {
            get;
            set;
        }

        public string playerTwoName
        {
            get;
            set;
        }

        public Map.mapType mapType
        {
            get;
            set;
        }

        public BuilderGame(string playerOneName, string playerTwoName, FactoryPopulation.populationType playerOnePopulationType, FactoryPopulation.populationType playerTwoPopulationType, Map.mapType mapType)
        {
            this.playerOneName = playerOneName;
            this.playerTwoName = playerTwoName;
            this.playerOnePopulationType = playerOnePopulationType;
            this.playerTwoPopulationType = playerTwoPopulationType;
            this.mapType = mapType;
        }


        public Game createGame()
        {
            this.game = new Game();
            return game; //TODO reverifier 
        }

        abstract public void createMap();

        public void createPlayers()
        {
            game.playerOne = new Player(playerOneName, playerOnePopulationType);
            game.playerTwo = new Player(playerTwoName, playerTwoPopulationType);

            Random random = new Random();
            int startingPlayer = random.Next(0, 2);
            if (startingPlayer == 0) {
                game.playerOne.playing = true;
            }
            else
            {
                game.playerTwo.playing = true;
            }
        }

        abstract public void placeUnitsOnMap();

        public void launchGame()
        {
            throw new System.NotImplementedException();
        }

        
    }
}
