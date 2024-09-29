using NUnit.Framework;
using System;
using System.Linq;
using System.Collections.Generic;

namespace RecourceCloud.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void ResourceTestConstructor()
        {
            Resource resource = new Resource("pipi", "lolo");
            Assert.IsNotNull(resource);
            Assert.AreEqual(resource.Name, "pipi");
            Assert.AreEqual(resource.ResourceType,"lolo");
            Assert.False(resource.IsTested);
        }

        [Test]
        public void DepartmentCloudTestConstructor()
        {
            DepartmentCloud department = new DepartmentCloud();
            Resource resource = new Resource("pipi", "lolo");
            Task task = new Task(10, "pet", "koko");
            Assert.AreEqual(department.Tasks.Count,0);
            Assert.AreEqual(department.Resources.Count,0);

        }

       

        [Test]
        public void TaskCloudTestConstructorNull()
        {
            DepartmentCloud department = new DepartmentCloud();
            Resource resource = new Resource("pipi", "lolo");
            Task task = new Task(10, null, null);
        }

        [Test]
        public void CreateResource()
        {
            DepartmentCloud department = new DepartmentCloud();
            Resource resource = new Resource("pipi", "lolo");
            Task task = new Task(10, "pet", "koko");
            Assert.IsNotNull(resource);
            Assert.IsNotNull(task);
        }

        [Test]
        public void TestResource()
        {
            Resource resource = new Resource("pipi", "lolo");
            
            Assert.IsFalse(resource.IsTested);
        }

        [Test]
        public void TestResourceIsNull()
        {
            Resource resource = new Resource(null, "lolo");

            
            Assert.AreEqual(resource.Name,null);
        }

    }
        
    
}