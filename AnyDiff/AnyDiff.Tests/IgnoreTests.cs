﻿using System.Collections.Generic;
using System.Linq;
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
        public void ShouldExclude_ChildrenByPropertyList()
        {
            var object1 = new ComplexObjectWithListChildren(1, "A string",
                new List<ComplexChild> {
                    new ComplexChild(1, "Child1",
                        new BasicChild(1, "BasicChild1")),
                    new ComplexChild(2, "Child2",
                        new BasicChild(2, "BasicChild2"))
                }, new BasicChild(1, "BasicChild1"));
            var object2 = new ComplexObjectWithListChildren(1, "A string",
                new List<ComplexChild> {
                    new ComplexChild(1, "Child1",
                        new BasicChild(3, "BasicChild1")),
                    new ComplexChild(2, "Child2",
                        new BasicChild(4, "BasicChild2"))
                }, new BasicChild(2, "BasicChild1"));

            var diff = object1.Diff(object2,
                x => x.Name,
                x => x.BasicChild.BasicChildId,
                x => x.Children.Select(y => y.ComplexChildId),
                x => x.Children.Select(y => y.BasicChild.BasicChildId),
                x => x.Children.Select(y => y.BasicChild.Children.Select(z => z.BasicChildId))
            );
            Assert.AreEqual(0, diff.Count);
        }

        [Test]
        public void ShouldExclude_OneChildByPropertyList()
        {
            var object1 = new ComplexObjectWithListChildren(1, "A string",
                new List<ComplexChild> {
                    new ComplexChild(1, "Child1",
                        new BasicChild(1, "BasicChild1")),
                    new ComplexChild(2, "Child2",
                        new BasicChild(2, "BasicChild2"))
                }, new BasicChild(1, "BasicChild1"));
            var object2 = new ComplexObjectWithListChildren(1, "A string",
                new List<ComplexChild> {
                    new ComplexChild(1, "Child1",
                        new BasicChild(1, "BasicChild1")),
                    new ComplexChild(3, "Child2",
                        new BasicChild(2, "BasicChild2"))
                }, new BasicChild(1, "BasicChild1"));

            var diff = object1.Diff(object2,
                x => x.Children.Select(y => y.ComplexChildId)
            );
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
        public void ShouldInclude_AllChildren()
        {
            var object1 = new ComplexObjectWithListChildren(1, "A string",
                new List<ComplexChild> {
                    new ComplexChild(1, "Child1",
                        new BasicChild(1, "BasicChild1")),
                    new ComplexChild(2, "Child2",
                        new BasicChild(2, "BasicChild2"))
                }, new BasicChild(1, "BasicChild1"));
            var object2 = new ComplexObjectWithListChildren(2, "A different string",
                new List<ComplexChild> {
                    new ComplexChild(1, "Child1",
                        new BasicChild(1, "BasicChild1")),
                    new ComplexChild(2, "Child2",
                        new BasicChild(2, "BasicChild2"))
                }, new BasicChild(1, "BasicChild1"));

            var diff = object1.Diff(object2, ComparisonOptions.IncludeList | ComparisonOptions.All,
                x => x.Children
            );
            Assert.AreEqual(0, diff.Count);
        }

        [Test]
        public void ShouldInclude_AllChildren_NoInheritance()
        {
            var object1 = new ComplexObjectWithListChildren(1, "A string",
                new List<ComplexChild> {
                    new ComplexChild(1, "Child1",
                        new BasicChild(1, "BasicChild1")),
                    new ComplexChild(2, "Child2",
                        new BasicChild(2, "BasicChild2"))
                }, new BasicChild(1, "BasicChild1"));
            var object2 = new ComplexObjectWithListChildren(2, "A different string",
                new List<ComplexChild> {
                    new ComplexChild(1, "Child1",
                        new BasicChild(1, "BasicChild1")),
                    new ComplexChild(3, "Child2",
                        new BasicChild(2, "BasicChild2"))
                }, new BasicChild(1, "BasicChild1"));

            var diff = object1.Diff(object2, ComparisonOptions.IncludeList | ComparisonOptions.IncludeListNoInheritance | ComparisonOptions.All,
                x => x.Children, x => x.Children.Select(y => y.ComplexChildId)
            );
            // only the ComplexChildId should have changed
            Assert.AreEqual(1, diff.Count);
            Assert.AreEqual(".Children.ComplexChildId", diff.First().Path);
            Assert.AreEqual(2, diff.First().LeftValue);
            Assert.AreEqual(3, diff.First().RightValue);
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

        [Test]
        public void Should_IgnoreByCustomAttribute()
        {
            var object1 = new CustomAttributeObject(1, "A string");
            var object2 = new CustomAttributeObject(1, "A different string");

            var diffOptions = new DiffOptions(new List<object> { typeof(System.ComponentModel.DescriptionAttribute) });
            var diff = object1.Diff(object2, ComparisonOptions.All, diffOptions);
            Assert.AreEqual(0, diff.Count);
        }

        [Test]
        public void Should_IgnoreByCustomAttributeName()
        {
            var object1 = new CustomAttributeObject(1, "A string");
            var object2 = new CustomAttributeObject(1, "A different string");

            var diffOptions = new DiffOptions(new List<object> { "DescriptionAttribute" });
            var diff = object1.Diff(object2, ComparisonOptions.All, diffOptions);
            Assert.AreEqual(0, diff.Count);
        }
    }
}
