namespace Television.Tests
{
    using System;
    using NUnit.Framework;
    public class Tests
    {
        private TelevisionDevice tv;
        [SetUp]
        public void Setup()
        {
            //string brand, double price, int screenWidth, int screenHeigth
            tv = new TelevisionDevice("soni",400,100,300);
        }

        [Test]
        public void ConstructorWorkCorrectly()
        {
            TelevisionDevice television = new("lg", 560, 150, 250);
            Assert.AreEqual(television.Brand, "lg");
            Assert.AreEqual(television.Price, 560);
            Assert.AreEqual(television.ScreenWidth, 150);
            Assert.AreEqual(television.ScreenHeigth, 250);
            Assert.IsNotNull(television);
        }
        [Test]
        public void SwichOnMethodWorkCorrectly()
        {
            string expectedOutput = "Cahnnel 0 - Volume 13 - Sound On";
            string output = tv.SwitchOn();

            Assert.AreEqual(expectedOutput, output);
        }
        [Test]
        public void LastMuttedMethodWorkCorrectly()
        {
            string expectedOutput = "Cahnnel 0 - Volume 13 - Sound Off";
            tv.MuteDevice();
            string output = tv.SwitchOn();
            Assert.AreEqual(expectedOutput, output);
        }
        [Test]
        public void ChangeChannelToNegativeNumber()
        {
            Assert.Throws<ArgumentException>(() => tv.ChangeChannel(-15));
        }
        [Test]
        public void ChangeChannelCorrectly()
        {
            string expectedVolume = "Volume: 23";
            string output = tv.VolumeChange("UP", 10);
            Assert.AreEqual(expectedVolume, output);
        }
        [Test]
        public void MethodVolumeChangeUpWith100OrMore()
        {
            
            string expectedVolume = "Volume: 100";
            string output = tv.VolumeChange("UP", 100);
            Assert.AreEqual(expectedVolume, output);
        }
        [Test]
        public void MethodVolumeChangeDownChangeWorkCorrectly()
        {
           
            string expectedVolume = "Volume: 3";
            string output = tv.VolumeChange("DOWN", 10);
            Assert.AreEqual(expectedVolume, output);
        }
        [Test]
        public void MethodVolumeChangeDownLessThanZeroVolume()
        {
            
            string expectedVolume = "Volume: 0";
            string output = tv.VolumeChange("DOWN", 20);
            Assert.AreEqual(expectedVolume, output);
        }

        [Test]
        public void TvIsUnMuted()
        {
            tv.MuteDevice();//mute first
            bool isMuted = tv.MuteDevice();// Is not muted
            Assert.IsFalse(isMuted);
        }
        [Test]
        public void TvIsMuted()
        {
           

            bool isMuted = tv.MuteDevice();// Mute
            Assert.IsTrue(isMuted);
        }

        [Test]
        public void MethodOverrideToStrigWorkCorrectly()
        {
           
            string expected = "TV Device: soni, Screen Resolution: 100x300, Price 400$";
            string output = tv.ToString();
            Assert.AreEqual(expected, output);
        }
    }
}