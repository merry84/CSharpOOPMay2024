namespace SmartDevice.Tests
{
    using NUnit.Framework;
    using System;
    using System.Text;

    public class Tests
    {
        //100/100
        private Device device;

        [SetUp]
        public void Setup()
        {
            device = new Device(350);
        }

        [Test]
        public void Test()
        {
            Assert.AreEqual(device.AvailableMemory,350);
            Assert.AreEqual(device.MemoryCapacity,350);
            Assert.AreEqual(device.Photos,0);
            Assert.AreEqual(device.Applications.Count,0);
            
        }

        [Test]
        public void TakePhotoWork()
        {
           var takePhoto= device.TakePhoto(150);
            Assert.IsTrue(takePhoto);
            Assert.AreEqual(device.AvailableMemory,device.MemoryCapacity- 150);
            Assert.AreEqual(device.Photos,1);
        }

        [Test]
        public void TakePhotoWorkReturnFalse()
        {
            var takePhoto = device.TakePhoto(450);
            Assert.IsFalse(takePhoto);
            //Assert.AreEqual(device.AvailableMemory, device.MemoryCapacity - 150);
            //Assert.AreEqual(device.Photos, 1);
        }
        [Test]
        public void InstallAppWork()
        {
            var expectedResult = device.InstallApp("cat", 100);
            Assert.AreEqual(device.Applications.Count,1);
            Assert.AreEqual(device.AvailableMemory, device.MemoryCapacity - 100);
            Assert.AreEqual(expectedResult,"cat is installed successfully. Run application?");
        }
        [Test]
        public void InstallAppNotEnoughMemory()
        {
            Assert.Throws<InvalidOperationException>(()=> device.InstallApp("cat", 400));
            
        }

        [Test]
        public void FormatDeviceWork()
        {
            device.TakePhoto(100);
            device.InstallApp("dog", 250);
            device.FormatDevice();
            Assert.AreEqual(device.AvailableMemory,350);
            Assert.AreEqual(device.Photos,0);
            Assert.AreEqual(device.Applications.Count,0);
        }

        [Test]
        public void GetDeviceStatusWork()
        {

            device.TakePhoto(100);
            device.InstallApp("dog", 250);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Memory Capacity: 350 MB, Available Memory: 0 MB");
            sb.AppendLine($"Photos Count: 1");
            sb.AppendLine($"Applications Installed: dog");
            var expectedResult = sb.ToString().TrimEnd();
            var actualResult = device.GetDeviceStatus();
            Assert.AreEqual(expectedResult,actualResult);

        }
    }
}