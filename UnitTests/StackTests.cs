using Algoritmiek3;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public class StackTests
    {
        [TestMethod]
        public void PlaceContainer()
        {
            //Arrange
            var side = Side.StackSide.Left;
            List<Container> list = new List<Container>();
            Stack stack = new Stack(0, side);
            Container c = new Container(20, Algoritmiek3.Type.ColdContainers);
            Container c2 = new Container(22, Algoritmiek3.Type.ColdandValuableContainers);
            Container c3 = new Container(23, Algoritmiek3.Type.ColdContainers);
            Container c4 = new Container(22, Algoritmiek3.Type.ColdandValuableContainers);
            list.Add(c);
            list.Add(c2);
            list.Add(c3);
            list.Add(c4);
            var expected = list.Count-1;//Omdat het laaste waardevol is, is het niet toegevoegd.


            //Act
         
            List<Container> containersToPlace = list.OrderBy(x => (byte)x.type).ThenByDescending(a => a.Weight).ToList();
            containersToPlace.ForEach(container =>
            {
               stack.PlaceContainer(container);


            });
            var firstcontainer = stack.containers.First();
            var lastcontainer = stack.containers.Last();



            //Assert
            Assert.IsTrue(firstcontainer.Weight >= lastcontainer.Weight);
            Assert.AreEqual(expected,stack.containers.Count);

        }
        [TestMethod]
        public void Morethan120()
        {
            //Arrange
            var side = Side.StackSide.Left;
            List<Container> list = new List<Container>();
            Stack stack = new Stack(0, side);
            Container c = new Container(10, Algoritmiek3.Type.ColdContainers);
            Container c2 = new Container(30, Algoritmiek3.Type.ColdContainers);
            Container c3 = new Container(30, Algoritmiek3.Type.ColdContainers);
            Container c4 = new Container(30, Algoritmiek3.Type.ColdContainers);
            Container c5 = new Container(30, Algoritmiek3.Type.ColdContainers);
            Container c6 = new Container(30, Algoritmiek3.Type.ColdContainers);
            list.Add(c);
            list.Add(c2);
            list.Add(c3);
            list.Add(c4);
            list.Add(c5);
            list.Add(c6);   

            //Act
            List<Container> containersToPlace = list.OrderBy(x => (byte)x.type).ThenByDescending(a => a.Weight).ToList();
            containersToPlace.ForEach(container =>
            {
                stack.PlaceContainer(container);


            });
            var totalheavyforstack = (stack.containers.Sum(c => c.Weight));
            var firscontainerheavy = stack.containers[0].Weight;//Omdat ik op een container max 120 ton kan plaatsen trek ik het gewicht van eerste container af.


            //Assert
            Assert.IsTrue(totalheavyforstack-firscontainerheavy<=120);
            Assert.AreEqual(5, stack.containers.Count);





        }
    }
}
