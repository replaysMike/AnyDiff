using System;
using System.Linq;
using AnyDiff.Tests.TestObjects;
using AnyDiff.Tests.TestObjects.Abstract;
using NUnit.Framework;

namespace AnyDiff.Tests
{
    [TestFixture]
    public class AbstractTypeTests
    {
        [Test]
        public void If_BaseType_Is_Abstract_And_Both_Objects_Implement_Then_Should_Emit_Whole_Object_If_Types_Different()
        {

                var provider = new DiffProvider();

                var obj1 = new TypeWithAbstractProperty { Id = "1", AbstractProp = new AbstractImplementation1() };
                var obj2 = new TypeWithAbstractProperty { Id = "1", AbstractProp = new AbstractImplementation2() };
                var diff = provider.ComputeDiff(obj1, obj2,ComparisonOptions.All | ComparisonOptions.AllowCompareDifferentObjects);
                Assert.AreEqual(1, diff.Count);

        }
    }
}
