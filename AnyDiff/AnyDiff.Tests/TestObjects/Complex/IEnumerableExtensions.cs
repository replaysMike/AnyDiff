using System;
using System.Collections.Generic;
using System.Text;

namespace AnyDiff.Tests.TestObjects.Complex
{
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// ForEach with index
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="elements"></param>
        /// <param name="action"></param>
        public static void ForEach<T>(this IEnumerable<T> elements, Action<T, int> action)
        {
            var i = 0;
            foreach (var e in elements)
            {
                action(e, i++);
            }
        }
    }
}
