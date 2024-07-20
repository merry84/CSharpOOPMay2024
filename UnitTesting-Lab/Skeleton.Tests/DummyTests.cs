using NUnit.Framework;
using System;

namespace Skeleton.Tests
{
    [TestFixture]
    public class DummyTests
    {
        private int attack = 3;
        private int durability = 2;
        private int health = 5;
        private int experiance = 2;
        private Axe axe;
        private Dummy dummy;

        [SetUp]
        public void SetUp()
        {
            axe = new Axe(attack, durability);
            dummy = new Dummy(health, experiance);
        }
        [Test]
        public void TestAttack()
        {
            var healthBefore = dummy.Health;
            dummy.TakeAttack(axe.AttackPoints);
            var healthAfter = dummy.Health;
            Assert.That(healthAfter, Is.LessThan(healthBefore));
        }
        [Test]
        public void DummyIsDead()
        {
            
            dummy.TakeAttack(axe.AttackPoints);
            dummy.TakeAttack(axe.AttackPoints);
            Assert.That(dummy.IsDead, Is.True);
        }
        [Test]
        public void GiveExperienceThrow()
        {
            
            dummy.TakeAttack(axe.AttackPoints);
            Assert.Throws<InvalidOperationException>(() =>
           dummy.GiveExperience());
            Assert.That(dummy.IsDead, Is.False);
        }
        [Test]
        public void GiveExperience()
        {
            dummy.TakeAttack(axe.AttackPoints);
            dummy.TakeAttack(axe.AttackPoints);
            Assert.AreEqual(dummy.GiveExperience() ,2);
            Assert.That(dummy.IsDead, Is.True);
        }

        [Test]
        public void DummyIsDeadThrowException()
        {
            
            dummy.TakeAttack(axe.AttackPoints);
            dummy.TakeAttack(axe.AttackPoints);
            Assert.That(dummy.IsDead,Is.True);
            
            Assert.Throws<InvalidOperationException>(()=>
            dummy.TakeAttack(axe.AttackPoints));
        }

    }
}