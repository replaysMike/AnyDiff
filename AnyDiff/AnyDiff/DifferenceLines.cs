using System;
using System.Collections;
using System.Collections.Generic;
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
            public ICollection<string> Additions = new List<string>();
            public ICollection<string> Deletions = new List<string>();
            public ICollection<string> Unchanged = new List<string>();

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
                    for (var m = 0; m < aItem.deletedA; m++)
                    {
                        Deletions.Add(aLines[aItem.StartA + m]);
                    }
                    while (n < aItem.StartB + aItem.insertedB)
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
            public int StartA;
            public int StartB;
            public int deletedA;
            public int insertedB;
        }

        /// <summary>
        /// Shortest Middle Snake Return Data
        /// </summary>
        private struct SMSRD
        {
            internal int _x, _y;
        }

        /// <summary>
        /// Find the difference in 2 texts, comparing by textlines.
        /// </summary>
        /// <param name="TextA">A-version of the text (usualy the old one)</param>
        /// <param name="TextB">B-version of the text (usualy the new one)</param>
        /// <returns>Returns a array of Items that describe the differences.</returns>
        public LineDifferenceResult DiffLines(string TextA, string TextB)
        {
            return (DiffLines(TextA, TextB, false, false, false));
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

            h = null; // free up hashtable memory (maybe)

            LCS(DataA, 0, DataA._length, DataB, 0, DataB._length);
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

            LCS(DataA, 0, DataA._length, DataB, 0, DataB._length);
            return CreateDiffs(DataA, DataB);
        }


        /// <summary>
        /// This function converts all textlines of the text into unique numbers for every unique textline
        /// so further work can work only with simple numbers.
        /// </summary>
        /// <param name="aText">the input text</param>
        /// <param name="h">This extern initialized hashtable is used for storing all ever used textlines.</param>
        /// <param name="trimSpace">ignore leading and trailing space characters</param>
        /// <returns>a array of integers.</returns>
        private static int[] DiffCodes(string aText, Hashtable h, bool trimSpace, bool ignoreSpace, bool ignoreCase)
        {
            // get all codes of the text
            string[] Lines;
            int[] Codes;
            var lastUsedCode = h.Count;
            object aCode;
            string s;

            // strip off all cr, only use lf as textline separator.
            aText = aText.Replace("\r", "");
            Lines = aText.Split('\n');

            Codes = new int[Lines.Length];

            for (var i = 0; i < Lines.Length; ++i)
            {
                s = Lines[i];
                if (trimSpace)
                    s = s.Trim();

                if (ignoreSpace)
                {
                    s = Regex.Replace(s, "\\s+", " ");
                }

                if (ignoreCase)
                    s = s.ToLower();

                aCode = h[s];
                if (aCode == null)
                {
                    lastUsedCode++;
                    h[s] = lastUsedCode;
                    Codes[i] = lastUsedCode;
                }
                else
                {
                    Codes[i] = (int)aCode;
                }
            }
            return (Codes);
        }


        /// <summary>
        /// This is the algorithm to find the Shortest Middle Snake (SMS).
        /// </summary>
        /// <param name="DataA">sequence A</param>
        /// <param name="LowerA">lower bound of the actual range in DataA</param>
        /// <param name="UpperA">upper bound of the actual range in DataA (exclusive)</param>
        /// <param name="DataB">sequence B</param>
        /// <param name="LowerB">lower bound of the actual range in DataB</param>
        /// <param name="UpperB">upper bound of the actual range in DataB (exclusive)</param>
        /// <returns>a MiddleSnakeData record containing x,y and u,v</returns>
        private static SMSRD SMS(DiffData DataA, int LowerA, int UpperA, DiffData DataB, int LowerB, int UpperB)
        {
            SMSRD ret;
            var MAX = DataA._length + DataB._length + 1;

            var DownK = LowerA - LowerB; // the k-line to start the forward search
            var UpK = UpperA - UpperB; // the k-line to start the reverse search

            var Delta = (UpperA - LowerA) - (UpperB - LowerB);
            var oddDelta = (Delta & 1) != 0;

            /// vector for the (0,0) to (x,y) search
            var DownVector = new int[2 * MAX + 2];

            /// vector for the (u,v) to (N,M) search
            var UpVector = new int[2 * MAX + 2];

            // The vectors in the publication accepts negative indexes. the vectors implemented here are 0-based
            // and are access using a specific offset: UpOffset UpVector and DownOffset for DownVektor
            var DownOffset = MAX - DownK;
            var UpOffset = MAX - UpK;

            var MaxD = ((UpperA - LowerA + UpperB - LowerB) / 2) + 1;

            // init vectors
            DownVector[DownOffset + DownK + 1] = LowerA;
            UpVector[UpOffset + UpK - 1] = UpperA;

            for (var D = 0; D <= MaxD; D++)
            {

                // Extend the forward path.
                for (var k = DownK - D; k <= DownK + D; k += 2)
                {
                    // Debug.Write(0, "SMS", "extend forward path " + k.ToString());

                    // find the only or better starting point
                    int x, y;
                    if (k == DownK - D)
                    {
                        x = DownVector[DownOffset + k + 1]; // down
                    }
                    else
                    {
                        x = DownVector[DownOffset + k - 1] + 1; // a step to the right
                        if ((k < DownK + D) && (DownVector[DownOffset + k + 1] >= x))
                            x = DownVector[DownOffset + k + 1]; // down
                    }
                    y = x - k;

                    // find the end of the furthest reaching forward D-path in diagonal k.
                    while ((x < UpperA) && (y < UpperB) && (DataA._data[x] == DataB._data[y]))
                    {
                        x++;
                        y++;
                    }
                    DownVector[DownOffset + k] = x;

                    // overlap ?
                    if (oddDelta && (UpK - D < k) && (k < UpK + D))
                    {
                        if (UpVector[UpOffset + k] <= DownVector[DownOffset + k])
                        {
                            ret._x = DownVector[DownOffset + k];
                            ret._y = DownVector[DownOffset + k] - k;
                            return (ret);
                        }
                    }

                }

                // Extend the reverse path.
                for (var k = UpK - D; k <= UpK + D; k += 2)
                {
                    // Debug.Write(0, "SMS", "extend reverse path " + k.ToString());

                    // find the only or better starting point
                    int x, y;
                    if (k == UpK + D)
                    {
                        x = UpVector[UpOffset + k - 1]; // up
                    }
                    else
                    {
                        x = UpVector[UpOffset + k + 1] - 1; // left
                        if ((k > UpK - D) && (UpVector[UpOffset + k - 1] < x))
                            x = UpVector[UpOffset + k - 1]; // up
                    } // if
                    y = x - k;

                    while ((x > LowerA) && (y > LowerB) && (DataA._data[x - 1] == DataB._data[y - 1]))
                    {
                        x--;
                        y--; // diagonal
                    }
                    UpVector[UpOffset + k] = x;

                    // overlap ?
                    if (!oddDelta && (DownK - D <= k) && (k <= DownK + D))
                    {
                        if (UpVector[UpOffset + k] <= DownVector[DownOffset + k])
                        {
                            ret._x = DownVector[DownOffset + k];
                            ret._y = DownVector[DownOffset + k] - k;
                            // ret.u = UpVector[UpOffset + k];     // 2002.09.20: no need for 2 points 
                            // ret.v = UpVector[UpOffset + k] - k;
                            return (ret);
                        }
                    }

                }

            }

            throw new ApplicationException("the algorithm should never come here.");
        }


        /// <summary>
        /// This is the divide-and-conquer implementation of the longes common-subsequence (LCS) 
        /// algorithm.
        /// The published algorithm passes recursively parts of the A and B sequences.
        /// To avoid copying these arrays the lower and upper bounds are passed while the sequences stay constant.
        /// </summary>
        /// <param name="DataA">sequence A</param>
        /// <param name="LowerA">lower bound of the actual range in DataA</param>
        /// <param name="UpperA">upper bound of the actual range in DataA (exclusive)</param>
        /// <param name="DataB">sequence B</param>
        /// <param name="LowerB">lower bound of the actual range in DataB</param>
        /// <param name="UpperB">upper bound of the actual range in DataB (exclusive)</param>
        private static void LCS(DiffData DataA, int LowerA, int UpperA, DiffData DataB, int LowerB, int UpperB)
        {
            // Fast walkthrough equal lines at the start
            while (LowerA < UpperA && LowerB < UpperB && DataA._data[LowerA] == DataB._data[LowerB])
            {
                LowerA++;
                LowerB++;
            }

            // Fast walkthrough equal lines at the end
            while (LowerA < UpperA && LowerB < UpperB && DataA._data[UpperA - 1] == DataB._data[UpperB - 1])
            {
                --UpperA;
                --UpperB;
            }

            if (LowerA == UpperA)
            {
                // mark as inserted lines.
                while (LowerB < UpperB)
                    DataB._modified[LowerB++] = true;

            }
            else if (LowerB == UpperB)
            {
                // mark as deleted lines.
                while (LowerA < UpperA)
                    DataA._modified[LowerA++] = true;

            }
            else
            {
                // Find the middle snakea and length of an optimal path for A and B
                SMSRD smsrd = SMS(DataA, LowerA, UpperA, DataB, LowerB, UpperB);

                // The path is from LowerX to (x,y) and (x,y) ot UpperX
                LCS(DataA, LowerA, smsrd._x, DataB, LowerB, smsrd._y);
                LCS(DataA, smsrd._x, UpperA, DataB, smsrd._y, UpperB);  // 2002.09.20: no need for 2 points 
            }
        }


        /// <summary>
        /// Scan the tables of which lines are inserted and deleted,
        /// producing an edit script in forward order.  
        /// </summary>
        /// dynamic array
        private static Item[] CreateDiffs(DiffData DataA, DiffData DataB)
        {
            var a = new ArrayList();
            Item aItem;
            Item[] result;

            int StartA, StartB;
            int LineA, LineB;

            LineA = 0;
            LineB = 0;
            while (LineA < DataA._length || LineB < DataB._length)
            {
                if ((LineA < DataA._length) && (!DataA._modified[LineA])
                    && (LineB < DataB._length) && (!DataB._modified[LineB]))
                {
                    // equal lines
                    LineA++;
                    LineB++;

                }
                else
                {
                    // maybe deleted and/or inserted lines
                    StartA = LineA;
                    StartB = LineB;

                    while (LineA < DataA._length && (LineB >= DataB._length || DataA._modified[LineA]))
                        LineA++;

                    while (LineB < DataB._length && (LineA >= DataA._length || DataB._modified[LineB]))
                        LineB++;

                    if ((StartA < LineA) || (StartB < LineB))
                    {
                        // store a new difference-item
                        aItem = new Item();
                        aItem.StartA = StartA;
                        aItem.StartB = StartB;
                        aItem.deletedA = LineA - StartA;
                        aItem.insertedB = LineB - StartB;
                        a.Add(aItem);
                    }
                }
            }

            result = new Item[a.Count];
            a.CopyTo(result);

            return (result);
        }

        /// <summary>
        /// Data on one input file being compared.  
        /// </summary>
        internal class DiffData
        {

            /// <summary>Number of elements (lines).</summary>
            internal int _length;

            /// <summary>Buffer of numbers that will be compared.</summary>
            internal int[] _data;

            /// <summary>
            /// Array of booleans that flag for modified data.
            /// This is the result of the diff.
            /// This means deletedA in the first Data or inserted in the second Data.
            /// </summary>
            internal bool[] _modified;

            /// <summary>
            /// Initialize the Diff-Data buffer.
            /// </summary>
            /// <param name="data">reference to the buffer</param>
            internal DiffData(int[] initData)
            {
                _data = initData;
                _length = initData.Length;
                _modified = new bool[_length + 2];
            }

        }
    }
}
