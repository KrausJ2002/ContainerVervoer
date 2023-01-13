using ContainerVervoer.Classes;
using ContainerVervoer.Classes.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace ContainerVervoerTest
{
    [TestClass]
    public class VakTest
    {
        [TestMethod]
        public void TotaalGewicht_EmptyList_ReturnsZero()
        {
            // Arrange
            var vak = new Vak(Positie.voorkant, Kant.links);
            int expectedResult = 0;

            // Act
            int actualResult = vak.TotaalGewicht();

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TotalGewicht_WithDifferentWeightContainers_ReturnsCorrectTotalWeight()
        {
            // Arrange
            Vak vak = new Vak(Positie.voorkant, Kant.links);
            vak.containers = new List<Container>
            {
            new Container(10, ContainerType.waardevol),
            new Container(20, ContainerType.normaal),
            new Container(30, ContainerType.gekoeld)
            };
            int expectedWeight = 60;

            // Act
            int actualWeight = vak.TotaalGewicht();

            // Assert
            Assert.AreEqual(expectedWeight, actualWeight);
        }

        [TestMethod]
        public void ContainerFit_ContainersListEmpty_ReturnsTrue()
        {
            // Arrange
            Vak vak = new Vak(Positie.voorkant, Kant.links);
            Container container = new Container(100, ContainerType.normaal);

            // Act
            bool result = vak.ContainerFit(container);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ContainerFit_TotalWeightLessThanOrEqualTo120_ReturnsTrue()
        {
            // Arrange
            var vak = new Vak(Positie.voorkant, Kant.links);
            vak.containers.Add(new Container(50, ContainerType.normaal));
            vak.containers.Add(new Container(50, ContainerType.normaal));
            Container container = new Container(20, ContainerType.normaal);

            // Act
            bool result = vak.ContainerFit(container);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ContainerFit_TotalWeightGreaterThan120_ReturnsFalse()
        {
            // Arrange
            var vak = new Vak(ContainerVervoer.Classes.Enums.Positie.midden, ContainerVervoer.Classes.Enums.Kant.links);
            var container1 = new Container(50, ContainerType.normaal);
            var container2 = new Container(50, ContainerType.normaal);
            var container3 = new Container(50, ContainerType.normaal);
            var container4 = new Container(50, ContainerType.normaal);
            vak.containers.Add(container1);
            vak.containers.Add(container2);
            vak.containers.Add(container3);

            // Act
            bool result = vak.ContainerFit(container4);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ContainerFit_AddContainerToFullListWithWaardevolContainer_ReturnsFalse()
        {
            // Arrange
            var vak = new Vak(Positie.voorkant, Kant.links);
            var waardevolContainer = new Container(100, ContainerType.waardevol);
            var newContainer = new Container(20, ContainerType.normaal);
            vak.containers.Add(waardevolContainer);

            // Act
            bool result = vak.ContainerFit(newContainer);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ContainerFit_WithLastContainerNotWaardevol_ReturnsTrue()
        {
            // Arrange
            var vak = new Vak(Positie.voorkant, Kant.links);
            vak.containers.Add(new Container(50, ContainerType.normaal));
            var container = new Container(50, ContainerType.normaal);

            // Act
            var result = vak.ContainerFit(container);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void PlaceContainer_AddContainerToList_ContainerAddedToList()
        {
            // Arrange
            Vak vak = new Vak(ContainerVervoer.Classes.Enums.Positie.voorkant, ContainerVervoer.Classes.Enums.Kant.links);
            Container container = new Container(100, ContainerType.normaal);

            // Act
            vak.PlaceContainer(container);

            // Assert
            Assert.IsTrue(vak.containers.Contains(container));
        }
    }
}