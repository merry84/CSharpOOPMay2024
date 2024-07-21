namespace Railway.Tests
{
    using NUnit.Framework;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    public class Tests
    {
        private RailwayStation station;
        [SetUp]
        public void Setup()
        {
            station = new RailwayStation("Plovdiv");
        }

        [Test]
        public void ValidateCreateNewStationNameIsNull()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                RailwayStation station = new RailwayStation(null);

            });
        }
        [Test]
        public void ValidateCreateNewStationNameIsWhiteSpace()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                RailwayStation station = new RailwayStation(" ");
            });
        }

        [Test]
        public void ValidateCreateNewStationNameIsCorrectly()
        {
            
            var expectedName = "Plovdiv";
            Assert.AreEqual(expectedName, station.Name);

        }
        [Test]
        public void NewArrivalOnBoardCorrectly()
        {
            
            station.NewArrivalOnBoard("sofia-plovdiv");
            Assert.AreEqual(1, station.ArrivalTrains.Count);

        }

        [Test]
        public void DepartureTrainsWorkCorrectly()
        {           
            station.NewArrivalOnBoard("sofia-plovdiv");
            station.NewArrivalOnBoard("plovdiv-svilengrad");
            station.TrainHasArrived("sofia-plovdiv");

            Assert.AreEqual(1, station.DepartureTrains.Count);

        }

        [Test]
        public void TrainHasArrivedCorrectly()
        {
            
            station.NewArrivalOnBoard("sofia-plovdiv");
            station.NewArrivalOnBoard("plovdiv-svilengrad");
            var actualResult = station.TrainHasArrived("sofia-plovdiv");
            var expectedResult = "sofia-plovdiv is on the platform and will leave in 5 minutes.";
            Assert.AreEqual(actualResult, expectedResult);
        }
        [Test]
        public void TrainHasArrivedThrows()
        {
            
            station.NewArrivalOnBoard("sofia-plovdiv");
            station.NewArrivalOnBoard("plovdiv-svilengrad");
            var actualResult = station.TrainHasArrived("plovdiv-svilengrad");
            var expectedResult = "There are other trains to arrive before plovdiv-svilengrad.";
            Assert.AreEqual(actualResult, expectedResult);
        }

        [Test]
        public void TrainHasLeftWorkCorrectly()
        {
            
            station.NewArrivalOnBoard("sofia-plovdiv");
            station.TrainHasArrived("sofia-plovdiv");
            var actualResult = station.TrainHasLeft("sofia-plovdiv");
            Assert.That(actualResult, Is.True);

        }
        [Test]
        public void TrainHasLeftNotWorkCorrectly()
        {
            station.NewArrivalOnBoard("sofia-plovdiv");
            station.NewArrivalOnBoard("plovdiv-svilengrad");
            station.TrainHasArrived("sofia-plovdiv");
            var actualResult = station.TrainHasLeft("plovdiv-svilengrad");
            Assert.That(actualResult, Is.False);

        }
        [Test]
        public void ValidateTrainHasLeftDeququed()
        {           
            station.NewArrivalOnBoard("sofia-plovdiv");
            station.TrainHasArrived("sofia-plovdiv");

            Assert.That(station.DepartureTrains.Count == 1);

            station.TrainHasLeft("sofia-plovdiv");

            Assert.That(station.DepartureTrains.Count == 0);
        }
    }
}