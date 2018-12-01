using AnyDiff.Tests.TestObjects;
using NUnit.Framework;
using System.Linq;

namespace AnyDiff.Tests
{
    [TestFixture]
    public class NullableIntegralTests
    {
        [Test]
        public void ShouldDetect_NullableBools_AreSame()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { NullableBoolValue = true };
            var obj2 = new TestObject { NullableBoolValue = true };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(0, diff.Count);
        }

        [Test]
        public void ShouldDetect_NullableBools_AreNotSame()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { NullableBoolValue = true };
            var obj2 = new TestObject { NullableBoolValue = false };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(1, diff.Count);
        }

        [Test]
        public void ShouldDetect_NullableBytes_AreSame()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { NullableByteValue = 7 };
            var obj2 = new TestObject { NullableByteValue = 7 };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(0, diff.Count);
        }

        [Test]
        public void ShouldDetect_NullableBytes_AreNotSame()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { NullableByteValue = 7 };
            var obj2 = new TestObject { NullableByteValue = 4 };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(1, diff.Count);
        }

        [Test]
        public void ShouldDetect_NullableShorts_AreSame()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { NullableShortValue = 7 };
            var obj2 = new TestObject { NullableShortValue = 7 };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(0, diff.Count);
        }

        [Test]
        public void ShouldDetect_NullableShorts_AreNotSame()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { NullableShortValue = 7 };
            var obj2 = new TestObject { NullableShortValue = 4 };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(1, diff.Count);
        }

        [Test]
        public void ShouldDetect_NullableIntegers_AreSame()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { NullableIntValue = 7 };
            var obj2 = new TestObject { NullableIntValue = 7 };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(0, diff.Count);
        }

        [Test]
        public void ShouldDetect_NullableIntegers_AreNotSame()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { NullableIntValue = 7 };
            var obj2 = new TestObject { NullableIntValue = 4 };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(1, diff.Count);
        }

        [Test]
        public void ShouldDetect_NullableLongs_AreSame()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { NullableLongValue = 7 };
            var obj2 = new TestObject { NullableLongValue = 7 };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(0, diff.Count);
        }

        [Test]
        public void ShouldDetect_NullableLongs_AreNotSame()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { NullableLongValue = 7 };
            var obj2 = new TestObject { NullableLongValue = 4 };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(1, diff.Count);
        }

        [Test]
        public void ShouldDetect_NullableFloats_AreSame()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { NullableFloatValue = 7.0F };
            var obj2 = new TestObject { NullableFloatValue = 7.0F };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(0, diff.Count);
        }

        [Test]
        public void ShouldDetect_NullableFloats_AreNotSame()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { NullableFloatValue = 7.0F };
            var obj2 = new TestObject { NullableFloatValue = 4.0F };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(1, diff.Count);
        }

        [Test]
        public void ShouldDetect_NullableDoubles_AreSame()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { NullableDoubleValue = 7.0 };
            var obj2 = new TestObject { NullableDoubleValue = 7.0 };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(0, diff.Count);
        }

        [Test]
        public void ShouldDetect_NullableDoubles_AreNotSame()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { NullableDoubleValue = 7.0 };
            var obj2 = new TestObject { NullableDoubleValue = 4.0 };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(1, diff.Count);
        }

        [Test]
        public void ShouldDetect_NullableDecimals_AreSame()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { NullableDecimalValue = 7.0M };
            var obj2 = new TestObject { NullableDecimalValue = 7.0M };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(0, diff.Count);
        }

        [Test]
        public void ShouldDetect_NullableDecimals_AreNotSame()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { NullableDecimalValue = 7.0M };
            var obj2 = new TestObject { NullableDecimalValue = 4.0M };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(1, diff.Count);
        }
    }
}
