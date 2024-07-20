using NUnit.Framework;
using System;

namespace Skeleton.Tests
{
    [TestFixture]
    public class AxeTests
    {
        private int attack = 10;
        private int durability = 10;
        private Axe axe;
        private Dummy dummy;
        [SetUp]
        public void SetUp()
        {
            axe = new Axe(attack, durability);
            dummy = new Dummy(10, 10);
        }      
    
        [Test]
        public void AxeLoosesDurabilityAfterAttack()
        {

            axe.Attack(dummy);
            Assert.That(axe.DurabilityPoints, Is.EqualTo(9), "Axe Durability doesn't change after attack.");
            //Assert.That(axe.DurabilityPoints == 9, "Axe Durability doesn't change after attack.");
        }
        [Test]
        public void AttackingWithABrokenWeapon()
        {
            axe = new Axe(10, 0);


            Assert.Throws<InvalidOperationException>(() =>
            {
                axe.Attack(dummy);
            }, " Weapon isn't broken");

        }

    }
}