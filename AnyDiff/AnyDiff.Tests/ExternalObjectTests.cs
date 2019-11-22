using NUnit.Framework;
using System.Drawing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AnyDiff.Tests
{
    [TestFixture]
    public class ExternalObjectTests
    {
        [Test]
        public void ShouldDetect_NoDifferences_Point()
        {
            var obj1 = new Point(100, 200);
            var obj2 = new Point(100, 200);

            var diff = AnyDiff.Diff(obj1, obj2, ComparisonOptions.CompareProperties);
            Assert.AreEqual(0, diff.Count);
        }

        [Test]
        public void ShouldDetect_Differences_Point()
        {
            var obj1 = new Point(100, 200);
            var obj2 = new Point(101, 200);

            var diff = AnyDiff.Diff(obj1, obj2, ComparisonOptions.CompareProperties);
            Assert.AreEqual(1, diff.Count);
        }

        [Test]
        public void ShouldDetect_NoDifferences_TestFixtureData()
        {
            var obj1 = new TestFixtureData(new { Field1 = "Test" });
            var obj2 = new TestFixtureData(new { Field1 = "Test" });

            var diff = AnyDiff.Diff(obj1, obj2);
            Assert.AreEqual(0, diff.Count);
        }

        [Test]
        public void ShouldDetect_Differences_TestFixtureData()
        {
            var obj1 = new TestFixtureData(new { Field1 = "Test" });
            var obj2 = new TestFixtureData(new { Field1 = "Test 2" });

            var diff = AnyDiff.Diff(obj1, obj2);
            Assert.AreEqual(1, diff.Count);
        }

        [Test]
        public void ShouldDiff_JToken()
        {
            var obj1 = JToken.Parse(@"{ ""list"": [""test1"", ""test2""] }");
            var obj2 = JToken.Parse(@"{ ""list"": [""test3"", ""test4""] }");
            var diff = AnyDiff.Diff(obj1, obj2);
            Assert.AreEqual(4, diff.Count);
        }
    }
}
