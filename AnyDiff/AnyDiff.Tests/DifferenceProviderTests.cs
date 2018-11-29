using AnyDiff.Tests.TestObjects;
using NUnit.Framework;
using System;
using System.Linq;

namespace AnyDiff.Tests
{
    [TestFixture]
    public class DifferenceProviderTests
    {
        [Test]
        public void ShouldDetectAdditionDelta()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { IntValue = 1 };
            var obj2 = new TestObject { IntValue = 5 };
            var diff = provider.ComputeDiff(obj1, obj2);
            Assert.AreEqual(1, diff.Count);
            Assert.AreEqual(4, diff.First().Delta);
        }

        [Test]
        public void ShouldDetectNullableAdditionDelta()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { NullableIntValue = null };
            var obj2 = new TestObject { NullableIntValue = 5 };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(1, diff.Count);
            Assert.AreEqual(5, diff.First().Delta);
        }

        [Test]
        public void ShouldDetectSubtractionDelta()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { IntValue = 7 };
            var obj2 = new TestObject { IntValue = 2 };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(1, diff.Count);
            Assert.AreEqual(-5, diff.First().Delta);
        }

        [Test]
        public void ShouldDetectIntegersAreSame()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { IntValue = 7 };
            var obj2 = new TestObject { IntValue = 7 };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(0, diff.Count);
        }

        [Test]
        public void ShouldDetectNullableSubtractionDelta()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { NullableIntValue = 7 };
            var obj2 = new TestObject { NullableIntValue = null };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(1, diff.Count);
            Assert.AreEqual(-7, diff.First().Delta);
        }

        [Test]
        public void ShouldDetectDateDelta()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { DateValue = new DateTime(2018, 05, 05, 1, 1, 1) };
            var obj2 = new TestObject { DateValue = new DateTime(2018, 05, 05, 1, 1, 1) + TimeSpan.FromMinutes(15) };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(1, diff.Count);
            Assert.AreEqual(TimeSpan.FromMinutes(15), diff.First().Delta);
        }

        [Test]
        public void ShouldDetectDatesAreSame()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { DateValue = new DateTime(2018, 05, 05, 1, 1, 1) };
            var obj2 = new TestObject { DateValue = new DateTime(2018, 05, 05, 1, 1, 1) };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(0, diff.Count);
        }

        [Test]
        public void ShouldDetectStringsAreDifferent()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { StringValue = "Once upon a time there was a little boy named Peter." };
            var obj2 = new TestObject { StringValue = "Once upon a time there was a little girl named Paula." };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(1, diff.Count);
            var deltas = ((DifferenceWords.WordDifferences)diff.First().Delta);
            Assert.AreEqual("girl", deltas.Additions.First());
            Assert.AreEqual("Paula", deltas.Additions.Skip(1).First());
            Assert.AreEqual("boy", deltas.Deletions.First());
            Assert.AreEqual("Peter", deltas.Deletions.Skip(1).First());
        }

        [Test]
        public void ShouldDetectStringsAreSame()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { StringValue = "Once upon a time there was a little boy named Peter." };
            var obj2 = new TestObject { StringValue = "Once upon a time there was a little boy named Peter." };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(0, diff.Count);
        }

        [Test]
        public void ShouldDetectGuidsAreDifferent()
        {
            var provider = new DiffProvider();

            // guids are treated as word detection
            var obj1 = new TestObject { GuidValue = new Guid("00000000-0000-0000-0000-000000000001") };
            var obj2 = new TestObject { GuidValue = new Guid("00000000-0000-0000-0000-00000000000a") };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(1, diff.Count);
            var deltas = ((DifferenceWords.WordDifferences)diff.First().Delta);
            Assert.AreEqual("00000000000a", deltas.Additions.First());
            Assert.AreEqual("000000000001", deltas.Deletions.First());
        }

        [Test]
        public void ShouldDetectGuidsAreSame()
        {
            var provider = new DiffProvider();

            // guids are treated as word detection
            var obj1 = new TestObject { GuidValue = new Guid("00000000-0000-0000-0000-000000000001") };
            var obj2 = new TestObject { GuidValue = new Guid("00000000-0000-0000-0000-000000000001") };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(0, diff.Count);
        }

        [Test]
        public void ShouldDetect_Differences_Collection()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { Collection = new int[] { 0, 0, 5, 0, 0 } };
            var obj2 = new TestObject { Collection = new int[] { 0, 0, 2, 0, 0 } };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(1, diff.Count);
            Assert.AreEqual(-3, diff.First().Delta);
            Assert.AreEqual(2, diff.First().ArrayIndex);
        }

        [Test]
        public void ShouldDetect_NoDifferences_Collection()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { IntValue = 5, Collection = new int[] { 0, 0, 5, 0, 0 } };
            var obj2 = new TestObject { IntValue = 5, Collection = new int[] { 0, 0, 5, 0, 0 } };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(0, diff.Count);
        }

        [Test]
        public void ShouldDetect_NoDifferences_Hashtable()
        {
            var provider = new DiffProvider();
            var obj1 = new System.Collections.Hashtable();
            obj1.Add(1, "Test");
            obj1.Add(2, "Test");
            var obj2 = new System.Collections.Hashtable();
            obj2.Add(1, "Test");
            obj2.Add(2, "Test");

            var diff = provider.ComputeDiff(obj1, obj2);
            Assert.AreEqual(0, diff.Count);
        }

        [Test]
        public void ShouldDetect_Differences_Hashtable()
        {
            var provider = new DiffProvider();
            var obj1 = new System.Collections.Hashtable();
            obj1.Add(1, "Test");
            obj1.Add(3, "Test");
            var obj2 = new System.Collections.Hashtable();
            obj2.Add(1, "Test");
            obj2.Add(2, "Test");

            var diff = provider.ComputeDiff(obj1, obj2, ComparisonOptions.All, "._keys", "._buckets");
            Assert.AreEqual(2, diff.Count);
        }
    }  
}
