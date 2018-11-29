using AnyDiff.Tests.TestObjects;
using NUnit.Framework;
using System;

namespace AnyDiff.Tests
{
    [TestFixture]
    public class ComplexObjectTests
    {
        [Test]
        public void ShouldDetect_NoDifferences_ComplexObject()
        {
            var date = new DateTime(2018, 12, 1, 5, 1, 30);
            var obj1 = new ComplexObject(100, 200, date, "Test user");
            var obj2 = new ComplexObject(100, 200, date, "Test user");
            obj1.TestEnum = TestEnum.Value1;
            obj1.AddInt(5000);
            obj1.AddInt(6000);
            obj1.AddInt(7000);

            obj2.TestEnum = TestEnum.Value1;
            obj2.AddInt(5000);
            obj2.AddInt(6000);
            obj2.AddInt(7000);

            var diff = AnyDiff.Diff(obj1, obj2);
            Assert.AreEqual(0, diff.Count);
        }

        [Test]
        public void ShouldDetect_Differences_ComplexObject()
        {
            var date = new DateTime(2018, 12, 1, 5, 1, 30);
            var obj1 = new ComplexObject(100, 200, date, "Test user");
            var obj2 = new ComplexObject(100, 200, date, "Test user");
            obj1.TestEnum = TestEnum.Value1;
            obj1.AddInt(5000);
            obj1.AddInt(6000);
            obj1.AddInt(7000);
            obj1.AnotherObject = new ComplexObject(500, 600, date, "Test");

            obj2.TestEnum = TestEnum.Value3;
            obj2.AddInt(5000);
            obj2.AddInt(6000);
            obj2.AddInt(7001);
            obj2.AnotherObject = new ComplexObject(300, 600, date, "Test2");

            var diff = AnyDiff.Diff(obj1, obj2);
            Assert.AreEqual(5, diff.Count);
        }

        [Test]
        public void ShouldDetect_NoDifferences_CircularReferences()
        {
            var obj1 = new CircularReferenceObject(100);
            var obj2 = new CircularReferenceObject(100);
            obj1.Parent = obj1;
            obj2.Parent = obj2;

            var diff = AnyDiff.Diff(obj1, obj2);
            Assert.AreEqual(0, diff.Count);
        }

        [Test]
        public void ShouldDetect_Differences_CircularReferences()
        {
            var obj1 = new CircularReferenceObject(100);
            var obj2 = new CircularReferenceObject(200);
            obj1.Parent = obj1;
            obj2.Parent = obj2;

            var diff = AnyDiff.Diff(obj1, obj2);
            Assert.AreEqual(1, diff.Count);
        }
    }
}
