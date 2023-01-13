using Algoritmiek3;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Type = Algoritmiek3.Type;

namespace UnitTests
{
    [TestClass]
    public class ShipTests
    {
        //Hierbij Controleer ik schip zonder gewicht of het veilig is.
        [TestMethod]
        public void IsMyShipSafe_ReturnFalse()
        {
            //Arrange
            Ship ship = new Ship(30, 50, 100);
            bool expected = false;

            //Act
            bool result = ship.IsSafe();


            //Assert
            Assert.AreEqual(expected, result);


        }

        //Hierbij Controleer ik schip zonder gewicht of het veilig is.
        [TestMethod]
        public void IsMyShipSafe_ReturnTrue()
        {
            //Arrange
            Ship ship = new Ship(30, 50, 100);
            bool expected = true;

            //Act
            List<Container> containersToPlace = new List<Container>();
            Random rnd = new Random();
            int containerCount = 50;
            for (int i = 0; i < containerCount; i++)
            {
                containersToPlace.Add(new Container(rnd.Next(10, 26), (Algoritmiek3.Type)(rnd.Next(0, 4))));
            }
            containersToPlace.ForEach(container =>
            {
                ship.PlaceContainer(container);
            });

            bool result = ship.IsSafe();


            //Assert
            Assert.AreEqual(expected, result);


        }
        [TestMethod]
        public void CreateRow()
        {
            //Arrange
            Ship ship = new Ship(30, 50, 100);
            int expected = ship.rows.Count + 1;

            //Act extra uitleg:zodra een schip aanmakt, dan wordt 3 rijen aangemakt worden en hierbij voeg ik nog eentje toe dus in totaal 4.
            var result = ship.CreateRow(0);


            //Assert
            Assert.AreEqual(expected, result);

        }
        [TestMethod]
        public void PlaceContainer()
        {
            //Arrange
            Ship ship = new Ship(30, 50, 100);
            List<Container> containersToPlace = new List<Container>();

            Random rnd = new Random();


            int containerCount = 50;


            //Act
            for (int i = 0; i < containerCount; i++)
            {
                containersToPlace.Add(new Container(rnd.Next(10, 26), (Type)(rnd.Next(0, 4))));
            }
            containersToPlace = containersToPlace.OrderBy(x => (byte)x.type).ThenByDescending(a => a.Weight).ToList();
            containersToPlace.ForEach(container =>
            {
                var result = ship.PlaceContainer(container);
            });

            bool isColdContaineratrow2 = ship.rows[1].stacks[1].containers.Any(c => c.type == Type.ColdContainers);
            bool isvaluablecontaineratrow2 = ship.rows[1].stacks[0].containers.Any(c=>c.type==Type.ValuableContainers);
            int ismorethan120 = ship.rows[1].stacks[0].containers.Sum(c => c.Weight);
            bool isvaluableOnthetop = ship.rows[0].stacks[1].containers[0].type == Type.ValuableContainers;
            bool iscoldandvaluableonthetop = ship.rows[0].stacks[0].containers[0].type == Type.ColdandValuableContainers;



            //Assert
            Assert.IsFalse(isColdContaineratrow2);
            Assert.IsFalse(isvaluablecontaineratrow2);
            Assert.IsTrue(ismorethan120 <= 120);
            Assert.IsFalse(isvaluableOnthetop);
            Assert.IsFalse(iscoldandvaluableonthetop);

        }
        //Controleer of rijen toegevoegd zijn
        [TestMethod]
        public void Init()
        {
            //Arrange
            Ship ship = new Ship(30, 50, 100);
            int expected = ship.rows.Count;

            //Act
            var result = ship.init();



            //Assert
            Assert.AreEqual(expected, result);


        }
       
    }
}