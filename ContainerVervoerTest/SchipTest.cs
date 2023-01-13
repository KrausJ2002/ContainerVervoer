using ContainerVervoer.Classes;
using ContainerVervoer.Classes.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContainerVervoerTest
{
    [TestClass]
    public class SchipTest
    {
        [TestMethod]
        public void SortLightToHeavy_SortsVakkenInOrderOfMiddenLightHeavy()
        {
            // Arrange
            Schip schip = new Schip(5, 20);
            Vak vak1 = new Vak(ContainerVervoer.Classes.Enums.Positie.midden, ContainerVervoer.Classes.Enums.Kant.midden);
            vak1.containers.Add(new Container(100, ContainerType.normaal));
            vak1.containers.Add(new Container(100, ContainerType.normaal));
            Vak vak2 = new Vak(ContainerVervoer.Classes.Enums.Positie.midden, ContainerVervoer.Classes.Enums.Kant.links);
            vak2.containers.Add(new Container(50, ContainerType.normaal));
            Vak vak3 = new Vak(ContainerVervoer.Classes.Enums.Positie.midden, ContainerVervoer.Classes.Enums.Kant.rechts);
            vak3.containers.Add(new Container(75, ContainerType.normaal));
            schip.vakken.AddRange(new List<Vak> { vak1, vak2, vak3 });

            // Act
            schip.SortLightToHeavy();

            // Assert
            List<Vak> expected = new List<Vak> { vak1, vak2, vak3 };
            CollectionAssert.AreEqual(expected, schip.vakken);
        }

        [TestMethod]
        public void SortLightToHeavy_EmptyVakkenList_SortsCorrectly()
        {
            // Arrange
            Schip schip = new Schip(1000, 5);

            // Act
            schip.SortLightToHeavy();

            // Assert
            Assert.AreEqual(0, schip.vakken.Count);
        }

        [TestMethod]
        public void ContainerFit_TotalWeightExceedsMaxGewicht_ReturnsFalse()
        {
            // Arrange
            Schip schip = new Schip(200, 2);
            Container container1 = new Container(100, ContainerType.normaal);
            Container container2 = new Container(150, ContainerType.normaal);

            // Act
            schip.vakken.Add(new Vak(Positie.voorkant, Kant.links));
            schip.vakken[0].containers.Add(container1);
            bool result = schip.ContainerFit(container2);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ContainerFit_ReturnsTrueIfSuitableVakFound()
        {
            // Arrange
            Schip ship = new Schip(500, 2);
            Vak vak1 = new Vak(ContainerVervoer.Classes.Enums.Positie.voorkant, ContainerVervoer.Classes.Enums.Kant.links);
            Vak vak2 = new Vak(ContainerVervoer.Classes.Enums.Positie.voorkant, ContainerVervoer.Classes.Enums.Kant.rechts);
            ship.vakken.Add(vak1);
            ship.vakken.Add(vak2);
            Container container = new Container(100, ContainerType.normaal);

            // Act
            bool result = ship.ContainerFit(container);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ContainerFit_EmptyVakkenList_ReturnsFalse()
        {
            Schip schip = new Schip(1000, 5);
            Container container = new Container(100, ContainerType.normaal);
            Assert.IsFalse(schip.ContainerFit(container));
        }

        [TestMethod]
        public void IsBalanced_WeightUnbalanced_ReturnsFalse()
        {
            Schip schip = new Schip(1000, 2);
            Vak leftVak = new Vak(Positie.voorkant, Kant.links);
            Vak rightVak = new Vak(Positie.voorkant, Kant.rechts);
            schip.vakken.Add(leftVak);
            schip.vakken.Add(rightVak);
            Container leftContainer = new Container(10, ContainerType.normaal);
            Container rightContainer = new Container(10, ContainerType.normaal);
            leftVak.PlaceContainer(leftContainer);
            rightVak.PlaceContainer(rightContainer);
            Container heavyContainer = new Container(100, ContainerType.normaal);
            Assert.IsFalse(schip.IsBalanced(heavyContainer.gewicht, leftVak));
        }

        [TestMethod]
        public void IsBalanced_WeightBalanced_ReturnsTrue()
        {
            Schip schip = new Schip(1000, 2);
            Vak leftVak = new Vak(Positie.voorkant, Kant.links);
            Vak rightVak = new Vak(Positie.voorkant, Kant.rechts);
            schip.vakken.Add(leftVak);
            schip.vakken.Add(rightVak);
            Container leftContainer = new Container(10, ContainerType.normaal);
            Container rightContainer = new Container(10, ContainerType.normaal);
            leftVak.PlaceContainer(leftContainer);
            rightVak.PlaceContainer(rightContainer);
            Container heavyContainer = new Container(2, ContainerType.normaal);
            Assert.IsTrue(schip.IsBalanced(heavyContainer.gewicht, leftVak));
        }

        [TestMethod]
        public void ReturnVoor_VakkenListWithVariousPositions_ReturnsListOfVakkenWithVoorkantPosition()
        {
            // Arrange
            Schip schip = new Schip(1000, 5);
            Vak vak1 = new Vak(Positie.voorkant, Kant.midden);
            Vak vak2 = new Vak(Positie.midden, Kant.midden);
            Vak vak3 = new Vak(Positie.achterkant, Kant.midden);
            Vak vak4 = new Vak(Positie.voorkant, Kant.midden);
            schip.vakken.Add(vak1);
            schip.vakken.Add(vak2);
            schip.vakken.Add(vak3);
            schip.vakken.Add(vak4);

            // Act
            List<Vak> result = schip.ReturnVoor();

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.All(vak => vak.positie == Positie.voorkant));
        }

        [TestMethod]
        public void ReturnMiddenVoorAchter_VariousPositions_ReturnsCorrectList()
        {
            Schip schip = new Schip(1000, 5);

            // Create a list of vakken with various positions
            List<Vak> vakken = new List<Vak>
            {
                new Vak(Positie.midden, Kant.links),
                new Vak(Positie.voorkant, Kant.midden),
                new Vak(Positie.achterkant, Kant.rechts),
                new Vak(Positie.voorkant, Kant.links),
                new Vak(Positie.midden, Kant.rechts)
            };

            schip.vakken = vakken;

            // Call the ReturnMiddenVoorAchter method and store the returned list
            List<Vak> returnedVakken = schip.ReturnMiddenVoorAchter();

            // Check that the returned list contains vakken with the positions of midden, voorkant, and achterkant
            Assert.IsTrue(returnedVakken.Any(v => v.positie == Positie.midden));
            Assert.IsTrue(returnedVakken.Any(v => v.positie == Positie.voorkant));
            Assert.IsTrue(returnedVakken.Any(v => v.positie == Positie.achterkant));
        }

        [TestMethod]
        public void ReturnVoorAchter_MultipleVakken_ReturnsCorrectList()
        {
            // Arrange
            Schip schip = new Schip(1000, 5);
            Vak vak1 = new Vak(Positie.achterkant, Kant.links);
            Vak vak2 = new Vak(Positie.midden, Kant.links);
            Vak vak3 = new Vak(Positie.voorkant, Kant.links);
            Vak vak4 = new Vak(Positie.achterkant, Kant.rechts);
            Vak vak5 = new Vak(Positie.midden, Kant.rechts);
            Vak vak6 = new Vak(Positie.voorkant, Kant.rechts);
            schip.vakken.Add(vak1);
            schip.vakken.Add(vak2);
            schip.vakken.Add(vak3);
            schip.vakken.Add(vak4);
            schip.vakken.Add(vak5);
            schip.vakken.Add(vak6);

            // Act
            List<Vak> voorAchterVakken = schip.ReturnVoorAchter();

            // Assert
            Assert.AreEqual(4, voorAchterVakken.Count);
            Assert.IsTrue(voorAchterVakken.Contains(vak1));
            Assert.IsTrue(voorAchterVakken.Contains(vak3));
            Assert.IsTrue(voorAchterVakken.Contains(vak4));
            Assert.IsTrue(voorAchterVakken.Contains(vak6));
        }
    }
}