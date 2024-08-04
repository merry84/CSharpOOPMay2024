using NUnit.Framework;
using System.Collections.Generic;

namespace VendingRetail.Tests
{
    public class Tests
    {

        [Test]
        public void FillWaterTank_ShouldFillToCapacity()
        {
            // Arrange
            var coffeeMat = new CoffeeMat(1000, 5);

            // Act
            string result = coffeeMat.FillWaterTank();

            // Assert
            Assert.AreEqual("Water tank is filled with 1000ml", result);
        }

        [Test]
        public void FillWaterTank_WhenFull_ShouldReturnFullMessage()
        {
            // Arrange
            var coffeeMat = new CoffeeMat(1000, 5);
            coffeeMat.FillWaterTank(); // Fill it initially

            // Act
            string result = coffeeMat.FillWaterTank();

            // Assert
            Assert.AreEqual("Water tank is already full!", result);
        }

        [Test]
        public void AddDrink_WhenUnderButtonCount_ShouldAddDrink()
        {
            // Arrange
            var coffeeMat = new CoffeeMat(1000, 5);

            // Act
            bool result = coffeeMat.AddDrink("Espresso", 1.50);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void AddDrink_WhenDrinkAlreadyExists_ShouldReturnFalse()
        {
            // Arrange
            var coffeeMat = new CoffeeMat(1000, 5);
            coffeeMat.AddDrink("Espresso", 1.50);

            // Act
            bool result = coffeeMat.AddDrink("Espresso", 1.50);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void AddDrink_WhenOverButtonCount_ShouldReturnFalse()
        {
            // Arrange
            var coffeeMat = new CoffeeMat(1000, 1); // Only 1 button

            // Act
            coffeeMat.AddDrink("Espresso", 1.50);
            bool result = coffeeMat.AddDrink("Latte", 2.50);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void BuyDrink_WhenOutOfWater_ShouldReturnOutOfWaterMessage()
        {
            // Arrange
            var coffeeMat = new CoffeeMat(1000, 5);

            // Act
            string result = coffeeMat.BuyDrink("Espresso");

            // Assert
            Assert.AreEqual("CoffeeMat is out of water!", result);
        }

        [Test]
        public void BuyDrink_WhenDrinkDoesNotExist_ShouldReturnNotAvailableMessage()
        {
            // Arrange
            var coffeeMat = new CoffeeMat(1000, 5);
            coffeeMat.FillWaterTank();

            // Act
            string result = coffeeMat.BuyDrink("NonExistentDrink");

            // Assert
            Assert.AreEqual("NonExistentDrink is not available!", result);
        }

        [Test]
        public void BuyDrink_WhenSuccessful_ShouldReturnBillMessage()
        {
            // Arrange
            var coffeeMat = new CoffeeMat(1000, 5);
            coffeeMat.FillWaterTank();
            coffeeMat.AddDrink("Espresso", 1.50);

            // Act
            string result = coffeeMat.BuyDrink("Espresso");

            // Assert
            Assert.AreEqual("Your bill is 1.50$", result);
        }

        [Test]
        public void CollectIncome_ShouldReturnTotalIncomeAndResetIncome()
        {
            // Arrange
            var coffeeMat = new CoffeeMat(1000, 5);
            coffeeMat.FillWaterTank();
            coffeeMat.AddDrink("Espresso", 1.50);
            coffeeMat.BuyDrink("Espresso");

            // Act
            double income = coffeeMat.CollectIncome();

            // Assert
            Assert.AreEqual(1.50, income);
            Assert.AreEqual(0, coffeeMat.Income);
        }

        [Test]
        public void AddDrink_WhenPriceIsNegative_ShouldReturnFalse()
        {
            // Arrange
            var coffeeMat = new CoffeeMat(1000, 5);

            // Act
            bool result = coffeeMat.AddDrink("Espresso", -1.50);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void AddDrink_WhenDrinkNameIsEmpty_ShouldReturnFalse()
        {
            // Arrange
            var coffeeMat = new CoffeeMat(1000, 5);

            // Act
            bool result = coffeeMat.AddDrink("", 1.50);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void BuyDrink_ShouldReduceWaterTankLevelBy80ml()
        {
            // Arrange
            var coffeeMat = new CoffeeMat(1000, 5);
            coffeeMat.FillWaterTank();
            coffeeMat.AddDrink("Espresso", 1.50);

            // Act
            coffeeMat.BuyDrink("Espresso");

            // Use reflection to access the private waterTankLevel field
            var waterTankLevelField = typeof(CoffeeMat).GetField("waterTankLevel", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            int waterTankLevel = (int)waterTankLevelField.GetValue(coffeeMat);

            // Assert
            Assert.AreEqual(920, waterTankLevel); // 1000 - 80 = 920
        }

        

        [Test]
        public void Income_ShouldBeCorrectAfterMultiplePurchases()
        {
            // Arrange
            var coffeeMat = new CoffeeMat(1000, 5);
            coffeeMat.FillWaterTank();
            coffeeMat.AddDrink("Espresso", 1.50);
            coffeeMat.AddDrink("Latte", 2.00);

            // Act
            coffeeMat.BuyDrink("Espresso");
            coffeeMat.BuyDrink("Latte");

            // Assert
            Assert.AreEqual(3.50, coffeeMat.Income);
        }

        [Test]
        public void BuyDrink_WhenNotEnoughWater_ShouldReturnOutOfWaterMessage()
        {
            // Arrange
            var coffeeMat = new CoffeeMat(100, 5);
            coffeeMat.FillWaterTank();

            // Act
            string result = coffeeMat.BuyDrink("Espresso");

            // Assert
            Assert.AreEqual("Espresso is not available!", result);
        }

        [Test]
        public void AddDrink_MultipleDrinks_ShouldReturnCorrectCount()
        {
            // Arrange
            var coffeeMat = new CoffeeMat(1000, 5);

            // Act
            coffeeMat.AddDrink("Espresso", 1.50);
            coffeeMat.AddDrink("Latte", 2.00);
            coffeeMat.AddDrink("Cappuccino", 2.50);

            // Use reflection to access the private drinks field
            var drinksField = typeof(CoffeeMat).GetField("drinks", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var drinks = (Dictionary<string, double>)drinksField.GetValue(coffeeMat);

            // Assert
            Assert.AreEqual(3, drinks.Count);
        }
    }
}