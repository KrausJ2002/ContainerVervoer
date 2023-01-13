using ContainerVervoer.Classes;
using ContainerVervoer.Classes.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerVervoerTest
{
    [TestClass]
    public class ProgramTest
    {
        [TestMethod]
        public void Master()
        {
            //Arrange
            Crew crew = new();
            crew.containers.Add(new(30, ContainerType.gekoeld));
            crew.containers.Add(new(24, ContainerType.normaal));
            crew.containers.Add(new(29, ContainerType.normaal));
            crew.containers.Add(new(5, ContainerType.normaal));
            crew.containers.Add(new(15, ContainerType.normaal));
            crew.containers.Add(new(14, ContainerType.waardevol));
            crew.containers.Add(new(29, ContainerType.gekoeld));
            crew.containers.Add(new(24, ContainerType.waardevol));
            crew.containers.Add(new(30, ContainerType.normaal));
            crew.containers.Add(new(4, ContainerType.gekoeld));
            crew.containers.Add(new(30, ContainerType.gekoeld));
            crew.containers.Add(new(24, ContainerType.normaal));
            crew.containers.Add(new(29, ContainerType.normaal));
            crew.containers.Add(new(5, ContainerType.normaal));
            crew.containers.Add(new(15, ContainerType.normaal));
            crew.containers.Add(new(23, ContainerType.normaal));
            crew.containers.Add(new(21, ContainerType.normaal));
            crew.containers.Add(new(13, ContainerType.waardevol));
            crew.containers.Add(new(30, ContainerType.gekoeld));
            crew.containers.Add(new(24, ContainerType.waardevol));
            crew.containers.Add(new(30, ContainerType.normaal));
            crew.containers.Add(new(4, ContainerType.gekoeld));

            Schip schip = crew.GenerateSchip(3, 3);
            ProgramClass programClass = new ProgramClass();

            //Act
            Schip returnedSchip = programClass.Master(crew, schip);

            //Assert
            Container returnContainer(Kant kant, Positie positie, int nummer)
            {
                return schip.vakken.Where(v => v.kant == kant && v.positie == positie).ToList()[0].containers[nummer];
            }

            Assert.AreEqual(returnContainer(Kant.midden, Positie.voorkant, 0).gewicht, 30);
            Assert.AreEqual(returnContainer(Kant.midden, Positie.voorkant, 1).gewicht, 30);
            Assert.AreEqual(returnContainer(Kant.midden, Positie.voorkant, 2).gewicht, 30);
            Assert.AreEqual(returnContainer(Kant.midden, Positie.voorkant, 3).gewicht, 29);
            Assert.AreEqual(returnContainer(Kant.midden, Positie.voorkant, 4).gewicht, 4);
            Assert.AreEqual(returnContainer(Kant.midden, Positie.voorkant, 5).gewicht, 4);
            Assert.AreEqual(returnContainer(Kant.midden, Positie.voorkant, 6).gewicht, 13);
            Assert.AreEqual(returnContainer(Kant.midden, Positie.midden, 0).gewicht, 30);
            Assert.AreEqual(returnContainer(Kant.midden, Positie.midden, 1).gewicht, 30);
            Assert.AreEqual(returnContainer(Kant.midden, Positie.midden, 2).gewicht, 29);
            Assert.AreEqual(returnContainer(Kant.midden, Positie.midden, 3).gewicht, 29);
            Assert.AreEqual(returnContainer(Kant.midden, Positie.midden, 4).gewicht, 24);
            Assert.AreEqual(returnContainer(Kant.midden, Positie.midden, 5).gewicht, 5);
            Assert.AreEqual(returnContainer(Kant.midden, Positie.achterkant, 0).gewicht, 24);
            Assert.AreEqual(returnContainer(Kant.rechts, Positie.midden, 0).gewicht, 24);
            Assert.AreEqual(returnContainer(Kant.rechts, Positie.midden, 1).gewicht, 15);
            Assert.AreEqual(returnContainer(Kant.rechts, Positie.midden, 2).gewicht, 15);
            Assert.AreEqual(returnContainer(Kant.rechts, Positie.achterkant, 0).gewicht, 14);
            Assert.AreEqual(returnContainer(Kant.links, Positie.midden, 0).gewicht, 23);
            Assert.AreEqual(returnContainer(Kant.links, Positie.midden, 1).gewicht, 21);
            Assert.AreEqual(returnContainer(Kant.links, Positie.midden, 2).gewicht, 5);
            Assert.AreEqual(returnContainer(Kant.links, Positie.achterkant, 0).gewicht, 24);

            Assert.AreEqual(returnContainer(Kant.midden, Positie.voorkant, 0).type, ContainerType.gekoeld);
            Assert.AreEqual(returnContainer(Kant.midden, Positie.voorkant, 1).type, ContainerType.gekoeld);
            Assert.AreEqual(returnContainer(Kant.midden, Positie.voorkant, 2).type, ContainerType.gekoeld);
            Assert.AreEqual(returnContainer(Kant.midden, Positie.voorkant, 3).type, ContainerType.gekoeld);
            Assert.AreEqual(returnContainer(Kant.midden, Positie.voorkant, 4).type, ContainerType.gekoeld);
            Assert.AreEqual(returnContainer(Kant.midden, Positie.voorkant, 5).type, ContainerType.gekoeld);
            Assert.AreEqual(returnContainer(Kant.midden, Positie.voorkant, 6).type, ContainerType.waardevol);
            Assert.AreEqual(returnContainer(Kant.midden, Positie.midden, 0).type, ContainerType.normaal);
            Assert.AreEqual(returnContainer(Kant.midden, Positie.midden, 1).type, ContainerType.normaal);
            Assert.AreEqual(returnContainer(Kant.midden, Positie.midden, 2).type, ContainerType.normaal);
            Assert.AreEqual(returnContainer(Kant.midden, Positie.midden, 3).type, ContainerType.normaal);
            Assert.AreEqual(returnContainer(Kant.midden, Positie.midden, 4).type, ContainerType.normaal);
            Assert.AreEqual(returnContainer(Kant.midden, Positie.midden, 5).type, ContainerType.normaal);
            Assert.AreEqual(returnContainer(Kant.midden, Positie.achterkant, 0).type, ContainerType.waardevol);
            Assert.AreEqual(returnContainer(Kant.rechts, Positie.midden, 0).type, ContainerType.normaal);
            Assert.AreEqual(returnContainer(Kant.rechts, Positie.midden, 1).type, ContainerType.normaal);
            Assert.AreEqual(returnContainer(Kant.rechts, Positie.midden, 2).type, ContainerType.normaal);
            Assert.AreEqual(returnContainer(Kant.rechts, Positie.achterkant, 0).type, ContainerType.waardevol);
            Assert.AreEqual(returnContainer(Kant.links, Positie.midden, 0).type, ContainerType.normaal);
            Assert.AreEqual(returnContainer(Kant.links, Positie.midden, 1).type, ContainerType.normaal);
            Assert.AreEqual(returnContainer(Kant.links, Positie.midden, 2).type, ContainerType.normaal);
            Assert.AreEqual(returnContainer(Kant.links, Positie.achterkant, 0).type, ContainerType.waardevol);
        }
    }
}
