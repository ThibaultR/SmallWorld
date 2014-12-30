using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UML_SW
{
    public class BuilderSavedGame : BuilderGame
    {
        public BuilderSavedGame(string playerOneName, string playerTwoName, FactoryPopulation.populationType playerOnePopulationType, FactoryPopulation.populationType playerTwoPopulationType, Map.mapType mapType)
            : base(playerOneName, playerTwoName, playerOnePopulationType, playerTwoPopulationType, mapType)
        {
            //TODO add param file
        }


        public override void createMap()
        {
            throw new NotImplementedException();
        }


        public override void placeUnitsOnMap()
        {
            throw new NotImplementedException();
        }

        public void launchGame()
        {
            throw new System.NotImplementedException();
        }
    }
}
