using NUnit.Framework;
using System.Drawing;
using Newtonsoft.Json.Linq;
using TypeSupport.Extensions;
#if !NET40
using Microsoft.TeamFoundation.Build.WebApi;
#endif

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

#if !NET40
        [Test]
        public void ShouldDiff_BuildDefinition()
        {
            var obj1 = new BuildDefinition() {
                AuthoredBy = new Microsoft.VisualStudio.Services.WebApi.IdentityRef { UniqueName = "Test Name", DisplayName = "Display Name" },
                BadgeEnabled = true,
                Comment = "Test Comment",
                Id = 1,
            };
            obj1.SetPropertyValue("LatestBuild", new Build { Id = 1 });
            obj1.LatestBuild.Properties.Add("test property", "property value");
            obj1.Drafts.Add(new DefinitionReference { Name = "Definition 1" });
            obj1.Metrics.Add(new BuildMetric { Name = "Metric 1" });
            obj1.Variables.Add("test", new BuildDefinitionVariable { Value = "Test value" });
            var obj2 = new BuildDefinition() {
                AuthoredBy = new Microsoft.VisualStudio.Services.WebApi.IdentityRef { UniqueName = "Test Name", DisplayName = "Display Name 2" },
                BadgeEnabled = true,
                Comment = "Test Comment 2",
                Id = 1
            };
            obj2.SetPropertyValue("LatestBuild", new Build { Id = 2 });
            obj2.LatestBuild.Properties.Add("test property", "property value");
            obj2.Drafts.Add(new DefinitionReference { Name = "Definition 1" });
            obj2.Metrics.Add(new BuildMetric { Name = "Metric 1" });
            obj2.Variables.Add("test", new BuildDefinitionVariable { Value = "Test value" });
            var diff = AnyDiff.Diff(obj1, obj2);
            Assert.AreEqual(3, diff.Count);
        }
#endif
    }
}
