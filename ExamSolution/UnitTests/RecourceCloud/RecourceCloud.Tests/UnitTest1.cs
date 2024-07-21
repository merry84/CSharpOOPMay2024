using NUnit.Framework;
using System;
using System.Linq;
using System.Collections.Generic;

namespace RecourceCloud.Tests
{
    //92/100
    public class Tests
    {
        private DepartmentCloud departmentCloud;

        [SetUp]
        public void Setup()
        {
            departmentCloud = new DepartmentCloud();

        }

        [Test]
        public void LogTaskLenghtThrowException()
        {
            string[] arg = { "5", "koko" };
            string[] arg1 = { "5", "koko", "kalin", "tomow" };
            Assert.Throws<ArgumentException>(() => departmentCloud.LogTask(arg));
            Assert.Throws<ArgumentException>(() => departmentCloud.LogTask(arg1));
        }
        [Test]
        public void LogTaskNullThrowException()
        {
            //string[] arg = { "5", "koko" };
            string[] arg1 = { "5", null, "kalin" };
            //Assert.Throws<ArgumentException>(() => departmentCloud.LogTask(arg));
            Assert.Throws<ArgumentException>(() => departmentCloud.LogTask(arg1));
        }
        [Test]
        public void LogTaskWithValidArg()
        {

            string[] arg1 = { "5", "iliq", "kalin" };
            var actualResult = departmentCloud.LogTask(arg1);
            Assert.That(actualResult, Is.EqualTo("Task logged successfully."));
            Assert.That(departmentCloud.Tasks, Has.Count.EqualTo(1));

        }
        [Test]
        public void LogTaskWithExistArg()
        {

            string[] arg1 = { "5", "iliq", "kalin" };
            departmentCloud.LogTask(arg1);
            var result = departmentCloud.LogTask(new string[] { "6", "iliqn", "kalin" });
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.EqualTo($"kalin is already logged."));
                Assert.That(departmentCloud.Tasks, Has.Count.EqualTo(1));
            });
        }
        [Test]
        public void CreateResourceIsFalse()
        {
            var result = departmentCloud.CreateResource();
            Assert.That(result, Is.False);
        }
        [Test]
        public void CreateResourceIsTrue()
        {
            departmentCloud.LogTask(new string[] { "1", "koko", "popow" });
            var result = departmentCloud.CreateResource();
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.True);
                Assert.That(departmentCloud.Resources, Has.Count.EqualTo(1));
                var resource = departmentCloud.Resources.First();
                Assert.That(resource.Name, Is.EqualTo("popow"));
                Assert.That(resource.ResourceType, Is.EqualTo("koko"));
            });

        }
        [Test]
        public void TestResourceIsTrue()
        {
            string[] arg1 = { "5", "iliq", "kalin" };
            departmentCloud.LogTask(arg1);
            departmentCloud.CreateResource();
            var result = departmentCloud.TestResource("kalin");

            Assert.That(result, Is.Not.Null);
            Assert.That(result.IsTested, Is.True);
        }
        [Test]
        public void TestResourceIsNull()
        {
            var result= departmentCloud.TestResource("penko");
            Assert.That(result, Is.Null);
        }
    }
   
}
