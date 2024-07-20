namespace Database.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class DatabaseTests
    {       
       
        [Test]
        [TestCase(16)]
        public void CreateDatabaseCorrect(int count)
        {
           Database database = new ();
            for (int i = 0; i < count; i++) 
            {
                database.Add(i);
            }
            Assert.That(count, Is.EqualTo(database.Count));
           
        }
        [Test]
        [TestCase(16,6)]
        [TestCase(16,9)]
        [TestCase(16,125)]
        [TestCase(16,-9)]
        public void CreateDatabaseInCorrect(int count,int number)
        {
            Database database = new();
            for (int i = 0; i < count; i++)
            {
                database.Add(i);
            }
            Assert.Throws<InvalidOperationException>(() => database.Add(number));

        }
        [Test]
        [TestCase(16)]
       
        public void RemoveCorrect(int count)
        {
            Database database = new();
            for (int i = 1; i <= count; i++)
            {
                database.Add(i);
            }
            database.Remove();
            Assert.AreEqual(count-1,(database.Count));

        }
        [Test]       

        public void RemoveInCorrect()
        {
            Database database = new();
           
           // database.Remove();
            Assert.Throws<InvalidOperationException>(()=> database.Remove());

        }
        [Test]
        public void FetchMethodReturnCorrectlyTheCollection()
        {
            Database db = new (1, 2, 3);
            db.Add(4);
            db.Add(5);
            db.Remove();
            db.Remove();
            db.Remove();
            int[] fetchData = db.Fetch();
            int[] expectedData = new[] { 1, 2 };

            Assert.AreEqual(expectedData, fetchData);
        }


    }
}
