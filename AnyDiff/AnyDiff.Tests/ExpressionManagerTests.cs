using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AnyDiff.Tests.TestObjects;
using NUnit.Framework;

namespace AnyDiff.Tests
{
    [TestFixture]
    public class ExpressionManagerTests
    {
        [Test]
        public void Should_CreatePath_BasicExpression()
        {
            var manager = new ExpressionManager();
            var expressions = CreateExpressions<ComplexObject>(x => x.Id);
            var paths = expressions.Select(x => manager.GetPropertyPath(x)).ToList();
            CollectionAssert.AreEqual(new[] { ".Id" }, paths);
        }

        [Test]
        public void Should_CreatePath_WithChildrenRoot()
        {
            var manager = new ExpressionManager();
            var expressions = CreateExpressions<ComplexObjectWithListChildren>(
                x => x.Id,
                x => x.Name,
                x => x.Children
            );
            var paths = expressions.Select(x => manager.GetPropertyPath(x)).ToList();
            CollectionAssert.AreEqual(new[] { ".Id", ".Name", ".Children" }, paths);
        }

        [Test]
        public void Should_CreatePath_WithChildrenSubselect()
        {
            var manager = new ExpressionManager();
            var expressions = CreateExpressions<ComplexObjectWithListChildren>(
                x => x.Id,
                x => x.Name,
                x => x.Children.Select(y => y.BasicChild.BasicChildId)
            );
            var paths = expressions.Select(x => manager.GetPropertyPath(x)).ToList();
            CollectionAssert.AreEqual(new[] { ".Id", ".Name", ".Children.BasicChild.BasicChildId" }, paths);
        }

        [Test]
        public void Should_CreatePath_WithDeepSubselect()
        {
            var manager = new ExpressionManager();
            var expressions = CreateExpressions<ComplexObjectWithListChildren>(
                x => x.Id,
                x => x.Name,
                x => x.Children.Select(y => y.BasicChild.BasicChildId),
                x => x.BasicChild.Children.Select(y => y.BasicChildId),
                x => x.BasicChild.Children.Select(y => y.Children.Select(z => z.BasicChildName))
            );
            var paths = expressions.Select(x => manager.GetPropertyPath(x)).ToList();
            CollectionAssert.AreEqual(new[] {
                ".Id",
                ".Name",
                ".Children.BasicChild.BasicChildId",
                ".BasicChild.Children.BasicChildId",
                ".BasicChild.Children.Children.BasicChildName"}, paths);
        }

        /// <summary>
        /// Helper for creating expressions
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyList"></param>
        /// <returns></returns>
        private ICollection<Expression> CreateExpressions<T>(params Expression<Func<T, object>>[] propertyList)
        {
            var expressions = new List<Expression>();
            foreach (var expression in propertyList)
            {
                expressions.Add(expression);
            }
            return expressions;
        }
    }
}
