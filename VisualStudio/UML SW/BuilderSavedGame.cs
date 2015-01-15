using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace UML_SW
{
    public class BuilderSavedGame : BuilderGame
    {

        public string saveFile { get; set; }

        public BuilderSavedGame(string file)
        {
            this.saveFile = file;
        }

        public override void createGame()
        {
            Stream stream = File.Open(this.saveFile, FileMode.Open);
            BinaryFormatter bFormatter = new BinaryFormatter();
            this.game = (Game)bFormatter.Deserialize(stream);
            stream.Close();
        }


        // It seems that our design Pattern (Builder) doesn't worl really well here : Héritgae refusé
        public BuilderSavedGame(string playerOneName, string playerTwoName, FactoryPopulation.populationType playerOnePopulationType, FactoryPopulation.populationType playerTwoPopulationType, Map.mapType mapType)
            : base(playerOneName, playerTwoName, playerOnePopulationType, playerTwoPopulationType, mapType)
        {
            
        }

        public override void createMap()
        {
        }


        public override void placeUnitsOnMap()
        {
        }

    }
}
