using NUnit.Framework;

namespace VehicleGarage.Tests
{
    public class Tests
    {
        private Garage garage;
        [SetUp]
        public void Setup()
        {
            garage = new Garage(3);
        }

        [Test]
        public void ConstructorWork()
        {
            Assert.IsNotNull(garage);
            Assert.AreEqual(garage.Capacity,3);
            Assert.AreEqual(garage.Vehicles.Count,0);

        }

        [Test]
        public void AddVehicleCorrect()
        {
            Vehicle vehicle = new Vehicle("Opel", "Corsa", "asdf");
            garage.AddVehicle(vehicle);
            Assert.IsFalse(garage.AddVehicle(vehicle));
            Assert.IsTrue(garage.AddVehicle(new Vehicle("vw","golf","p9o9i")));
            Assert.AreEqual(garage.Vehicles.Count,2);
        }
        [Test]
        public void AddVehicleWithOutOfCapacity()
        {
            Vehicle vehicle = new Vehicle("Opel", "Corsa", "asdf");
            garage.AddVehicle(vehicle);
         
            garage.AddVehicle(new Vehicle("vw", "golf", "p9o9i"));
            garage.AddVehicle(new Vehicle("vw", "pasat", "p979i"));
            Assert.IsFalse(garage.AddVehicle(new Vehicle("vw", "Tesla", "p859i")));
            Assert.IsFalse(garage.AddVehicle(new Vehicle("vw", "pasat", "p979i")));
           
        }

        [Test]
        public void ChargeVehiclesCorrectly()
        {
            var car = new Vehicle("vw", "golf", "p9o9i");
            var car1 = new Vehicle("vw", "pasat", "p979i");
            garage.AddVehicle(car1);
            garage.AddVehicle(car);
           

            garage.ChargeVehicles(50);
            Assert.AreEqual(car1.BatteryLevel,100);
            Assert.AreEqual(car.BatteryLevel,100);
        }

        [Test]
        public void ChargeVehiclesWithHighBatteryLevel()
        {
            var car = new Vehicle("vw", "golf", "p9o9i");
            var car1 = new Vehicle("vw", "pasat", "p979i");
            garage.AddVehicle(car1);
            garage.AddVehicle(car);


            garage.ChargeVehicles(200);
            Assert.AreEqual(car1.BatteryLevel, 100);
            Assert.AreEqual(car.BatteryLevel, 100);
        }
        [Test]
        public void ChargeVehicles()
        {
            var car = new Vehicle("vw", "golf", "p9o9i");
            var car1 = new Vehicle("vw", "pasat", "p979i");
            garage.AddVehicle(car1);
            garage.AddVehicle(car);


            garage.ChargeVehicles(0);
            Assert.AreEqual(car1.BatteryLevel, 100);
            Assert.AreEqual(car.BatteryLevel, 100);
        }

        [Test]
        public void DriveVehicle()
        {
            garage.Vehicles.Add(new Vehicle("vw", "golf", "p9o9i"));
            garage.DriveVehicle("p9o9i", 50, false);

            // Assert
            var vehicle = garage.Vehicles.Find(v => v.LicensePlateNumber == "p9o9i");
            Assert.AreEqual(50, vehicle.BatteryLevel);
            Assert.IsFalse(vehicle.IsDamaged);
        }
        [Test]
        public void DriveVehicleValidDriveShouldReduceBatteryLevel()
        {
            // Arrange
            var vehicle = new Vehicle("ABC123", "f452", "ABC123");
            garage.Vehicles.Add(vehicle);
            string licensePlateNumber = "ABC123";
            int batteryDrainage = 50;
            bool accidentOccured = false;

            // Act
            garage.DriveVehicle(licensePlateNumber, batteryDrainage, accidentOccured);

            // Assert
            Assert.AreEqual(50, vehicle.BatteryLevel);
            Assert.IsFalse(vehicle.IsDamaged);
        }
        [Test]
        public void DriveVehicleValidDriveAndAccidentShouldReduceBatteryAndSetAsDamaged()
        {
            // Arrange
            var vehicle = new Vehicle("ABC123", "f452", "ABC123");
            garage.Vehicles.Add(vehicle);
            string licensePlateNumber = "ABC123";
            int batteryDrainage = 50;
            bool accidentOccured = true;

            // Act
            garage.DriveVehicle(licensePlateNumber, batteryDrainage, accidentOccured);

            // Assert
            Assert.AreEqual(50, vehicle.BatteryLevel);
            Assert.IsTrue(vehicle.IsDamaged);
        }
        [Test]
        public void Garage_RepairVehicles()
        {
            Garage garage = new Garage(5);

            Vehicle car = new Vehicle("Peugoet", "208", "CT7006H");
            Vehicle van = new Vehicle("Mercedes-Benz", "Vito", "H7806AH");
            Vehicle truck = new Vehicle("Scania", "Citywide", "P7006XX");
            Vehicle scooter = new Vehicle("Yamaha", "Aerox", "PB6006PA");

            garage.AddVehicle(car);
            garage.AddVehicle(van);
            garage.AddVehicle(truck);
            garage.AddVehicle(scooter);

            garage.DriveVehicle("CT7006H", 51, true);
            garage.DriveVehicle("H7806AH", 51, true);
            garage.DriveVehicle("P7006XX", 51, true);
            garage.DriveVehicle("PB6006PA", 50, false);

            string actualResult = garage.RepairVehicles();
            string expectedResult = "Vehicles repaired: 3";

            Assert.AreEqual(expectedResult, actualResult);
            Assert.IsFalse(car.IsDamaged);
            Assert.IsFalse(van.IsDamaged);
            Assert.IsFalse(truck.IsDamaged);
        }
        [Test]
        public void RepairVehicles_NoDamagedVehicles_ShouldReturnZero()
        {
            // Arrange
            garage.Vehicles.Add(new Vehicle("ABC123", "koko", "123"));
            garage.Vehicles.Add(new Vehicle("XYZ789", "koko", "123"));

            // Act
            string result = garage.RepairVehicles();

            // Assert
            Assert.AreEqual("Vehicles repaired: 0", result);
        }
        [Test]
        public void RepairVehicles_AllVehiclesDamaged_ShouldRepairAllAndReturnCorrectCount()
        {
            // Arrange
            garage.Vehicles.Add(new Vehicle("ABC123", "koko", "123"));
            garage.Vehicles.Add(new Vehicle("XYZ789", "koko", "153"));

            // Act
            string result = garage.RepairVehicles();

            // Assert
            Assert.AreEqual("Vehicles repaired: 0", result);
            Assert.IsFalse(garage.Vehicles[0].IsDamaged);
            Assert.IsFalse(garage.Vehicles[1].IsDamaged);
        }


    }
}