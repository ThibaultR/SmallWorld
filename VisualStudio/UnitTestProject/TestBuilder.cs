using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UML_SW;

namespace UnitTestProject
{
    [TestClass]
    public class TestBuilder
    {
        [TestMethod]
        public void TestNewBuilder()
        {
            BuilderNewGame bng = new BuilderNewGame("Laura", "Thibault", FactoryPopulation.populationType.Elf, FactoryPopulation.populationType.Orc, Map.mapType.DEMO);

            bng.createGame();
            Assert.AreEqual(0, bng.game.currentRoundNumber);

            bng.createMap();
            Assert.AreEqual(4, bng.game.map.strategy.nbUnits);
            Assert.AreEqual(36, bng.game.map.tilesList.Count);

            bng.createPlayers();
            Assert.AreEqual(FactoryPopulation.populationType.Elf, bng.game.playerOne.populationType);
            Assert.AreEqual("Laura", bng.game.playerOne.name);
            Assert.AreEqual(0, bng.game.playerOne.units.Count);
            Assert.AreNotEqual(bng.game.playerOne.playing, bng.game.playerTwo.playing);

            bng.placeUnitsOnMap();
            Assert.AreEqual(4, bng.game.playerOne.units.Count);
            Assert.IsInstanceOfType(bng.game.playerOne.units[0], typeof(UnitElf));
            Assert.IsInstanceOfType(bng.game.playerTwo.units[0], typeof(UnitOrc));
            Assert.AreEqual(bng.game.playerOne.units[0].coordinate.x, bng.game.playerOne.units[3].coordinate.x);
            Assert.AreEqual(bng.game.playerOne.units[0].coordinate.y, bng.game.playerOne.units[3].coordinate.y);
            Assert.AreEqual(6, Math.Abs(bng.game.playerOne.units[0].coordinate.x - bng.game.playerTwo.units[0].coordinate.x));
            Assert.AreEqual(6, Math.Abs(bng.game.playerOne.units[0].coordinate.y - bng.game.playerTwo.units[0].coordinate.y));

        }
    }
}
