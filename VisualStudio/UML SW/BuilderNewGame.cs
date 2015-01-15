using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wrapper;

namespace UML_SW
{
    public class BuilderNewGame : BuilderGame
    {

        public BuilderNewGame(string playerOneName, string playerTwoName, FactoryPopulation.populationType playerOnePopulationType, FactoryPopulation.populationType playerTwoPopulationType, Map.mapType mapType)
            : base(playerOneName, playerTwoName, playerOnePopulationType, playerTwoPopulationType, mapType)
        {
            
        }

        public override void createMap()
        {
            this.game.map = new Map();
            this.game.map.setStrategy(mapType);
            this.game.map.createMap();
        }


        unsafe public override void placeUnitsOnMap()
        {
            FactoryPopulation fp = new FactoryPopulation();

            WrapperAlgo algoW = new WrapperAlgo();
            int* tabCoord = algoW.findStartCoordinate(game.map.strategy.size);


            for (int i = 0; i < game.map.strategy.nbUnits; i++)
            {
                Unit tempUnit1 = fp.createUnit(this.game.playerOne.populationType);
                tempUnit1.coordinate = new Coordinate(tabCoord[0], tabCoord[1]);
                game.playerOne.units.Add(tempUnit1);

                Unit tempUnit2 = fp.createUnit(this.game.playerTwo.populationType);
                tempUnit2.coordinate = new Coordinate(tabCoord[2], tabCoord[3]);
                game.playerTwo.units.Add(tempUnit2);
            }
        }

    }
}
