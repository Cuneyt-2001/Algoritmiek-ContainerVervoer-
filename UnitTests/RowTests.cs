using Algoritmiek3;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Algoritmiek3.Side;

namespace UnitTests
{
    [TestClass]
    public class RowTests
    {
        //Ship is empty
        [TestMethod]
        public void CalculateRightSideWeight()
        {
            //Arrange
            Ship ship = new Ship(30, 50, 100);
            Row row = new Row(0, ship.ShipWidth, 10, ship);
            var expected = row.TotalWeight() - row.LeftSideWeight();


            //Act
            var result = row.RightSideWeight();


            //Assert
            Assert.AreEqual(expected, result);

        }
        [TestMethod]
        public void CalculateRghtSideWithHeavy()
        {
            //Arrange
            Ship ship = new Ship(30, 50, 100);
            Row row = new Row(0, ship.ShipWidth, 10, ship);
            var expectedheavy = 10;

            //Act

            ship.PlaceContainer(new Container(20, Algoritmiek3.Type.ColdContainers));//Ik begin via linker kant daarom en dat word gestapeld in mid kant en die container verdeeld aan linker kant als 10 en rechterkant als 10
            var result = row.RightSideWeight();


            //Assert

            Assert.AreEqual(expectedheavy, result);


        }
        [TestMethod]
        public void CalculateLeftSideHeavy()
        {
            //Arrange
            Ship ship = new Ship(30, 50, 100);
            Row row = new Row(0, ship.ShipWidth, 10, ship);
            var expectedheavy = 15;

            //Act

            ship.PlaceContainer(new Container(30, Algoritmiek3.Type.ColdContainers));//Ik begin via linker kant omdat eerste container op mid gestapeld word
            var result = row.LeftSideWeight();


            //Assert

            Assert.AreEqual(expectedheavy, result);




        }
        [TestMethod]
        public void CalculateTotalHeavy()//het berekent totale massa van het schip.
        {
            //Arrange
            Ship ship = new Ship(30, 50, 100);
            Row row = new Row(0, ship.ShipWidth, 10, ship);
            Container c = new Container(17, Algoritmiek3.Type.ColdContainers);
            var expectedheavy = c.Weight;


            //Act

            ship.PlaceContainer(c);
            var result = row.TotalWeight();


            //Assert

            Assert.AreEqual(expectedheavy, result);


        }

        [TestMethod]
        public void FindStackSide()
        {
            //Arrange
            Ship ship = new Ship(30, 50, 100);
            Row row = new Row(0, ship.ShipWidth, 10, ship);
            var expectedside = Side.StackSide.Mid;
            int stackindex = 1;
            Container c = new Container(17, Algoritmiek3.Type.ColdContainers);

            //Act

            ship.PlaceContainer(c);
            var result = row.FindStackSide(ship.ShipWidth, 10, stackindex);
            var istrueside = row.IsTrueSide(c, result);


            //Assert

            Assert.AreEqual(expectedside, result);
            Assert.AreNotEqual(istrueside, result);



        }
        [TestMethod]
        public void PlaceOneContainer()
        {
            //Arrange
            Ship ship = new Ship(30, 50, 100);
            Row row = new Row(0, ship.ShipWidth, 10, ship);
            Container c = new Container(20, Algoritmiek3.Type.ColdContainers);

            //Act

            var placed = row.PlaceContainer(c);// Eerste container wordt altijd op middenkant gestapeld
            var leftstack = row.stacks[0].containers.Count();
            var midstack = row.stacks[1].containers.Count();
            var rightstack = row.stacks[2].containers.Count();


            //Assert

            Assert.IsTrue(placed);
            Assert.IsTrue(midstack > leftstack);
            Assert.AreEqual(leftstack, rightstack);
            Assert.AreEqual(c.Weight, row.stacks[1].containers.Sum(x => x.Weight));



        }
        [TestMethod]
        public void CheckBalance()
        {
            //Arrange
            Ship ship = new Ship(30, 50, 100);
            Row row = new Row(0, ship.ShipWidth, 10, ship);
            List<Container> containersToPlace = new List<Container>();

            Random rnd = new Random();


            int containerCount = 50;


            //Act
            for (int i = 0; i < containerCount; i++)
            {
                containersToPlace.Add(new Container(rnd.Next(10, 26), (Algoritmiek3.Type)(rnd.Next(0, 4))));
            }
            containersToPlace = containersToPlace.OrderBy(x => (byte)x.type).ThenByDescending(a => a.Weight).ToList();
            containersToPlace.ForEach(container =>
            {
                var result = ship.PlaceContainer(container);
            });

            var heavyforLeft = row.LeftSideWeight();
            var heavyforRight = row.RightSideWeight();
            var result=Math.Abs(heavyforLeft - heavyforRight);
            var total = heavyforLeft + heavyforRight;




            //Assert
            Assert.IsTrue(total* 0.2 >= result); // Als het true is, dan schip heeft balans.







        }















    }
}
