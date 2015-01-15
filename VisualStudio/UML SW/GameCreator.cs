using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UML_SW
{
    public class GameCreator
    {
        public BuilderGame builderGame { get; set; }
    
        public void createGame()
        {
            builderGame.createGame();
            builderGame.createMap();
            builderGame.createPlayers();
            builderGame.placeUnitsOnMap();
        }
    }
}
