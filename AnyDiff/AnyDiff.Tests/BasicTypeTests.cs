using AnyDiff.Tests.TestObjects;
using NUnit.Framework;
using System;
using System.Linq;

namespace AnyDiff.Tests
{
    [TestFixture]
    public class BasicTypeTests
    {
        [Test]
        public void ShouldDetect_Date_Delta()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { DateValue = new DateTime(2018, 05, 05, 1, 1, 1) };
            var obj2 = new TestObject { DateValue = new DateTime(2018, 05, 05, 1, 1, 1) + TimeSpan.FromMinutes(15) };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(1, diff.Count);
            Assert.AreEqual(TimeSpan.FromMinutes(15), diff.First().Delta);
        }

        [Test]
        public void ShouldDetect_NullableDate_Delta()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { NullableDateValue = new DateTime(2018, 05, 05, 1, 1, 1) };
            var obj2 = new TestObject { NullableDateValue = new DateTime(2018, 05, 05, 1, 1, 1) + TimeSpan.FromMinutes(15) };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(1, diff.Count);
            Assert.AreEqual(TimeSpan.FromMinutes(15), diff.First().Delta);
        }

        [Test]
        public void ShouldDetect_Dates_AreSame()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { DateValue = new DateTime(2018, 05, 05, 1, 1, 1) };
            var obj2 = new TestObject { DateValue = new DateTime(2018, 05, 05, 1, 1, 1) };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(0, diff.Count);
        }

        [Test]
        public void ShouldDetect_Strings_AreDifferent()
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
        public void ShouldDetect_Strings_AreSame()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { StringValue = "Once upon a time there was a little boy named Peter." };
            var obj2 = new TestObject { StringValue = "Once upon a time there was a little boy named Peter." };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(0, diff.Count);
        }

        [Test]
        public void ShouldDetect_Guids_AreDifferent()
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
        public void ShouldDetect_NullableGuids_AreDifferent()
        {
            var provider = new DiffProvider();

            // guids are treated as word detection
            var obj1 = new TestObject { NullableGuidValue = new Guid("00000000-0000-0000-0000-000000000001") };
            var obj2 = new TestObject { NullableGuidValue = new Guid("00000000-0000-0000-0000-00000000000a") };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(1, diff.Count);
            var deltas = ((DifferenceWords.WordDifferences)diff.First().Delta);
            Assert.AreEqual("00000000000a", deltas.Additions.First());
            Assert.AreEqual("000000000001", deltas.Deletions.First());
        }

        [Test]
        public void ShouldDetect_Guids_AreSame()
        {
            var provider = new DiffProvider();

            // guids are treated as word detection
            var obj1 = new TestObject { GuidValue = new Guid("00000000-0000-0000-0000-000000000001") };
            var obj2 = new TestObject { GuidValue = new Guid("00000000-0000-0000-0000-000000000001") };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(0, diff.Count);
        }

        [Test]
        public void ShouldDetect_NullableGuids_AreSame()
        {
            var provider = new DiffProvider();

            // guids are treated as word detection
            var obj1 = new TestObject { NullableGuidValue = new Guid("00000000-0000-0000-0000-000000000001") };
            var obj2 = new TestObject { NullableGuidValue = new Guid("00000000-0000-0000-0000-000000000001") };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(0, diff.Count);
        }
    }  
}
