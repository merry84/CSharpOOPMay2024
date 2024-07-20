namespace DatabaseExtended.Tests
{
    using ExtendedDatabase;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class ExtendedDatabaseTests
    {
        private Database database;
        private Person persons;
        [SetUp]
        public void SetUp() 
        {
            var persons = new Person[]
             {
                new Person  (1,"zoko"),
                new Person  (2,"coko"),
                new Person  (3,"goko"),
                new Person  (4,"roko"),
                new Person  (5,"poko"),
                new Person  (6,"moko"),
                new Person  (7,"kiko"),
             };
            database = new Database (persons);
        }
        [Test]
        public void ConstructorWorkCorrectly()
        {
            var expectResult = 7;
            Assert.AreEqual(expectResult, database.Count);
        }
        [Test]
        public void AddMethodWorkCorrectly()
        {
            var expectResult = 8;
            database.Add(new Person(8, "momo"));
            Assert.AreEqual(expectResult, database.Count);
          
        }
        [Test]
        public void AddMethodThrowCapacity()
        {
           
            database.Add(new Person(8, "momo"));
            database.Add(new Person(9, "Asen"));
            database.Add(new Person(10, "Jivko"));
            database.Add(new Person(11, "Toshko"));
            database.Add(new Person(12, "John"));
            database.Add(new Person(13, "Paul"));
            database.Add(new Person(14, "Green"));
            database.Add(new Person(15, "Brown"));
            database.Add(new Person(16, "Binko"));

            Assert.Throws<InvalidOperationException>(() => database.Add(new Person(17, "lili")));

        }
        [Test]
        public void AddMethodThrowExistId()
        {
            Assert.Throws<InvalidOperationException>(() => database.Add(new Person(5, "malin")));
        }
        [Test]
        public void AddMethodThrowExistUsername()
        {
            Assert.Throws<InvalidOperationException>(() => database.Add(new Person(10, "kiko")));
        }
        [Test]
        public void RemoveMethodWorkCorrectly()
        {
            database.Remove();
            database.Remove();
            database.Remove();
            database.Remove();
            Assert.AreEqual(3, database.Count);
            Assert.That(database.Count, Is.Not.Null);
        }
        [Test]
        public void RemoveMethodWorkThrow()
        {
            database.Remove();
            database.Remove();
            database.Remove();
            database.Remove();
            database.Remove();
            database.Remove();
            database.Remove();
          
            Assert.Throws<InvalidOperationException>(()=> database.Remove());
            //Assert.That(database.Count == null, Is.False);
        }
        [Test]
        public void DatabaseRemoveMethodShouldThrowExceptionIfDatabaseIsEmpty()
        {
            var database = new Database();

            Assert.Throws<InvalidOperationException>(() => database.Remove());
        }
        public void DatabaseFindByUsernameMethodShouldBeCaseSensitive()
        {
            var notExpectedResult = "peShO";
            var actualResult = this.database.FindByUsername("Pesho").UserName;

            Assert.That(actualResult, Is.Not.EqualTo(notExpectedResult));
        }

        [Test]
        public void FindByUsernameCorrectly()
        {
            var result = "kiko";
            var actualResult = database.FindByUsername(result).UserName;
            Assert.AreEqual(result, actualResult);
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void FindByUsernameThrowUsernameIsNullOrEmpty(string username)
        {
            Assert.Throws<ArgumentNullException>(()=> this.database.FindByUsername(username));
            
        }

        [Test]
        [TestCase(1)]
        //[TestCase(null)]
        public void FindByIdCorrectly(int id)
        {
            var expectedResult = "zoko";
           var result= this.database.FindById(id).UserName;
            Assert.AreEqual(expectedResult, result);


        }
        [Test]
        [TestCase(-1)]
        //[TestCase(null)]
        public void FindByIdWithNegativeNumber(int id)
        {
           Assert.Throws<ArgumentOutOfRangeException>(()=> this.database.FindById(id));
        }
        [Test]
        [TestCase(45)]
        //[TestCase(null)]
        public void FindByIdWithNonExtistId(int id)
        {
            Assert.Throws<InvalidOperationException>(() => this.database.FindById(id));


        }
    }
}