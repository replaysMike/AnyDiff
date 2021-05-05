using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnyDiff.Extensions;
using NUnit.Framework;

namespace AnyDiff.Tests
{
    [TestFixture]
    class PathTests
    {
        [Test]
        public void ShouldInclude_ArrayIndex_InPath()
        {
            var obj1 = new A();
            var obj2 = new A();

            obj2.bs[0].cs[0].ii[2] = 3;

            var diff = obj1.Diff(obj2);
            Assert.AreEqual(".bs[0].cs[0].ii[2]", diff.First().Path);
        }

        [Test]
        public void ShouldInclude_ArrayIndex_InPath_WhenSizeIsDifferent()
        {
            var obj1 = new A();
            var obj2 = new A();

            obj2.bs[0].cs[0].ii = new int[6];

            var diff = obj1.Diff(obj2);
            Assert.AreEqual(".bs[0].cs[0].ii[4]", diff.First().Path);
            Assert.AreEqual(".bs[0].cs[0].ii[5]", diff.ElementAt(1).Path);
        }

        [Test]
        public void ShouldInclude_ArrayIndex_InPath_WhenSizeIsDifferent2()
        {
            var obj1 = new A();
            var obj2 = new A();

            obj2.bs[0].cs[0].ii = new int[2];

            var diff = obj1.Diff(obj2);
            Assert.AreEqual(".bs[0].cs[0].ii[3]", diff.First().Path);
            Assert.AreEqual(".bs[0].cs[0].ii[2]", diff.ElementAt(1).Path);
        }

        //[Test]
        //public void ShouldConsiderAbsDiffWhenValueIsFloat()
        //{
        //    var obj1 = new A();
        //    var obj2 = new A();

        //    obj1.n = 0.000035f;
        //    obj2.n = 0.000035d;

        //    var diff = obj1.Diff(obj2);
        //    Assert.AreEqual(0, diff.Count);
        //}

        class A
        {
            public B[] bs = new B[]
            {
                new B(),
            };

            public double n = 3.5d;
        }

        class B
        {
            public C[] cs = new C[]
            {
                new C(),
            };
        }

        class C
        {
            public int[] ii = new int[4];
        }
    }
}
