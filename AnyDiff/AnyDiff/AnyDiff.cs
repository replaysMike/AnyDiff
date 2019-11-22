using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AnyDiff
{
    /// <summary>
    /// Compare two objects for value differences
    /// </summary>
    public static class AnyDiff
    {

        /// <summary>
        /// Compare two objects for value differences
        /// </summary>
        /// <param name="left">Object A</param>
        /// <param name="right">Object B</param>
        /// <returns></returns>
        public static ICollection<Difference> Diff(object left, object right)
        {
            var diffProvider = new DiffProvider();
            return diffProvider.ComputeDiff(left, right, DiffProvider.DefaultMaxDepth, ComparisonOptions.All);
        }

        /// <summary>
        /// Compare two objects for value differences
        /// </summary>
        /// <param name="left">Object A</param>
        /// <param name="right">Object B</param>
        /// <param name="diffOptions">Specify custom diff options</param>
        /// <returns></returns>
        public static ICollection<Difference> Diff(object left, object right, DiffOptions diffOptions)
        {
            var diffProvider = new DiffProvider();
            return diffProvider.ComputeDiff(left, right, DiffProvider.DefaultMaxDepth, ComparisonOptions.All, diffOptions);
        }

        /// <summary>
        /// Compare two objects for value differences
        /// </summary>
        /// <param name="left">Object A</param>
        /// <param name="right">Object B</param>
        /// <param name="comparisonOptions">Specify the comparison options</param>
        /// <returns></returns>
        public static ICollection<Difference> Diff(object left, object right, ComparisonOptions comparisonOptions)
        {
            var diffProvider = new DiffProvider();
            return diffProvider.ComputeDiff(left, right, DiffProvider.DefaultMaxDepth, comparisonOptions);
        }

        /// <summary>
        /// Compare two objects for value differences
        /// </summary>
        /// <param name="left">Object A</param>
        /// <param name="right">Object B</param>
        /// <param name="comparisonOptions">Specify the comparison options</param>
        /// <param name="diffOptions">Specify custom diff options</param>
        /// <returns></returns>
        public static ICollection<Difference> Diff(object left, object right, ComparisonOptions comparisonOptions, DiffOptions diffOptions)
        {
            var diffProvider = new DiffProvider();
            return diffProvider.ComputeDiff(left, right, DiffProvider.DefaultMaxDepth, comparisonOptions, diffOptions);
        }

        /// <summary>
        /// Compare two objects for value differences
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="left">Object A</param>
        /// <param name="right">Object B</param>
        /// <param name="comparisonOptions">Specify the comparison options</param>
        /// <returns></returns>
        public static ICollection<Difference> Diff<T>(T left, T right, ComparisonOptions comparisonOptions)
        {
            var diffProvider = new DiffProvider();
            return diffProvider.ComputeDiff(left, right, DiffProvider.DefaultMaxDepth, comparisonOptions);
        }

        /// <summary>
        /// Compare two objects for value differences
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="left">Object A</param>
        /// <param name="right">Object B</param>
        /// <param name="comparisonOptions">Specify the comparison options</param>
        /// <param name="diffOptions">Specify custom diff options</param>
        /// <returns></returns>
        public static ICollection<Difference> Diff<T>(T left, T right, ComparisonOptions comparisonOptions, DiffOptions diffOptions)
        {
            var diffProvider = new DiffProvider();
            return diffProvider.ComputeDiff(left, right, DiffProvider.DefaultMaxDepth, comparisonOptions, diffOptions);
        }

        /// <summary>
        /// Compare two objects for value differences
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="left">Object A</param>
        /// <param name="right">Object B</param>
        /// <param name="comparisonOptions">Specify the comparison options</param>
        /// <param name="propertyList">A list of property names or full path names to include/exclude</param>
        /// <returns></returns>
        public static ICollection<Difference> Diff<T>(T left, T right, ComparisonOptions comparisonOptions, string[] propertyList)
        {
            var diffProvider = new DiffProvider();
            return diffProvider.ComputeDiff(left, right, DiffProvider.DefaultMaxDepth, comparisonOptions, DiffOptions.Default, propertyList);
        }

        /// <summary>
        /// Compare two objects for value differences
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="left">Object A</param>
        /// <param name="right">Object B</param>
        /// <param name="comparisonOptions">Specify the comparison options</param>
        /// <param name="diffOptions">Specify custom diff options</param>
        /// <param name="propertyList">A list of property names or full path names to include/exclude</param>
        /// <returns></returns>
        public static ICollection<Difference> Diff<T>(T left, T right, ComparisonOptions comparisonOptions, DiffOptions diffOptions, string[] propertyList)
        {
            var diffProvider = new DiffProvider();
            return diffProvider.ComputeDiff(left, right, DiffProvider.DefaultMaxDepth, comparisonOptions, diffOptions, propertyList);
        }

        /// <summary>
        /// Compare two objects for value differences
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="left">Object A</param>
        /// <param name="right">Object B</param>
        /// <param name="comparisonOptions">Specify the comparison options</param>
        /// <param name="propertyList">A list of property names or full path names to include/exclude</param>
        /// <returns></returns>
        public static ICollection<Difference> Diff<T>(T left, T right, ComparisonOptions comparisonOptions, params Expression<Func<T, object>>[] propertyList)
        {
            var diffProvider = new DiffProvider();
            return diffProvider.ComputeDiff(left, right, DiffProvider.DefaultMaxDepth, comparisonOptions, propertyList);
        }

        /// <summary>
        /// Compare two objects for value differences
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="left">Object A</param>
        /// <param name="right">Object B</param>
        /// <param name="comparisonOptions">Specify the comparison options</param>
        /// <param name="diffOptions">Specify custom diff options</param>
        /// <param name="propertyList">A list of property names or full path names to include/exclude</param>
        /// <returns></returns>
        public static ICollection<Difference> Diff<T>(T left, T right, ComparisonOptions comparisonOptions, DiffOptions diffOptions, params Expression<Func<T, object>>[] propertyList)
        {
            var diffProvider = new DiffProvider();
            return diffProvider.ComputeDiff(left, right, DiffProvider.DefaultMaxDepth, comparisonOptions, diffOptions, propertyList);
        }

        /// <summary>
        /// Compare two objects for value differences
        /// </summary>
        /// <param name="left">Object A</param>
        /// <param name="right">Object B</param>
        /// <param name="maxDepth">Maximum recursion depth</param>
        /// <param name="comparisonOptions">Specify the comparison options</param>
        /// <param name="propertyList">A list of property names or full path names to include/exclude</param>
        /// <returns></returns>
        public static ICollection<Difference> Diff(object left, object right, int maxDepth, ComparisonOptions comparisonOptions, params string[] propertyList)
        {
            var diffProvider = new DiffProvider();
            return diffProvider.ComputeDiff(left, right, maxDepth, comparisonOptions, propertyList);
        }

        /// <summary>
        /// Compare two objects for value differences
        /// </summary>
        /// <param name="left">Object A</param>
        /// <param name="right">Object B</param>
        /// <param name="maxDepth">Maximum recursion depth</param>
        /// <param name="comparisonOptions">Specify the comparison options</param>
        /// <param name="diffOptions">Specify custom diff options</param>
        /// <param name="propertyList">A list of property names or full path names to include/exclude</param>
        /// <returns></returns>
        public static ICollection<Difference> Diff(object left, object right, int maxDepth, ComparisonOptions comparisonOptions, DiffOptions diffOptions, params string[] propertyList)
        {
            var diffProvider = new DiffProvider();
            return diffProvider.ComputeDiff(left, right, maxDepth, comparisonOptions, diffOptions, propertyList);
        }

        /// <summary>
        /// Compare two objects for value differences
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="left">Object A</param>
        /// <param name="right">Object B</param>
        /// <param name="maxDepth">Maximum recursion depth</param>
        /// <param name="comparisonOptions">Specify the comparison options</param>
        /// <param name="propertyList">A list of property names or full path names to include/exclude</param>
        /// <returns></returns>
        public static ICollection<Difference> Diff<T>(T left, T right, int maxDepth, ComparisonOptions comparisonOptions, params Expression<Func<T, object>>[] propertyList)
        {
            var diffProvider = new DiffProvider();
            return diffProvider.ComputeDiff(left, right, maxDepth, comparisonOptions, propertyList);
        }

        /// <summary>
        /// Compare two objects for value differences
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="left">Object A</param>
        /// <param name="right">Object B</param>
        /// <param name="maxDepth">Maximum recursion depth</param>
        /// <param name="comparisonOptions">Specify the comparison options</param>
        /// <param name="diffOptions">Specify custom diff options</param>
        /// <param name="propertyList">A list of property names or full path names to include/exclude</param>
        /// <returns></returns>
        public static ICollection<Difference> Diff<T>(T left, T right, int maxDepth, ComparisonOptions comparisonOptions, DiffOptions diffOptions, params Expression<Func<T, object>>[] propertyList)
        {
            var diffProvider = new DiffProvider();
            return diffProvider.ComputeDiff(left, right, maxDepth, comparisonOptions, diffOptions, propertyList);
        }
    }
}
