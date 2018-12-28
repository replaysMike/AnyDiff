using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnyDiff
{
    /// <summary>
    /// Get word differences between text
    /// </summary>
    public class DifferenceWords
    {
        public class WordDifferences
        {
            public ICollection<string> Additions { get; set; } = new List<string>();
            public ICollection<string> Deletions { get; set; } = new List<string>();

            public override string ToString()
            {
                var str = new StringBuilder();
                foreach (var addition in Additions)
                    str.Append("+" + addition + Environment.NewLine);
                foreach (var deletion in Deletions)
                    str.Append("-" + deletion + Environment.NewLine);
                return str.ToString();
            }
        }

        /// <summary>
        /// Get word differences between text
        /// </summary>
        /// <param name="leftText"></param>
        /// <param name="rightText"></param>
        /// <returns></returns>
        public static WordDifferences DiffWords(string leftText, string rightText)
        {
            return DiffWords(leftText, rightText, false);
        }

        /// <summary>
        /// Get word differences between text
        /// </summary>
        /// <param name="leftText"></param>
        /// <param name="rightText"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static WordDifferences DiffWords(string leftText, string rightText, bool ignoreCase)
        {
            var comparisonType = StringComparison.InvariantCulture;
            if (ignoreCase)
                comparisonType = StringComparison.InvariantCultureIgnoreCase;
            var leftWords = leftText.Split(new [] { ' ', '.', ',', ';', '-' }).ToList();
            var rightWords = rightText.Split(new [] { ' ', '.', ',', ';', '-' }).ToList();
            var diff = new WordDifferences();

            for (var i = 0; i < leftWords.Count; i++)
            {
                if (rightWords.Count > i && !rightWords[i].Equals(leftWords[i], comparisonType))
                    diff.Deletions.Add(leftWords[i]);
            }

            for (var i = 0; i < rightWords.Count; i++)
            {
                if (leftWords.Count > i && !leftWords[i].Equals(rightWords[i], comparisonType))
                    diff.Additions.Add(rightWords[i]);
            }

            return diff;
        }
    }
}
