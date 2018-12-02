using AnyDiff;
using AnyDiff.Extensions;
using AnyDiff.Tests.TestObjects;
using NUnit.Framework;

namespace AnyDiff.Tests
{
    [TestFixture]
    public class IgnoreTests
    {
        [Test]
        public void ShouldIgnore_ByPropertyList()
        {
            var object1 = new MyComplexObject(1, "A string", true);
            var object2 = new MyComplexObject(1, "A different string", true);

            var diff = object1.Diff(object2, ignoreProperties: x => x.Name);
            Assert.AreEqual(0, diff.Count);
        }

        [Test]
        public void ShouldIgnore_ByPropertyNameList()
        {
            var object1 = new MyComplexObject(1, "A string", true);
            var object2 = new MyComplexObject(1, "A different string", true);

            var diff = object1.Diff(object2, ignorePropertiesOrPaths: "Name");
            Assert.AreEqual(0, diff.Count);
        }

        [Test]
        public void ShouldIgnore_ByPropertyPathList()
        {
            var object1 = new MyComplexObject(1, "A string", new Location(49.282730, -123.120735));
            var object2 = new MyComplexObject(1, "A different string", new Location(40.712776, -123.120735));

            var diff = object1.Diff(object2, ".Name", ".Location.Latitude");
            Assert.AreEqual(0, diff.Count);
        }

        [Test]
        public void ShouldIgnore_ByAttribute()
        {
            var object1 = new MyComplexIgnoreAttributeObject(1, "A string", true);
            var object2 = new MyComplexIgnoreAttributeObject(1, "A different string", true);

            var diff = object1.Diff(object2);
            Assert.AreEqual(0, diff.Count);
        }

        [Test]
        public void ShouldDisable_IgnoreByAttribute()
        {
            var object1 = new MyComplexIgnoreAttributeObject(1, "A string", true);
            var object2 = new MyComplexIgnoreAttributeObject(1, "A different string", true);

            var diff = object1.Diff(object2, ComparisonOptions.DisableIgnoreAttributes);
            Assert.AreEqual(1, diff.Count);
        }
    }
}
