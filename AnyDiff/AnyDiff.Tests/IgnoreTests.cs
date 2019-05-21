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
        public void ShouldExclude_ByPropertyList()
        {
            var object1 = new MyComplexObject(1, "A string", true);
            var object2 = new MyComplexObject(1, "A different string", true);

            var diff = object1.Diff(object2, propertyList: x => x.Name);
            Assert.AreEqual(0, diff.Count);
        }

        [Test]
        public void ShouldExclude_ByPropertyNameList()
        {
            var object1 = new MyComplexObject(1, "A string", true);
            var object2 = new MyComplexObject(1, "A different string", true);

            var diff = object1.Diff(object2, propertyList: "Name");
            Assert.AreEqual(0, diff.Count);
        }

        [Test]
        public void ShouldExclude_ByPropertyPathList()
        {
            var object1 = new MyComplexObject(1, "A string", new Location(49.282730, -123.120735));
            var object2 = new MyComplexObject(1, "A different string", new Location(40.712776, -123.120735));

            var diff = object1.Diff(object2, ".Name", ".Location.Latitude");
            Assert.AreEqual(0, diff.Count);
        }

        [Test]
        public void ShouldInclude_ByPropertyList()
        {
            var object1 = new MyComplexObject(1, "A string", new Location(49.282730, -123.120735));
            var object2 = new MyComplexObject(1, "A different string", new Location(40.712776, -123.120735));

            var diff = object1.Diff(object2, ComparisonOptions.IncludeList | ComparisonOptions.All, x => x.Id);
            Assert.AreEqual(0, diff.Count);
        }

        [Test]
        public void ShouldInclude_ByPropertyPathList()
        {
            var object1 = new MyComplexObject(1, "A string", new Location(49.282730, -123.120735));
            var object2 = new MyComplexObject(1, "A different string", new Location(40.712776, -123.120735));

            var diff = object1.Diff(object2, ComparisonOptions.IncludeList | ComparisonOptions.All, ".Id");
            Assert.AreEqual(0, diff.Count);
        }

        [Test]
        public void ShouldInclude_ByPropertyNameList()
        {
            var object1 = new MyComplexObject(1, "A string", new Location(49.282730, -123.120735));
            var object2 = new MyComplexObject(1, "A different string", new Location(40.712776, -123.120735));

            var diff = object1.Diff(object2, ComparisonOptions.IncludeList | ComparisonOptions.All, "Id");
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

            var diff = object1.Diff(object2, ComparisonOptions.All | ComparisonOptions.DisableIgnoreAttributes);
            Assert.AreEqual(1, diff.Count);
        }
    }
}
