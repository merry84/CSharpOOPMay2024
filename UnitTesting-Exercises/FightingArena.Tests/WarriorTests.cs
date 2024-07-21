namespace FightingArena.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class WarriorTests
    {
        [Test]
        [TestCase("milko", 15, 40)]
        public void ConstructorWorkCorrectly(string name, int damage, int hp)
        {
            Warrior solder = new Warrior(name, damage, hp);
            Assert.AreEqual(name, solder.Name);
            Assert.AreEqual(damage, solder.Damage);
            Assert.AreEqual(hp, solder.HP);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("  ")]
        public void ConstructorThrowExceptionIfNameIsNullOrWhitespace(string name)
        {
            Assert.Throws<ArgumentException>(() => new Warrior(name, 45, 36));
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-45)]

        public void ConstructorThrowExceptionIfDamageIsZeroOrNegativeNumber(int damage)
        {
            Assert.Throws<ArgumentException>(() => new Warrior("Kalin", damage, 36));
        }

        [Test]
        [TestCase(-1)]
        [TestCase(-45)]

        public void ConstructorThrowExceptionIfHPIsNegativeNumber(int hp)
        {
            Assert.Throws<ArgumentException>(() => new Warrior("Kalin", 25, hp));
        }

        [Test]
        public void MethodAttackWorkCorrectly()
        {
            Warrior solder = new Warrior("mitko", 10, 100);
            Warrior enemySolder = new Warrior("petko", 5, 60);
            int expettedSolderHp = 95;
            int expectedEnemyHp = 50;
            solder.Attack(enemySolder);
            Assert.AreEqual(expettedSolderHp, solder.HP);
            Assert.AreEqual(expectedEnemyHp, expectedEnemyHp);
        }

        [Test]
        [TestCase(0)]
        [TestCase(29)]
        [TestCase(30)]
        public void MethodAttackSolderWithHPIs30OrBelow30(int hp)
        {
            Warrior solder = new Warrior("mitko", 10, hp);
            Warrior enemySolder = new Warrior("petko", 5, 60);
            Assert.Throws<InvalidOperationException>(() => solder.Attack(enemySolder));
        }

        [Test]
        [TestCase(0)]
        [TestCase(29)]
        [TestCase(30)]
        public void MethodAttackEnemyWithHPIs30OrBelow30(int hp)
        {
            Warrior solder = new Warrior("mitko", 10, 52);
            Warrior enemySolder = new Warrior("petko", 5, hp);
            Assert.Throws<InvalidOperationException>(() => solder.Attack(enemySolder));
        }
        [Test]
        [TestCase(600)]
        [TestCase(50)]
        [TestCase(31)]
        public void MethodAttackSolderDamageIsBiggerThanEnemyHp(int damage)
        {
            Warrior solder = new Warrior("mitko", damage, 52);
            Warrior enemySolder = new Warrior("petko", 5, 30);
            Assert.Throws<InvalidOperationException>(() => solder.Attack(enemySolder));
        }
        [Test]
        [TestCase(600)]
        [TestCase(50)]
        [TestCase(31)]
        public void MethodAttackEnemyDamageIsBiggerThanSolderHp(int damage)
        {
            Warrior solder = new Warrior("mitko", 15, 52);
            Warrior enemySolder = new Warrior("petko", damage, 30);
            Assert.Throws<InvalidOperationException>(() => solder.Attack(enemySolder));
        }

        [Test]
        public void MethodAttackSolderDamageIsBiggerThanEnemyDamage()
        {
            // if (this.Damage > warrior.HP)
            // {
            //     warrior.HP = 0;
            // }
            Warrior solder = new Warrior("mitko", 50, 100);
            Warrior enemySolder = new Warrior("petko", 45, 40);
            int expettedSolderHp = 55;
            int expectedEnemyHp = 0;
            solder.Attack(enemySolder);
            Assert.AreEqual(expettedSolderHp, solder.HP);
            Assert.AreEqual(expectedEnemyHp, expectedEnemyHp);
        }
    }
}