using AnyDiff.Tests.TestObjects;
using NUnit.Framework;
using System.Linq;

namespace AnyDiff.Tests
{
    [TestFixture]
    public class IntegralDeltaTests
    {
        [Test]
        public void ShouldDetect_Byte_AdditionDelta()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { ByteValue = 1 };
            var obj2 = new TestObject { ByteValue = 5 };
            var diff = provider.ComputeDiff(obj1, obj2);
            Assert.AreEqual(1, diff.Count);
            Assert.AreEqual(4, diff.First().Delta);
        }

        [Test]
        public void ShouldDetect_Byte_SubtractionDelta()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { ByteValue = 7 };
            var obj2 = new TestObject { ByteValue = 2 };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(1, diff.Count);
            Assert.AreEqual(-5, diff.First().Delta);
        }

        [Test]
        public void ShouldDetect_Short_AdditionDelta()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { ShortValue = 1 };
            var obj2 = new TestObject { ShortValue = 5 };
            var diff = provider.ComputeDiff(obj1, obj2);
            Assert.AreEqual(1, diff.Count);
            Assert.AreEqual(4, diff.First().Delta);
        }

        [Test]
        public void ShouldDetect_Short_SubtractionDelta()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { ShortValue = 7 };
            var obj2 = new TestObject { ShortValue = 2 };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(1, diff.Count);
            Assert.AreEqual(-5, diff.First().Delta);
        }

        [Test]
        public void ShouldDetect_Int_AdditionDelta()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { IntValue = 1 };
            var obj2 = new TestObject { IntValue = 5 };
            var diff = provider.ComputeDiff(obj1, obj2);
            Assert.AreEqual(1, diff.Count);
            Assert.AreEqual(4, diff.First().Delta);
        }

        [Test]
        public void ShouldDetect_Int_SubtractionDelta()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { IntValue = 7 };
            var obj2 = new TestObject { IntValue = 2 };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(1, diff.Count);
            Assert.AreEqual(-5, diff.First().Delta);
        }

        [Test]
        public void ShouldDetect_Long_AdditionDelta()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { LongValue = 1L };
            var obj2 = new TestObject { LongValue = 5L };
            var diff = provider.ComputeDiff(obj1, obj2);
            Assert.AreEqual(1, diff.Count);
            Assert.AreEqual(4, diff.First().Delta);
        }

        [Test]
        public void ShouldDetect_Long_SubtractionDelta()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { LongValue = 7L };
            var obj2 = new TestObject { LongValue = 2L };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(1, diff.Count);
            Assert.AreEqual(-5L, diff.First().Delta);
        }

        [Test]
        public void ShouldDetect_Float_AdditionDelta()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { FloatValue = 1F };
            var obj2 = new TestObject { FloatValue = 5F };
            var diff = provider.ComputeDiff(obj1, obj2);
            Assert.AreEqual(1, diff.Count);
            Assert.AreEqual(4F, diff.First().Delta);
        }

        [Test]
        public void ShouldDetect_Float_SubtractionDelta()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { FloatValue = 7F };
            var obj2 = new TestObject { FloatValue = 2F };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(1, diff.Count);
            Assert.AreEqual(-5F, diff.First().Delta);
        }

        [Test]
        public void ShouldDetect_Double_AdditionDelta()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { DoubleValue = 1.0 };
            var obj2 = new TestObject { DoubleValue = 5.0 };
            var diff = provider.ComputeDiff(obj1, obj2);
            Assert.AreEqual(1, diff.Count);
            Assert.AreEqual(4.0, diff.First().Delta);
        }

        [Test]
        public void ShouldDetect_Double_SubtractionDelta()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { DoubleValue = 7.0 };
            var obj2 = new TestObject { DoubleValue = 2.0 };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(1, diff.Count);
            Assert.AreEqual(-5.0, diff.First().Delta);
        }

        [Test]
        public void ShouldDetect_Decimal_AdditionDelta()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { DecimalValue = 1M };
            var obj2 = new TestObject { DecimalValue = 5M };
            var diff = provider.ComputeDiff(obj1, obj2);
            Assert.AreEqual(1, diff.Count);
            Assert.AreEqual(4M, diff.First().Delta);
        }

        [Test]
        public void ShouldDetect_Decimal_SubtractionDelta()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { DecimalValue = 7M };
            var obj2 = new TestObject { DecimalValue = 2M };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(1, diff.Count);
            Assert.AreEqual(-5M, diff.First().Delta);
        }

    }
}
