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
    public class ContainerTests
    {
        [TestMethod]
        public void CheckContainer()
        {
            //Arrange
            Container container = new Container(30,Algoritmiek3.Type.ColdContainers);
            int expected = 30;
            var expectedtype = Algoritmiek3.Type.ColdContainers;

            //Act

            var result = container.Weight;
            var type = container.type;
            //assert

            Assert.AreEqual(expected, result);
            Assert.AreEqual(expectedtype, type);    
        }




    }
}
