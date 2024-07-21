namespace FightingArena.Tests
{
    using NUnit.Framework;
    using System.Linq;
    using System;

    [TestFixture]
    public class ArenaTests
    {
        private Arena arena;

        [SetUp]
        public void SetUp()
        {
            arena = new Arena();
        }

        [Test]
        public void ArenaWorkCorrectly()
        {
            Assert.IsNotNull(arena);
            Assert.IsNotNull(arena.Warriors);
        }

        [Test]
        public void ArenaWorkCorrectlyCount()
        {
            Warrior solder = new Warrior("lili", 35, 45);
            arena.Enroll(solder);
            Assert.IsNotNull(arena.Warriors);
            Assert.AreEqual(1, arena.Count);
        }

        [Test]
        public void ArenaEnrollShouldWorkCorrectly()
        {

            Warrior warrior = new("Gosho", 5, 100);
            arena.Enroll(warrior);
            Assert.IsNotEmpty(arena.Warriors);
            Assert.AreEqual(warrior, arena.Warriors.Single());
        }

        [Test]
        public void MethodEnrolThrowExceptionIsAlreadyEnrolled()
        {
            Warrior solder = new Warrior("lili", 35, 45);
            arena.Enroll(solder);
            Assert.Throws<InvalidOperationException>(() => arena.Enroll(solder));
        }

        [Test]
        [TestCase("Gosho", "ilko")]
        public void FightMethodWorkCorrectly(string name, string enemyName)
        {
            Warrior solder = new Warrior(name, damage: 15, hp: 100);
            Warrior enemy = new Warrior(enemyName, damage: 10, hp: 50);
            arena.Enroll(solder);
            arena.Enroll(enemy);
            arena.Fight(name, enemyName);
            Assert.AreEqual(90, solder.HP);
            Assert.AreEqual(35, enemy.HP);
        }
        [Test]
        public void ArenaFightShouldThrowExceptionIfAttackerNotFound()
        {
            Warrior attacker = new("Gosho", 15, 100);
            Warrior defender = new("Pesho", 5, 50);
            arena.Enroll(defender);
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() =>
                arena.Fight(attacker.Name, defender.Name));
            Assert.AreEqual($"There is no fighter with name {attacker.Name} enrolled for the fights!", exception.Message);
        }

        [Test]

        public void FightMethodThrowExceptionIfEnemyNameIsNotFound()
        {
            Warrior solder = new Warrior("jojo", damage: 15, hp: 100);
            Warrior enemy = new Warrior("bobo", damage: 5, hp: 50);

            arena.Enroll(solder);
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() =>

                arena.Fight(solder.Name, enemy.Name));
            Assert.AreEqual($"There is no fighter with name {enemy.Name} enrolled for the fights!", ex.Message);
        }
    }
}
