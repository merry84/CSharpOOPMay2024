namespace CarManager.Tests
{
    using NUnit.Framework;
    using System;
    using System.Reflection;

    [TestFixture]
    public class CarManagerTests
    {
        private const string make = "Opel";
        private const string model = "Corsa";
        private const double fuelConsumption = 6.70;
        private const double fuelCapacity = 40;
        private Car opel;

        [SetUp]
        public void SetUp()
        {
            opel = new Car(make, model, fuelConsumption, fuelCapacity);
        }

        [Test]
        public void ConstructorWorkCorrectly()
        {
            Car car = new Car(make, model, fuelConsumption, fuelCapacity);
            Assert.AreEqual(make, car.Make);
            Assert.AreEqual(model, car.Model);
            Assert.AreEqual(fuelConsumption, car.FuelConsumption);
            Assert.AreEqual(fuelCapacity, car.FuelCapacity);
            Assert.AreEqual(car.FuelAmount, 0);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void PropertyMakeShouldBeNotNullOrEmpty(string make)
        {
            Assert.Throws<ArgumentException>(() =>
                opel = new Car(make, model, fuelConsumption, fuelCapacity));
        }
        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void PropertyModelShouldBeNotNullOrEmpty(string model)
        {
            Assert.Throws<ArgumentException>(() =>
                opel = new Car(make, model, fuelConsumption, fuelCapacity));
        }
        [Test]
        [TestCase(-3)]
        [TestCase(0)]
        public void PropertyFuelConsumptionShouldBeNotZeroOrNegativeNumber(double fuelConsumption)
        {
            Assert.Throws<ArgumentException>(() =>
                opel = new Car(make, model, fuelConsumption, fuelCapacity));
        }
        [Test]
        [TestCase(-3)]
        [TestCase(0)]
        public void PropertyFuelCapacityShouldBeNotZeroOrNegativeNumber(double fuelCapacity)
        {
            Assert.Throws<ArgumentException>(() =>
                opel = new Car(make, model, fuelConsumption, fuelCapacity));
        }
        [Test]
        public void PropertyFuelAmountShouldBeNotNegativeNumber()
        {
            Assert.Throws<InvalidOperationException>(() =>
                opel.Drive(15));
        }

        [Test]
        [TestCase(1.5)]
        [TestCase(12.4)]
        [TestCase(32.4)]
        public void RefuelMethodWorkCorrectly(double fuelValue)
        {
            double increaseFuel = opel.FuelAmount + fuelValue;
            opel.Refuel(fuelValue);
            Assert.AreEqual(opel.FuelAmount, increaseFuel);
        }
        [Test]
        [TestCase(-1.5)]
        [TestCase(0)]
        public void RefuelMethodThrowExceptionIfFuelIsZeroOrNegativeNumber(double fuelValue)
        {
            Assert.Throws<ArgumentException>(() => opel.Refuel(fuelValue));
        }
        [Test]
        public void RefuelFuelShouldNotBeHigherThanCapacity()
        {
            double fuel = fuelCapacity + 10;
            opel.Refuel(fuel);
            Assert.AreEqual(opel.FuelAmount, fuelCapacity);
        }
        [Test]
        [TestCase(2)]
        [TestCase(12.52)]
        [TestCase(31.65)]
        public void DriveMethodShouldDecreaseFuelAmount(double distance)
        {

            opel.Refuel(50);
            double fuelNeeded = opel.FuelAmount - (distance / 100) * opel.FuelConsumption;
            opel.Drive(distance);
            Assert.AreEqual(fuelNeeded, opel.FuelAmount);
        }
        [Test]
        [TestCase(255)]

        public void DriveMethodThrowException(double distance)
        {


            Assert.Throws<InvalidOperationException>(() => opel.Drive(distance));
        }
    }
}