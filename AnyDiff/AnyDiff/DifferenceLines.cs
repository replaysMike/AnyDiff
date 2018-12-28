using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace AnyDiff
{
    /// <summary>
    /// Get line differences between text
    /// </summary>
    public class DifferenceLines
    {
        /// <summary>
        /// Result of a Difference Lines operation
        /// </summary>
        public class LineDifferenceResult
        {
            public ICollection<string> Additions { get; } = new List<string>();
            public ICollection<string> Deletions { get; } = new List<string>();
            public ICollection<string> Unchanged { get; } = new List<string>();

            /// <summary>
            /// Result of a Difference Lines operation
            /// </summary>
            /// <param name="items"></param>
            /// <param name="leftValue"></param>
            /// <param name="rightValue"></param>
            public LineDifferenceResult(Item[] items, string leftValue, string rightValue)
            {
                var aLines = leftValue.Split('\n');
                var bLines = rightValue.Split('\n');
                var n = 0;
                for (var fdx = 0; fdx < items.Length; fdx++)
                {
                    var aItem = items[fdx];
                    while ((n < aItem.StartB) && (n < bLines.Length))
                    {
                        Unchanged.Add(bLines[n]);
                        n++;
                    }
                    for (var m = 0; m < aItem.DeletedA; m++)
                    {
                        Deletions.Add(aLines[aItem.StartA + m]);
                    }
                    while (n < aItem.StartB + aItem.InsertedB)
                    {
                        Additions.Add(bLines[n]);
                        n++;
                    }
                    while (n < bLines.Length)
                    {
                        Unchanged.Add(bLines[n]);
                        n++;
                    }
                }
            }
        }

        /// <summary>
        /// Details of one difference.
        /// </summary>
        public struct Item
        {
            public int StartA { get; set; }
            public int StartB { get; set; }
            public int DeletedA { get; set; }
            public int InsertedB { get; set; }
        }

        /// <summary>
        /// Shortest Middle Snake Return Data
        /// </summary>
        private struct SMSRD : IEquatable<SMSRD>
        {
            internal int _x, _y;

            public bool Equals(SMSRD other)
            {
                return _x == other._x && _y == other._y;
            }
        }

        /// <summary>
        /// Find the difference in 2 texts, comparing by textlines.
        /// </summary>
        /// <param name="TextA">A-version of the text (usualy the old one)</param>
        /// <param name="TextB">B-version of the text (usualy the new one)</param>
        /// <returns>Returns a array of Items that describe the differences.</returns>
        public static LineDifferenceResult DiffLines(string TextA, string TextB)
        {
            return DiffLines(TextA, TextB, false, false, false);
        }


        /// <summary>
        /// Find the difference in 2 text documents, comparing by textlines.
        /// The algorithm itself is comparing 2 arrays of numbers so when comparing 2 text documents
        /// each line is converted into a (hash) number. This hash-value is computed by storing all
        /// textlines into a common hashtable so i can find dublicates in there, and generating a 
        /// new number each time a new textline is inserted.
        /// </summary>
        /// <param name="TextA">A-version of the text (usualy the old one)</param>
        /// <param name="TextB">B-version of the text (usualy the new one)</param>
        /// <param name="trimSpace">When set to true, all leading and trailing whitespace characters are stripped out before the comparation is done.</param>
        /// <param name="ignoreSpace">When set to true, all whitespace characters are converted to a single space character before the comparation is done.</param>
        /// <param name="ignoreCase">When set to true, all characters are converted to their lowercase equivivalence before the comparation is done.</param>
        /// <returns>Returns a array of Items that describe the differences.</returns>
        public static LineDifferenceResult DiffLines(string TextA, string TextB, bool trimSpace, bool ignoreSpace, bool ignoreCase)
        {
            // prepare the input-text and convert to comparable numbers.
            var h = new Hashtable(TextA.Length + TextB.Length);

            // The A-Version of the data (original data) to be compared.
            var DataA = new DiffData(DiffCodes(TextA, h, trimSpace, ignoreSpace, ignoreCase));

            // The B-Version of the data (modified data) to be compared.
            var DataB = new DiffData(DiffCodes(TextB, h, trimSpace, ignoreSpace, ignoreCase));

            h.Clear();

            LCS(DataA, 0, DataA.Length, DataB, 0, DataB.Length);
            return new LineDifferenceResult(CreateDiffs(DataA, DataB), TextA, TextB);
        }


        /// <summary>
        /// Find the difference in 2 arrays of integers.
        /// </summary>
        /// <param name="ArrayA">A-version of the numbers (usualy the old one)</param>
        /// <param name="ArrayB">B-version of the numbers (usualy the new one)</param>
        /// <returns>Returns a array of Items that describe the differences.</returns>
        public static Item[] DiffInt(int[] ArrayA, int[] ArrayB)
        {
            // The A-Version of the data (original data) to be compared.
            var DataA = new DiffData(ArrayA);

            // The B-Version of the data (modified data) to be compared.
            var DataB = new DiffData(ArrayB);

            LCS(DataA, 0, DataA.Length, DataB, 0, DataB.Length);
            return CreateDiffs(DataA, DataB);
        }


        /// <summary>
        /// This function converts all textlines of the text into unique numbers for every unique textline
        /// so further work can work only with simple numbers.
        /// </summary>
        /// <param name="aText">the input text</param>
        /// <param name="hashTable">This extern initialized hashtable is used for storing all ever used textlines.</param>
        /// <param name="trimSpace">ignore leading and trailing space characters</param>
        /// <param name="ignoreSpace">True to ignore whitespace</param>
        /// <param name="ignoreCase">True to ignore case</param>
        /// <returns>a array of integers.</returns>
        private static int[] DiffCodes(string aText, Hashtable hashTable, bool trimSpace, bool ignoreSpace, bool ignoreCase)
        {
            // get all codes of the text
            var text = aText;
            string[] lines;
            int[] codes;
            var lastUsedCode = hashTable.Count;
            object charCode;
            string str;

            // strip off all cr, only use lf as textline separator.
            text = text.Replace("\r", "");
            lines = text.Split('\n');

            codes = new int[lines.Length];

            for (var i = 0; i < lines.Length; ++i)
            {
                str = lines[i];
                if (trimSpace)
                    str = str.Trim();

                if (ignoreSpace)
                {
                    str = Regex.Replace(str, "\\s+", " ");
                }

                if (ignoreCase)
                    str = str.ToLower(CultureInfo.InvariantCulture);

                charCode = hashTable[str];
                if (charCode == null)
                {
                    lastUsedCode++;
                    hashTable[str] = lastUsedCode;
                    codes[i] = lastUsedCode;
                }
                else
                {
                    codes[i] = (int)charCode;
                }
            }
            return (codes);
        }


        /// <summary>
        /// This is the algorithm to find the Shortest Middle Snake (SMS).
        /// </summary>
        /// <param name="dataLeft">sequence A</param>
        /// <param name="lowerLeft">lower bound of the actual range in DataA</param>
        /// <param name="upperLeft">upper bound of the actual range in DataA (exclusive)</param>
        /// <param name="dataRight">sequence B</param>
        /// <param name="lowerRight">lower bound of the actual range in DataB</param>
        /// <param name="upperRight">upper bound of the actual range in DataB (exclusive)</param>
        /// <returns>a MiddleSnakeData record containing x,y and u,v</returns>
        private static SMSRD SMS(DiffData dataLeft, int lowerLeft, int upperLeft, DiffData dataRight, int lowerRight, int upperRight)
        {
            SMSRD ret;
            var max = dataLeft.Length + dataRight.Length + 1;

            var downK = lowerLeft - lowerRight; // the k-line to start the forward search
            var upK = upperLeft - upperRight; // the k-line to start the reverse search

            var delta = (upperLeft - lowerLeft) - (upperRight - lowerRight);
            var oddDelta = (delta & 1) != 0;

            /// vector for the (0,0) to (x,y) search
            var downVector = new int[2 * max + 2];

            /// vector for the (u,v) to (N,M) search
            var upVector = new int[2 * max + 2];

            // The vectors in the publication accepts negative indexes. the vectors implemented here are 0-based
            // and are access using a specific offset: UpOffset UpVector and DownOffset for DownVektor
            var downOffset = max - downK;
            var upOffset = max - upK;

            var MaxD = ((upperLeft - lowerLeft + upperRight - lowerRight) / 2) + 1;

            // init vectors
            downVector[downOffset + downK + 1] = lowerLeft;
            upVector[upOffset + upK - 1] = upperLeft;

            for (var d = 0; d <= MaxD; d++)
            {

                // Extend the forward path.
                for (var k = downK - d; k <= downK + d; k += 2)
                {
                    // find the only or better starting point
                    int x, y;
                    if (k == downK - d)
                    {
                        x = downVector[downOffset + k + 1]; // down
                    }
                    else
                    {
                        x = downVector[downOffset + k - 1] + 1; // a step to the right
                        if ((k < downK + d) && (downVector[downOffset + k + 1] >= x))
                            x = downVector[downOffset + k + 1]; // down
                    }
                    y = x - k;

                    // find the end of the furthest reaching forward D-path in diagonal k.
                    while ((x < upperLeft) && (y < upperRight) && (dataLeft.Data[x] == dataRight.Data[y]))
                    {
                        x++;
                        y++;
                    }
                    downVector[downOffset + k] = x;

                    // overlap ?
                    if (oddDelta && (upK - d < k) && (k < upK + d))
                    {
                        if (upVector[upOffset + k] <= downVector[downOffset + k])
                        {
                            ret._x = downVector[downOffset + k];
                            ret._y = downVector[downOffset + k] - k;
                            return (ret);
                        }
                    }

                }

                // Extend the reverse path.
                for (var k = upK - d; k <= upK + d; k += 2)
                {
                    // find the only or better starting point
                    int x, y;
                    if (k == upK + d)
                    {
                        x = upVector[upOffset + k - 1]; // up
                    }
                    else
                    {
                        x = upVector[upOffset + k + 1] - 1; // left
                        if ((k > upK - d) && (upVector[upOffset + k - 1] < x))
                            x = upVector[upOffset + k - 1]; // up
                    } // if
                    y = x - k;

                    while ((x > lowerLeft) && (y > lowerRight) && (dataLeft.Data[x - 1] == dataRight.Data[y - 1]))
                    {
                        x--;
                        y--; // diagonal
                    }
                    upVector[upOffset + k] = x;

                    // overlap ?
                    if (!oddDelta && (downK - d <= k) && (k <= downK + d))
                    {
                        if (upVector[upOffset + k] <= downVector[downOffset + k])
                        {
                            ret._x = downVector[downOffset + k];
                            ret._y = downVector[downOffset + k] - k;
                            return (ret);
                        }
                    }

                }

            }

            throw new InvalidOperationException("Unknown error occurred.");
        }


        /// <summary>
        /// This is the divide-and-conquer implementation of the longes common-subsequence (LCS) 
        /// algorithm.
        /// The published algorithm passes recursively parts of the A and B sequences.
        /// To avoid copying these arrays the lower and upper bounds are passed while the sequences stay constant.
        /// </summary>
        /// <param name="dataLeft">sequence A</param>
        /// <param name="lowerLeft">lower bound of the actual range in DataA</param>
        /// <param name="upperLeft">upper bound of the actual range in DataA (exclusive)</param>
        /// <param name="dataRight">sequence B</param>
        /// <param name="lowerRight">lower bound of the actual range in DataB</param>
        /// <param name="upperRight">upper bound of the actual range in DataB (exclusive)</param>
        private static void LCS(DiffData dataLeft, int lowerLeft, int upperLeft, DiffData dataRight, int lowerRight, int upperRight)
        {
            // Fast walkthrough equal lines at the start
            while (lowerLeft < upperLeft && lowerRight < upperRight && dataLeft.Data[lowerLeft] == dataRight.Data[lowerRight])
            {
                lowerLeft++;
                lowerRight++;
            }

            // Fast walkthrough equal lines at the end
            while (lowerLeft < upperLeft && lowerRight < upperRight && dataLeft.Data[upperLeft - 1] == dataRight.Data[upperRight - 1])
            {
                --upperLeft;
                --upperRight;
            }

            if (lowerLeft == upperLeft)
            {
                // mark as inserted lines.
                while (lowerRight < upperRight)
                    dataRight.Modified[lowerRight++] = true;

            }
            else if (lowerRight == upperRight)
            {
                // mark as deleted lines.
                while (lowerLeft < upperLeft)
                    dataLeft.Modified[lowerLeft++] = true;

            }
            else
            {
                // Find the middle snakea and length of an optimal path for A and B
                var smsrd = SMS(dataLeft, lowerLeft, upperLeft, dataRight, lowerRight, upperRight);

                // The path is from LowerX to (x,y) and (x,y) ot UpperX
                LCS(dataLeft, lowerLeft, smsrd._x, dataRight, lowerRight, smsrd._y);
                LCS(dataLeft, smsrd._x, upperLeft, dataRight, smsrd._y, upperRight);  // 2002.09.20: no need for 2 points 
            }
        }


        /// <summary>
        /// Scan the tables of which lines are inserted and deleted,
        /// producing an edit script in forward order.  
        /// </summary>
        /// dynamic array
        private static Item[] CreateDiffs(DiffData dataLeft, DiffData dataRight)
        {
            var aList = new ArrayList();
            Item aItem;
            Item[] result;

            int startLeft, startRight;
            int lineLeft, lineRight;

            lineLeft = 0;
            lineRight = 0;
            while (lineLeft < dataLeft.Length || lineRight < dataRight.Length)
            {
                if ((lineLeft < dataLeft.Length) && (!dataLeft.Modified[lineLeft])
                    && (lineRight < dataRight.Length) && (!dataRight.Modified[lineRight]))
                {
                    // equal lines
                    lineLeft++;
                    lineRight++;

                }
                else
                {
                    // maybe deleted and/or inserted lines
                    startLeft = lineLeft;
                    startRight = lineRight;

                    while (lineLeft < dataLeft.Length && (lineRight >= dataRight.Length || dataLeft.Modified[lineLeft]))
                        lineLeft++;

                    while (lineRight < dataRight.Length && (lineLeft >= dataLeft.Length || dataRight.Modified[lineRight]))
                        lineRight++;

                    if ((startLeft < lineLeft) || (startRight < lineRight))
                    {
                        // store a new difference-item
                        aItem = new Item();
                        aItem.StartA = startLeft;
                        aItem.StartB = startRight;
                        aItem.DeletedA = lineLeft - startLeft;
                        aItem.InsertedB = lineRight - startRight;
                        aList.Add(aItem);
                    }
                }
            }

            result = new Item[aList.Count];
            aList.CopyTo(result);

            return (result);
        }

        /// <summary>
        /// Data on one input file being compared.  
        /// </summary>
        internal class DiffData
        {

            /// <summary>Number of elements (lines).</summary>
            internal int Length { get; set; }

            /// <summary>Buffer of numbers that will be compared.</summary>
            internal int[] Data { get; set; }

            /// <summary>
            /// Array of booleans that flag for modified data.
            /// This is the result of the diff.
            /// This means deletedA in the first Data or inserted in the second Data.
            /// </summary>
            internal bool[] Modified { get; set; }

            /// <summary>
            /// Initialize the Diff-Data buffer.
            /// </summary>
            /// <param name="data">reference to the buffer</param>
            internal DiffData(int[] initData)
            {
                Data = initData;
                Length = initData.Length;
                Modified = new bool[Length + 2];
            }

        }
    }
}
