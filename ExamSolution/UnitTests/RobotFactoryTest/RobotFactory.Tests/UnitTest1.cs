using System.Linq;
using NUnit.Framework;

namespace RobotFactory.Tests
{
    public class Tests
    {
        private Factory factory;
        [SetUp]
        public void Setup()
        {
            factory = new Factory("Kalina", 3);
        }

        [Test]
        public void ConstructorWorkCorrectly()
        {
            Assert.IsNotNull(factory);
            Assert.AreEqual(factory.Capacity,3);
            Assert.AreEqual(factory.Name,"Kalina");
            Assert.AreEqual(factory.Robots.Count,0);
            Assert.AreEqual(factory.Supplements.Count,0);
        }

        [Test]
        public void ProduceRobot()
        {
            //Robot robot = new Robot("petko", 1200, 450);
            Assert.AreEqual(factory.Robots.Count,0);
            factory.ProduceRobot("petko", 1200, 450);
            Assert.AreEqual(factory.Robots.Count,1);
        }
        [Test]
        public void ProduceRobotAddCheck()
        {
            //Robot robot = new Robot("petko", 1200, 450);
           
            factory.ProduceRobot("petko", 1200, 450);
            factory.ProduceRobot("milko", 1250, 150);
            factory.ProduceRobot("rada", 1300, 250);
            Assert.AreEqual(factory.ProduceRobot("kiko",2500,962), "The factory is unable to produce more robots for this production day!");
            Assert.AreEqual(factory.Robots.Count,3);
        }
        [Test]
        public void ProduceRobotAddValid()
        {
            //Robot robot = new Robot("petko", 1200, 450);
            Assert.AreEqual(factory.ProduceRobot("kiko", 2500, 962), "Produced --> Robot model: kiko IS: 962, Price: 2500.00");
            Assert.AreEqual(factory.Robots.Count,1);
        }

        [Test]
        public void ProduceSupplement()
        {
            Assert.AreEqual(factory.Supplements.Count, 0);
            var actualResult =factory.ProduceSupplement("lili", 5625);
            Assert.AreEqual("Supplement: lili IS: 5625",actualResult);
            Assert.AreEqual(factory.Supplements.Count,1);
        }

        [Test]
        public void UpgradeRobotCorrectly()
        {
            factory.ProduceRobot("koko", 450,852);
            factory.ProduceSupplement("perko", 852);

            var actualResult =
                factory.UpgradeRobot(factory.Robots.FirstOrDefault(), factory.Supplements.FirstOrDefault());
            Assert.IsTrue(actualResult);
        }

        [Test]
        public void UpgradeRobotIncorrectInterfaceStandard()
        {
            factory.ProduceRobot("koko", 450, 852);
            factory.ProduceSupplement("perko", 853);

            var actualResult =
                factory.UpgradeRobot(factory.Robots.FirstOrDefault(), factory.Supplements.FirstOrDefault());
            Assert.IsFalse(actualResult);
        }
        [Test]
        public void UpgradeRobotAlreadyUpgrade()
        {
            factory.ProduceRobot("koko", 450, 852);
            factory.ProduceSupplement("perko", 852);
            factory.UpgradeRobot(factory.Robots.FirstOrDefault(), factory.Supplements.FirstOrDefault());
            var actualResult =
                factory.UpgradeRobot(factory.Robots.FirstOrDefault(), factory.Supplements.FirstOrDefault());
            Assert.IsFalse(actualResult);
        }

        [Test]
        public void SellRobotCorrectly()
        {
            factory.ProduceRobot("Robo-3", 2000, 22);
            factory.ProduceRobot("Robo-3", 2500, 22);
            factory.ProduceRobot("Robo-3", 30000, 22);

            Robot robot = factory.Robots.FirstOrDefault(r => r.Price == 2000);

            var robotSold = factory.SellRobot(2499);

            Assert.AreSame(robot, robotSold);

        }
    }
}