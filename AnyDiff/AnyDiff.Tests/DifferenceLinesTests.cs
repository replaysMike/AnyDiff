using NUnit.Framework;
using AnyDiff;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AnyDiff.Tests
{
    [TestFixture]
    public class DifferenceLinesTests
    {
        [Test]
        public void Should_Detect_LineDifference()
        {
            var diff = DifferenceLines.DiffLines("Lorem ipsum dolor sit amet.", "Lorem ipsum dolor amet.");
            Assert.AreEqual(1, diff.Additions.Count);
            Assert.AreEqual(1, diff.Deletions.Count);
            Assert.AreEqual("Lorem ipsum dolor amet.", diff.Additions.First());
        }

        [Test]
        public void Should_Detect_LineDifferenceTrimWhitespace()
        {
            var diff = DifferenceLines.DiffLines("  Lorem ipsum dolor sit amet.", "Lorem ipsum dolor amet.", true, false, false);
            Assert.AreEqual(1, diff.Additions.Count);
            Assert.AreEqual(1, diff.Deletions.Count);
            Assert.AreEqual("Lorem ipsum dolor amet.", diff.Additions.First());
        }

        [Test]
        public void Should_Detect_LineDifferenceIgnoringWhitespace()
        {
            var diff = DifferenceLines.DiffLines("  Lorem  ipsum dolor    sit amet.", "Lorem ipsum dolor amet.", false, true, false);
            Assert.AreEqual(1, diff.Additions.Count);
            Assert.AreEqual(1, diff.Deletions.Count);
            Assert.AreEqual("Lorem ipsum dolor amet.", diff.Additions.First());
        }

        [Test]
        public void Should_Detect_LineDifferenceIgnoringCase()
        {
            var diff = DifferenceLines.DiffLines("lorem ipsum Dolor sit Amet.", "Lorem ipsum dolor amet.", false, false, true);
            Assert.AreEqual(1, diff.Additions.Count);
            Assert.AreEqual(1, diff.Deletions.Count);
            Assert.AreEqual("Lorem ipsum dolor amet.", diff.Additions.First());
        }

        [Test]
        public void Should_Detect_IntsDifference()
        {
            var diff = DifferenceLines.DiffInt(new [] { 1, 2, 3 }, new[] { 1, 4, 3 });
            Assert.AreEqual(1, diff.Length);
        }

        [Test]
        public void Should_Detect_IntsNoDifference()
        {
            var diff = DifferenceLines.DiffInt(new[] { 1, 2, 3 }, new[] { 1, 2, 3 });
            Assert.AreEqual(0, diff.Length);
        }
    }
}
