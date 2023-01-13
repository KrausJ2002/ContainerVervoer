using ContainerVervoer.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace ContainerVervoerTest
{
    [TestClass]
    public class CrewTest
    {
        [TestMethod]
        public void SortHeavyToLight_ContainersAreSortedCorrectly()
        {
            // Arrange
            var crew = new Crew();
            crew.containers.Add(new Container(20, ContainerType.normaal));
            crew.containers.Add(new Container(10, ContainerType.normaal));
            crew.containers.Add(new Container(30, ContainerType.normaal));

            // Act
            crew.SortHeavyToLight();

            // Assert
            Assert.AreEqual(30, crew.containers[0].gewicht);
            Assert.AreEqual(20, crew.containers[1].gewicht);
            Assert.AreEqual(10, crew.containers[2].gewicht);
        }

        [TestMethod]
        public void SortHeavyToLight_EmptyList_ThrowsException()
        {
            // Arrange
            var crew = new Crew();

            // Act and Assert
            Assert.ThrowsException<Exception>(() => crew.SortHeavyToLight());
        }

        [TestMethod]
        public void SortGekoeldNormaalWaardevol_SortsContainersCorrectly()
        {
            // Arrange
            var crew = new Crew();
            crew.containers.Add(new Container(10, ContainerType.waardevol));
            crew.containers.Add(new Container(20, ContainerType.gekoeld));
            crew.containers.Add(new Container(30, ContainerType.normaal));
            crew.containers.Add(new Container(40, ContainerType.normaal));
            crew.containers.Add(new Container(50, ContainerType.gekoeld));

            // Act
            crew.SortGekoeldNormaalWaardevol();

            // Assert
            Assert.AreEqual(ContainerType.gekoeld, crew.containers[0].type);
            Assert.AreEqual(ContainerType.gekoeld, crew.containers[1].type);
            Assert.AreEqual(ContainerType.normaal, crew.containers[2].type);
            Assert.AreEqual(ContainerType.normaal, crew.containers[3].type);
            Assert.AreEqual(ContainerType.waardevol, crew.containers[4].type);
        }

        [TestMethod]
        public void SortGekoeldNormaalWaardevol_EmptyList_ThrowsException()
        {
            // Arrange
            var crew = new Crew();
            crew.containers = new List<Container>();

            // Act and Assert
            Assert.ThrowsException<Exception>(() => crew.SortGekoeldNormaalWaardevol());
        }

        [TestMethod]
        public void GenerateContainers_AantalIsPositive_NoExceptionThrown()
        {
            // Arrange
            Crew crew = new Crew();
            int aantal = 1;

            // Act
            crew.GenerateContainers(aantal);

            // Assert (no exception should be thrown)
        }

        [TestMethod]
        public void GenerateContainers_AantalIsNegative_ThrowsException()
        {
            // Arrange
            Crew crew = new Crew();
            int aantal = -1;

            // Act
            try
            {
                crew.GenerateContainers(aantal);
                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                // Assert
                Assert.IsInstanceOfType(ex, typeof(Exception));
            }
        }

        [TestMethod]
        public void GenerateContainers_AantalIsZero_ThrowsException()
        {
            // Arrange
            Crew crew = new Crew();
            int aantal = 0;

            // Act
            try
            {
                crew.GenerateContainers(aantal);
                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                // Assert
                Assert.IsInstanceOfType(ex, typeof(Exception));
            }
        }

        [TestMethod]
        public void GenerateContainers_WithValidInput_AddsContainersToList()
        {
            // Arrange
            var crew = new Crew();
            int aantal = 10;

            // Act
            crew.GenerateContainers(aantal);

            // Assert
            Assert.AreEqual(aantal, crew.containers.Count);
        }

        [TestMethod]
        public void GenerateContainers_WithZeroInput_ThrowsException()
        {
            // Arrange
            var crew = new Crew();
            int aantal = 0;

            // Act
            try
            {
                crew.GenerateContainers(aantal);
                Assert.Fail("Expected ArgumentOutOfRangeException not thrown");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                // Assert
                Assert.IsInstanceOfType(ex, typeof(ArgumentOutOfRangeException));
            }
        }

        [TestMethod]
        public void GenerateContainers_WithNegativeInput_ThrowsException()
        {
            // Arrange
            var crew = new Crew();
            int aantal = -5;

            // Act
            try
            {
                crew.GenerateContainers(aantal);
                Assert.Fail("Expected ArgumentOutOfRangeException was not thrown.");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                // Assert
                Assert.IsInstanceOfType(ex, typeof(ArgumentOutOfRangeException));
            }
        }

        [TestMethod]
        public void GenerateSchip_WithValidInput_ReturnsNonNullSchipObject()
        {
            // Arrange
            var crew = new Crew();
            int breedte = 10;
            int lengte = 20;

            // Act
            Schip schip = crew.GenerateSchip(breedte, lengte);

            // Assert
            Assert.IsNotNull(schip);
        }

        [TestMethod]
        public void GenerateSchip_WithValidInput_NumberOfVakkenIsEqualToAantalVakken_EvenBreedte()
        {
            // Arrange
            var crew = new Crew();
            int breedte = 4;
            int lengte = 21;
            int expectedAantalVakken = breedte * lengte;

            // Act
            Schip schip = crew.GenerateSchip(breedte, lengte);
            int actualAantalVakken = schip.vakken.Count;

            // Assert
            Assert.AreEqual(expectedAantalVakken, actualAantalVakken);
        }

        [TestMethod]
        public void GenerateSchip_WithValidInput_NumberOfVakkenIsEqualToAantalVakken_OddBreedte()
        {
            // Arrange
            var crew = new Crew();
            int breedte = 5;
            int lengte = 21;
            int expectedAantalVakken = breedte * lengte;

            // Act
            Schip schip = crew.GenerateSchip(breedte, lengte);
            int actualAantalVakken = schip.vakken.Count;

            // Assert
            Assert.AreEqual(expectedAantalVakken, actualAantalVakken);
        }

        [TestMethod]
        public void GenerateSchip_WithValidInput_MaxGewichtIsCorrect()
        {
            // Arrange
            var crew = new Crew();
            int breedte = 5;
            int lengte = 21;
            int expectedMaxGewicht = breedte * lengte * 150;

            // Act
            Schip schip = crew.GenerateSchip(breedte, lengte);
            int actualMaxGewicht = schip.maxGewicht;

            // Assert
            Assert.AreEqual(expectedMaxGewicht, actualMaxGewicht);
        }

        [TestMethod]
        public void GenerateSchip_WithValidInput_VakkenHaveCorrectPositionsAndSides()
        {
            // Arrange
            var crew = new Crew();
            int breedte = 2;
            int lengte = 4;
            Schip expectedSchip = new Schip(breedte, lengte);
            expectedSchip.vakken = new List<Vak>
                {
                new Vak(ContainerVervoer.Classes.Enums.Positie.voorkant, ContainerVervoer.Classes.Enums.Kant.links),
                new Vak(ContainerVervoer.Classes.Enums.Positie.voorkant, ContainerVervoer.Classes.Enums.Kant.rechts),
                new Vak(ContainerVervoer.Classes.Enums.Positie.midden, ContainerVervoer.Classes.Enums.Kant.links),
                new Vak(ContainerVervoer.Classes.Enums.Positie.midden, ContainerVervoer.Classes.Enums.Kant.rechts),
                new Vak(ContainerVervoer.Classes.Enums.Positie.midden, ContainerVervoer.Classes.Enums.Kant.links),
                new Vak(ContainerVervoer.Classes.Enums.Positie.midden, ContainerVervoer.Classes.Enums.Kant.rechts),
                new Vak(ContainerVervoer.Classes.Enums.Positie.achterkant, ContainerVervoer.Classes.Enums.Kant.links),
                new Vak(ContainerVervoer.Classes.Enums.Positie.achterkant, ContainerVervoer.Classes.Enums.Kant.rechts)
                };

            // Act
            Schip actualSchip = crew.GenerateSchip(breedte, lengte);

            // Assert
            for (int i = 0; i < expectedSchip.vakken.Count; i++)
            {
                Assert.AreEqual(expectedSchip.vakken[i].positie, actualSchip.vakken[i].positie);
                Assert.AreEqual(expectedSchip.vakken[i].kant, actualSchip.vakken[i].kant);
            }
        }

        [TestMethod]
        public void GenerateSchip_WithInvalidBreedteInput_ThrowsException()
        {
            // Arrange
            var crew = new Crew();
            int breedte = 0;
            int lengte = 4;

            // Act and Assert
            Assert.ThrowsException<ArgumentException>(() => crew.GenerateSchip(breedte, lengte));
        }

        [TestMethod]
        public void GenerateSchip_WithInvalidLengteInput_ThrowsException()
        {
            // Arrange
            var crew = new Crew();
            int breedte = 4;
            int lengte = 0;

            // Act and Assert
            Assert.ThrowsException<ArgumentException>(() => crew.GenerateSchip(breedte, lengte));
        }
    }
}