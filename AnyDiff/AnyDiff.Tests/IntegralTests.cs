using AnyDiff.Tests.TestObjects;
using NUnit.Framework;

namespace AnyDiff.Tests
{
    [TestFixture]
    public class IntegralTests
    {
        [Test]
        public void ShouldDetect_Bools_AreSame()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { BoolValue = true };
            var obj2 = new TestObject { BoolValue = true };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(0, diff.Count);
        }

        [Test]
        public void ShouldDetect_Bools_AreNotSame()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { BoolValue = true };
            var obj2 = new TestObject { BoolValue = false };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(1, diff.Count);
        }

        [Test]
        public void ShouldDetect_Bytes_AreSame()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { ByteValue = 7 };
            var obj2 = new TestObject { ByteValue = 7 };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(0, diff.Count);
        }

        [Test]
        public void ShouldDetect_Bytes_AreNotSame()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { ByteValue = 7 };
            var obj2 = new TestObject { ByteValue = 4 };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(1, diff.Count);
        }

        [Test]
        public void ShouldDetect_SBytes_AreSame()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { SByteValue = 7 };
            var obj2 = new TestObject { SByteValue = 7 };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(0, diff.Count);
        }

        [Test]
        public void ShouldDetect_SBytes_AreNotSame()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { SByteValue = 7 };
            var obj2 = new TestObject { SByteValue = 4 };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(1, diff.Count);
        }

        [Test]
        public void ShouldDetect_Shorts_AreSame()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { ShortValue = 7 };
            var obj2 = new TestObject { ShortValue = 7 };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(0, diff.Count);
        }

        [Test]
        public void ShouldDetect_Shorts_AreNotSame()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { ShortValue = 7 };
            var obj2 = new TestObject { ShortValue = 4 };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(1, diff.Count);
        }

        [Test]
        public void ShouldDetect_UShorts_AreSame()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { UShortValue = 7 };
            var obj2 = new TestObject { UShortValue = 7 };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(0, diff.Count);
        }

        [Test]
        public void ShouldDetect_UShorts_AreNotSame()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { UShortValue = 7 };
            var obj2 = new TestObject { UShortValue = 4 };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(1, diff.Count);
        }

        [Test]
        public void ShouldDetect_Integers_AreSame()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { IntValue = 7 };
            var obj2 = new TestObject { IntValue = 7 };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(0, diff.Count);
        }

        [Test]
        public void ShouldDetect_Integers_AreNotSame()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { IntValue = 7 };
            var obj2 = new TestObject { IntValue = 4 };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(1, diff.Count);
        }

        [Test]
        public void ShouldDetect_UIntegers_AreSame()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { UIntValue = 7 };
            var obj2 = new TestObject { UIntValue = 7 };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(0, diff.Count);
        }

        [Test]
        public void ShouldDetect_UIntegers_AreNotSame()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { UIntValue = 7 };
            var obj2 = new TestObject { UIntValue = 4 };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(1, diff.Count);
        }

        [Test]
        public void ShouldDetect_Longs_AreSame()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { LongValue = 7 };
            var obj2 = new TestObject { LongValue = 7 };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(0, diff.Count);
        }

        [Test]
        public void ShouldDetect_Longs_AreNotSame()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { LongValue = 7 };
            var obj2 = new TestObject { LongValue = 4 };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(1, diff.Count);
        }

        [Test]
        public void ShouldDetect_ULongs_AreSame()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { ULongValue = 7 };
            var obj2 = new TestObject { ULongValue = 7 };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(0, diff.Count);
        }

        [Test]
        public void ShouldDetect_ULongs_AreNotSame()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { ULongValue = 7 };
            var obj2 = new TestObject { ULongValue = 4 };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(1, diff.Count);
        }

        [Test]
        public void ShouldDetect_Floats_AreSame()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { FloatValue = 7.0F };
            var obj2 = new TestObject { FloatValue = 7.0F };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(0, diff.Count);
        }

        [Test]
        public void ShouldDetect_Floats_AreNotSame()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { FloatValue = 7.0F };
            var obj2 = new TestObject { FloatValue = 4.0F };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(1, diff.Count);
        }

        [Test]
        public void ShouldDetect_Doubles_AreSame()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { DoubleValue = 7.0 };
            var obj2 = new TestObject { DoubleValue = 7.0 };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(0, diff.Count);
        }

        [Test]
        public void ShouldDetect_Doubles_AreNotSame()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { DoubleValue = 7.0 };
            var obj2 = new TestObject { DoubleValue = 4.0 };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(1, diff.Count);
        }

        [Test]
        public void ShouldDetect_Decimals_AreSame()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { DecimalValue = 7.0M };
            var obj2 = new TestObject { DecimalValue = 7.0M };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(0, diff.Count);
        }

        [Test]
        public void ShouldDetect_Decimals_AreNotSame()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { DecimalValue = 7.0M };
            var obj2 = new TestObject { DecimalValue = 4.0M };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(1, diff.Count);
        }
    }
}
