using AnyDiff.Extensions;
using AnyDiff.Tests.TestObjects;
using NUnit.Framework;
using System;

namespace AnyDiff.Tests
{
    [TestFixture]
    public class ExtensionTests
    {
        [Test]
        public void ShouldDiff_TwoObjects()
        {
            var obj1 = new TestObject { DateValue = new DateTime(2018, 05, 05, 1, 1, 1) };
            var obj2 = new TestObject { DateValue = new DateTime(2018, 05, 05, 1, 1, 1) + TimeSpan.FromMinutes(15) };

            var diff = obj1.Diff(obj2);
            Assert.AreEqual(1, diff.Count);
        }

        [Test]
        public void ShouldDiff_TwoObjects_WithOptions()
        {
            var obj1 = new TestObject { DateValue = new DateTime(2018, 05, 05, 1, 1, 1) };
            var obj2 = new TestObject { DateValue = new DateTime(2018, 05, 05, 1, 1, 1) + TimeSpan.FromMinutes(15) };

            var diff = obj1.Diff(obj2, ComparisonOptions.All);
            Assert.AreEqual(1, diff.Count);
        }

        [Test]
        public void ShouldDiff_TwoObjects_WithIgnorePaths()
        {
            var obj1 = new TestObject { DateValue = new DateTime(2018, 05, 05, 1, 1, 1), BoolValue = true };
            var obj2 = new TestObject { DateValue = new DateTime(2018, 05, 05, 1, 1, 1) + TimeSpan.FromMinutes(15), BoolValue = false };

            var diff = obj1.Diff(obj2, ".BoolValue");
            Assert.AreEqual(1, diff.Count);
        }

        [Test]
        public void ShouldDiff_TwoObjects_WithOptionsIgnorePaths()
        {
            var obj1 = new TestObject { DateValue = new DateTime(2018, 05, 05, 1, 1, 1), BoolValue = true };
            var obj2 = new TestObject { DateValue = new DateTime(2018, 05, 05, 1, 1, 1) + TimeSpan.FromMinutes(15), BoolValue = false };

            var diff = obj1.Diff(obj2, ComparisonOptions.All, ".BoolValue");
            Assert.AreEqual(1, diff.Count);
        }

        [Test]
        public void ShouldDiff_TwoObjects_WithMaxDepthOptionsIgnorePaths()
        {
            var obj1 = new TestObject { DateValue = new DateTime(2018, 05, 05, 1, 1, 1), BoolValue = true };
            var obj2 = new TestObject { DateValue = new DateTime(2018, 05, 05, 1, 1, 1) + TimeSpan.FromMinutes(15), BoolValue = false };

            var diff = obj1.Diff(obj2, 4, ComparisonOptions.All, ".BoolValue");
            Assert.AreEqual(1, diff.Count);
        }

        [Test]
        public void ShouldDiff_TwoObjects_WithIgnoreExpression()
        {
            var obj1 = new TestObject { DateValue = new DateTime(2018, 05, 05, 1, 1, 1), BoolValue = true };
            var obj2 = new TestObject { DateValue = new DateTime(2018, 05, 05, 1, 1, 1) + TimeSpan.FromMinutes(15), BoolValue = false };

            var diff = obj1.Diff(obj2, x => x.BoolValue);
            Assert.AreEqual(1, diff.Count);
        }

        [Test]
        public void ShouldDiff_TwoObjects_WithOptionsIgnoreExpression()
        {
            var obj1 = new TestObject { DateValue = new DateTime(2018, 05, 05, 1, 1, 1), BoolValue = true };
            var obj2 = new TestObject { DateValue = new DateTime(2018, 05, 05, 1, 1, 1) + TimeSpan.FromMinutes(15), BoolValue = false };

            var diff = obj1.Diff(obj2, ComparisonOptions.All, x => x.BoolValue);
            Assert.AreEqual(1, diff.Count);
        }

        [Test]
        public void ShouldDiff_TwoObjects_WithMaxDepthOptionsIgnoreExpression()
        {
            var obj1 = new TestObject { DateValue = new DateTime(2018, 05, 05, 1, 1, 1), BoolValue = true };
            var obj2 = new TestObject { DateValue = new DateTime(2018, 05, 05, 1, 1, 1) + TimeSpan.FromMinutes(15), BoolValue = false };

            var diff = obj1.Diff(obj2, 4, ComparisonOptions.All, x => x.BoolValue);
            Assert.AreEqual(1, diff.Count);
        }
    }
}
