using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wrapper;

namespace UML_SW
{
    public abstract class BuilderGame
    {
        protected Game game
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

        public Game createGame()
        {
            this.game = new Game();
            return game; //TODO reverifier 
        }

        protected void createMap()
        {
            this.game.map = new Map();
            this.game.map.setStrategy(mapType);
            this.game.map.createMap();
        }

        protected void createPlayers() {
            game.playerOne = new Player(playerOneName, playerOnePopulationType);
            game.playerTwo = new Player(playerTwoName, playerTwoPopulationType);
        }

        unsafe protected void placeUnitsOnMap() {
            FactoryPopulation fp = new FactoryPopulation();

            WrapperAlgo algoW = new WrapperAlgo();
            int* tabCoord = algoW.findStartCoordinate(game.map.strategy.size);


            for (int i = 0; i < game.map.strategy.nbUnits; i++) {
                Unit tempUnit1 = fp.createUnit(this.game.playerOne.populationType);
                tempUnit1.coordinate = new Coordinate(tabCoord[0], tabCoord[1]);
                game.playerOne.units.Add(tempUnit1);

                Unit tempUnit2 = fp.createUnit(this.game.playerTwo.populationType);
                tempUnit2.coordinate = new Coordinate(tabCoord[2], tabCoord[3]);
                game.playerTwo.units.Add(tempUnit2);
            }
        }

        protected void launchGame()
        {
            throw new System.NotImplementedException();
        }
    }
}
