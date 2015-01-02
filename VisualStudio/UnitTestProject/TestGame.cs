using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UML_SW;
using System.Collections.Generic;

namespace UnitTestProject
{
    [TestClass]
    public class TestGame
    {
        [TestMethod]
        public void TestTools()
        {
            BuilderNewGame bng = new BuilderNewGame("Laura", "Thibault", FactoryPopulation.populationType.Elf, FactoryPopulation.populationType.Orc, Map.mapType.DEMO);
            bng.createGame();
            bng.createMap();
            bng.createPlayers();
            bng.placeUnitsOnMap();

            bng.game.playerOne.units[0].coordinate = new Coordinate(1,0);
            bng.game.playerTwo.units[0].coordinate = new Coordinate(2, 0);
            bng.game.playerOne.playing = true;
            bng.game.playerTwo.playing = false;

            bng.game.selectUnit(bng.game.playerOne.units[0]);
            bng.game.selectTile(new Coordinate(2, 0));


            /****** TEST selectEnemyUnitsOnCoordinates ******/
            //Test no enemy
            List<Unit> lenemy = bng.game.selectEnemyUnitsOnCoordinates(new Coordinate(1,0));
            Assert.AreEqual(0, lenemy.Count);

            //Test dead enemy
            bng.game.playerTwo.units[0].die();
            lenemy = bng.game.selectEnemyUnitsOnCoordinates(bng.game.currentSelectedTileCoordinate);
            Assert.AreEqual(0, lenemy.Count);

            //Test enemy
            bng.game.playerTwo.units[0].isAlive = true;
            lenemy = bng.game.selectEnemyUnitsOnCoordinates(bng.game.currentSelectedTileCoordinate);
            Assert.AreEqual(1, lenemy.Count);
            Assert.AreSame(bng.game.playerTwo.units[0], lenemy[0]);

            bng.game.playerTwo.units[1].coordinate = new Coordinate(2, 0);
            lenemy = bng.game.selectEnemyUnitsOnCoordinates(bng.game.currentSelectedTileCoordinate);
            Assert.AreEqual(2, lenemy.Count);


            /****** TEST selectBestOpponent ******/
            bng.game.playerTwo.units[0].healthPoint = 4;
            Unit bestO = bng.game.selectBestOpponent(lenemy);
            Assert.AreSame(bng.game.playerTwo.units[1], bestO);


            /****** TEST getTypeofTile ******/
            FactoryTile ft = new FactoryTile();
            bng.game.map.tilesList[1] = ft.getOrCreatePlain();
            Assert.AreEqual(bng.game.getTypeofTile(new Coordinate(1,0)), typeof(Plain));


            /****** TEST isActionPossible ******/
            Assert.IsTrue(bng.game.isActionPossible());

            bng.game.playerOne.units[0].movementPoint = 0;
            Assert.IsFalse(bng.game.isActionPossible());

            bng.game.playerOne.units[0].movementPoint = 0.5;
            Assert.IsFalse(bng.game.isActionPossible());

            bng.game.map.tilesList[1] = ft.getOrCreateForest();
            Assert.IsTrue(bng.game.isActionPossible());
        }

        [TestMethod]
        public void TestAction()
        {
            BuilderNewGame bng = new BuilderNewGame("Laura", "Thibault", FactoryPopulation.populationType.Elf, FactoryPopulation.populationType.Orc, Map.mapType.DEMO);
            bng.createGame();
            bng.createMap();
            bng.createPlayers();
            bng.placeUnitsOnMap();

            bng.game.playerOne.units[0].coordinate = new Coordinate(1, 1);
            bng.game.playerTwo.units[0].coordinate = new Coordinate(2, 0);
            bng.game.playerTwo.units[1].coordinate = new Coordinate(2, 0);
            bng.game.playerTwo.units[0].healthPoint = 4;

            bng.game.playerOne.playing = true;
            bng.game.playerTwo.playing = false;

            bng.game.selectUnit(bng.game.playerOne.units[0]);
            bng.game.selectTile(new Coordinate(1, 0));

            FactoryTile ft = new FactoryTile();
            bng.game.map.tilesList[7] = ft.getOrCreateForest();

            bng.game.action();
            Assert.AreEqual(0.5, bng.game.playerOne.units[0].movementPoint);
            Assert.IsTrue(bng.game.playerOne.units[0].coordinate.Equals(new Coordinate(1, 0)));

            bng.game.selectTile(new Coordinate(2, 0));
            bng.game.map.tilesList[1] = ft.getOrCreatePlain();
            Assert.IsFalse(bng.game.isActionPossible());

            bng.game.map.tilesList[1] = ft.getOrCreateForest();
            bng.game.action();

            Assert.AreEqual(0, bng.game.playerOne.units[0].movementPoint);
            Assert.IsTrue(bng.game.currentSelectedUnit.movementPoint != 5 || bng.game.playerTwo.units[1].movementPoint != 5);

        }

        [TestMethod]
        public void TestScore()
        {
            BuilderNewGame bng = new BuilderNewGame("Laura", "Thibault", FactoryPopulation.populationType.Elf, FactoryPopulation.populationType.Orc, Map.mapType.DEMO);
            bng.createGame();
            bng.createMap();
            bng.createPlayers();
            bng.placeUnitsOnMap();

            Coordinate c1 = bng.game.playerOne.units[0].coordinate;
            Coordinate c2 = bng.game.playerTwo.units[0].coordinate;

            int tileNumber1 = c1.x + bng.game.map.strategy.size * c1.y;
            int tileNumber2 = c2.x + bng.game.map.strategy.size * c2.y;

            FactoryTile ft = new FactoryTile();
            bng.game.map.tilesList[tileNumber1] = ft.getOrCreateDesert();
            bng.game.map.tilesList[tileNumber2] = ft.getOrCreatePlain();

            bng.game.calculScore();
            Assert.AreEqual(0, bng.game.playerOne.currentScore);
            Assert.AreEqual(1, bng.game.playerTwo.currentScore);

            bng.game.playerOne.units[0].coordinate = new Coordinate(1, 0);
            bng.game.playerOne.units[1].coordinate = new Coordinate(1, 0);
            bng.game.map.tilesList[1] = ft.getOrCreateForest();

            bng.game.playerTwo.units[0].bonusPoint = 2;
            bng.game.playerTwo.units[1].coordinate = new Coordinate(2, 0);
            bng.game.map.tilesList[2] = ft.getOrCreateForest();
            bng.game.playerTwo.units[2].coordinate = new Coordinate(3, 0);
            bng.game.map.tilesList[3] = ft.getOrCreatePlain();

            bng.game.calculScore();
            Assert.AreEqual(1, bng.game.playerOne.currentScore);
            Assert.AreEqual(4, bng.game.playerTwo.currentScore);
        }
    }
}
