using System.Collections.Generic;
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

            var diff = object1.Diff(object2, propertiesToExcludeOrInclude: x => x.Name);
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

            var diff = object1.Diff(object2, propertiesToExcludeOrInclude: "Name");
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
        public void ShouldExclude_NestedCollectionByName()
        {
            // If nested collection excluding with propertiesToExcludeOrInclude is working this test should output only 1 difference because the Child 2/Child 3 difference would be ignored. The values that are different are object1 and object2 id and the 2nd complex child name.
            var basicChild = new BasicChild(1, "Basic Child");
            // Notice "Child 2" at index [1]
            var childrenList1 = new List<ComplexChild> { new ComplexChild(1, "Child 1", basicChild), new ComplexChild(2, "Child 2", basicChild) };
            // Notice "Child 3" at index [1]
            var childrenList2 = new List<ComplexChild> { new ComplexChild(1, "Child 1", basicChild), new ComplexChild(2, "Child 3", basicChild) };

            // Notice id 1
            var object1 = new ComplexObjectWithListChildren(1, "Name 1", childrenList1, basicChild);
            // Notice id 2
            var object2 = new ComplexObjectWithListChildren(2, "Name 1", childrenList2, basicChild);

            var diff = object1.Diff(object2, ".Children.ChildName");
            // There are 2 differences between the objects, but the difference count should be 1, because child name should be ignored.
            Assert.AreEqual(1, diff.Count);
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
        public void ShouldInclude_ByPropertyPathList2()
        {
            var object1 = new Person {
                Id = 1,
                Author = new Author { FirstName = "John", LastName = "Smith", Address = new Address { City = "Los Angeles", Country = "USA" } },
                Gender = Genders.Male,
                Tagline = "Student",
            };
            var object2 = new Person {
                Id = 2,
                Author = new Author { FirstName = "Jane", LastName = "Doe", Address = new Address { City = "Beijing", Country = "China" } },
                Gender = Genders.Female,
                Tagline = "Graduate",
            };

            var diff = object1.Diff(object2, ComparisonOptions.IncludeList | ComparisonOptions.All, ".Author.Address.City", ".Author.Address.Country");
            Assert.AreEqual(2, diff.Count);
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

        [Test]
        public void Should_IncludeOnly_DeepPath()
        {
            var provider = new DiffProvider();

            var obj1 = new DeepObject();
            var obj2 = new DeepObject();
            obj2.DeepChildObject.Id = 999;
            obj2.DeepChildObject.DeepChild2Object.DeepChild3Object.Name = "Test";

            var diff = provider.ComputeDiff<DeepObject>(obj1, obj2, ComparisonOptions.All | ComparisonOptions.IncludeList, x => x.DeepChildObject.DeepChild2Object.DeepChild3Object.Name);
            Assert.AreEqual(1, diff.Count);
            Assert.AreEqual(".DeepChildObject.DeepChild2Object.DeepChild3Object.Name", diff.First().Path);
        }

        [Test]
        public void Should_IncludeOnly_DeepPath2()
        {
            var provider = new DiffProvider();

            var obj1 = new Person {
                Id = 1,
                Author = new Author { FirstName = "John", LastName = "Smith", Address = new Address { City = "Los Angeles", Country = "USA" } },
                Gender = Genders.Male,
                Tagline = "Student",
            };
            var obj2 = new Person {
                Id = 2,
                Author = new Author { FirstName = "Jane", LastName = "Doe", Address = new Address { City = "Beijing", Country = "China" } },
                Gender = Genders.Female,
                Tagline = "Graduate",
            };

            var diff = provider.ComputeDiff<Person>(obj1, obj2, ComparisonOptions.All | ComparisonOptions.IncludeList, x => x.Author.Address.City, x => x.Author.Address.Country);
            Assert.AreEqual(2, diff.Count);
            Assert.AreEqual(".Author.Address.City", diff.First().Path);
            Assert.AreEqual(".Author.Address.Country", diff.Skip(1).First().Path);
        }
    }
}
