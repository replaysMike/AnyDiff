﻿using AnyDiff.Tests.TestObjects;
using NUnit.Framework;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AnyDiff.Tests
{
    [TestFixture]
    public class CollectionTests
    {
        [Test]
        public void ShouldDetect_Array_Differences()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { IntArray = new int[] { 0, 0, 5, 0, 0 } };
            var obj2 = new TestObject { IntArray = new int[] { 0, 0, 2, 0, 0 } };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(1, diff.Count);
            Assert.AreEqual(-3, diff.First().Delta);
            Assert.AreEqual(2, diff.First().ArrayIndex);
        }

        [Test]
        public void ShouldDetect_Array_NoDifferences()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { IntArray = new int[] { 0, 0, 5, 0, 0 } };
            var obj2 = new TestObject { IntArray = new int[] { 0, 0, 5, 0, 0 } };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(0, diff.Count);
        }

        [Test]
        public void ShouldDetect_List_Differences()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { IntCollection = new List<int> { 0, 0, 5, 0, 0 } };
            var obj2 = new TestObject { IntCollection = new List<int> { 0, 0, 2, 0, 0 } };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(1, diff.Count);
            Assert.AreEqual(-3, diff.First().Delta);
            Assert.AreEqual(2, diff.First().ArrayIndex);
        }

        [Test]
        public void ShouldDetect_List_CountDifferences()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { IntCollection = new List<int> { 0, 0, 5, 0, 0, 0 } };
            var obj2 = new TestObject { IntCollection = new List<int> { 0, 0, 5, 0, 0 } };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(1, diff.Count);
        }

        [Test]
        public void ShouldDetect_List_CountDifferences2()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { IntCollection = new List<int> { 0, 0, 5, 0, 0 } };
            var obj2 = new TestObject { IntCollection = new List<int> { 0, 0, 5, 0, 0, 0 } };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(1, diff.Count);
        }

        [Test]
        public void ShouldBeEqual_EmptyList_Null()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { IntCollection = new List<int> { } };
            var obj2 = new TestObject { IntCollection = null };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2, ComparisonOptions.All | ComparisonOptions.TreatEmptyListAndNullTheSame);
            Assert.AreEqual(0, diff.Count);
        }

        [Test]
        public void ShouldNotBeEqual_EmptyList_Null()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { IntCollection = new List<int> { } };
            var obj2 = new TestObject { IntCollection = null };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2, ComparisonOptions.All);
            Assert.AreEqual(1, diff.Count);
        }

        [Test]
        public void ShouldDetect_List_NoDifferences()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { IntCollection = new List<int> { 0, 0, 5, 0, 0 } };
            var obj2 = new TestObject { IntCollection = new List<int> { 0, 0, 5, 0, 0 } };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(0, diff.Count);
        }

        [Test]
        public void ShouldDetect_Dictionary_Differences()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { IntDictionary = new Dictionary<int, string> { { 0, "1" }, { 1, "2" }, { 2, "3" }, { 3, "4" }, { 4, "5" } } };
            var obj2 = new TestObject { IntDictionary = new Dictionary<int, string> { { 0, "1" }, { 1, "2" }, { 2, "2" }, { 3, "4" }, { 4, "5" } } };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(1, diff.Count);
        }

        [Test]
        public void ShouldDetect_Dictionary_NoDifferences()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { IntDictionary = new Dictionary<int, string> { { 0, "1" }, { 1, "2" }, { 2, "3" }, { 3, "4" }, { 4, "5" } } };
            var obj2 = new TestObject { IntDictionary = new Dictionary<int, string> { { 0, "1" }, { 1, "2" }, { 2, "3" }, { 3, "4" }, { 4, "5" } } };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(0, diff.Count);
        }

        [Test]
        public void ShouldDetect_Dictionary_CountDifferences()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { IntDictionary = new Dictionary<int, string> { { 0, "1" }, { 1, "2" }, { 2, "3" }, { 3, "4" }, { 4, "5" }, {5, "6" } } };
            var obj2 = new TestObject { IntDictionary = new Dictionary<int, string> { { 0, "1" }, { 1, "2" }, { 2, "3" }, { 3, "4" }, { 4, "5" } } };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(1, diff.Count);
        }

        [Test]
        public void ShouldDetect_Dictionary_CountDifferences2()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { IntDictionary = new Dictionary<int, string> { { 0, "1" }, { 1, "2" }, { 2, "3" }, { 3, "4" }, { 4, "5" } } };
            var obj2 = new TestObject { IntDictionary = new Dictionary<int, string> { { 0, "1" }, { 1, "2" }, { 2, "3" }, { 3, "4" }, { 4, "5" }, { 5, "6" } } };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2);
            Assert.AreEqual(1, diff.Count);
        }

        [Test]
        public void ShouldDetect_Hashtable_NoDifferences()
        {
            var provider = new DiffProvider();
            var obj1 = new System.Collections.Hashtable();
            obj1.Add(1, "Test");
            obj1.Add(2, "Test");
            var obj2 = new System.Collections.Hashtable();
            obj2.Add(1, "Test");
            obj2.Add(2, "Test");

            var diff = provider.ComputeDiff(obj1, obj2);
            Assert.AreEqual(0, diff.Count);
        }

        [Test]
        public void ShouldDetect_Hashtable_CountDifferences()
        {
            var provider = new DiffProvider();
            var obj1 = new System.Collections.Hashtable();
            obj1.Add(1, "Test");
            obj1.Add(2, "Test");
            var obj2 = new System.Collections.Hashtable();
            obj2.Add(1, "Test");
            obj2.Add(2, "Test");
            obj2.Add(3, "Test");

            var diff = provider.ComputeDiff(obj1, obj2);
            Assert.Greater(diff.Count, 0);
        }

        [Test]
        public void ShouldDetect_Hashtable_CountDifferences2()
        {
            var provider = new DiffProvider();
            var obj1 = new System.Collections.Hashtable();
            obj1.Add(1, "Test");
            obj1.Add(2, "Test");
            obj1.Add(3, "Test");
            var obj2 = new System.Collections.Hashtable();
            obj2.Add(1, "Test");
            obj2.Add(2, "Test");

            var diff = provider.ComputeDiff(obj1, obj2);
            Assert.Greater(diff.Count, 0);
        }

        [Test]
        public void ShouldDetect_Hashtable_Differences()
        {
            var provider = new DiffProvider();
            var obj1 = new System.Collections.Hashtable();
            obj1.Add(1, "Test");
            obj1.Add(3, "Test");
            var obj2 = new System.Collections.Hashtable();
            obj2.Add(1, "Test");
            obj2.Add(2, "Test");

            var diff = provider.ComputeDiff(obj1, obj2, ComparisonOptions.All, "._keys", "._buckets");
            Assert.Greater(diff.Count, 0);
        }

        [Test]
        public void ShouldDetect_Array_OutOfOrder_NoDifferences()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { IntArray = new int[] { 1, 2, 3, 4, 5 } };
            var obj2 = new TestObject { IntArray = new int[] { 2, 5, 4, 3, 1 } };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2, ComparisonOptions.All | ComparisonOptions.AllowCollectionsToBeOutOfOrder);
            Assert.AreEqual(0, diff.Count);
        }

        [Test]
        public void ShouldDetect_Array_OutOfOrder_LeftDifferences()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { IntArray = new int[] { 1, 2, 3, 4, 6 } };
            var obj2 = new TestObject { IntArray = new int[] { 2, 5, 4, 3, 1 } };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2, ComparisonOptions.All | ComparisonOptions.AllowCollectionsToBeOutOfOrder);
            Assert.AreEqual(2, diff.Count);
        }

        [Test]
        public void ShouldDetect_Array_OutOfOrder_RightDifferences()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { IntArray = new int[] { 1, 2, 3, 4, 5 } };
            var obj2 = new TestObject { IntArray = new int[] { 2, 5, 4, 3, 6 } };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2, ComparisonOptions.All | ComparisonOptions.AllowCollectionsToBeOutOfOrder);
            Assert.AreEqual(2, diff.Count);
        }

        [Test]
        public void ShouldDetect_Array_OutOfOrder_LeftAdditionalElements()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { IntArray = new int[] { 1, 2, 3, 4, 5, 9 } };
            var obj2 = new TestObject { IntArray = new int[] { 2, 5, 4, 3, 1 } };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2, ComparisonOptions.All | ComparisonOptions.AllowCollectionsToBeOutOfOrder);
            Assert.AreEqual(1, diff.Count);
        }

        [Test]
        public void ShouldDetect_Array_OutOfOrder_RightAdditionalElements()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { IntArray = new int[] { 1, 2, 3, 4, 5 } };
            var obj2 = new TestObject { IntArray = new int[] { 2, 5, 4, 3, 1, 8 } };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2, ComparisonOptions.All | ComparisonOptions.AllowCollectionsToBeOutOfOrder);
            Assert.AreEqual(1, diff.Count);
        }

        [Test]
        public void ShouldDetect_Array_OutOfOrder_DifferentCount()
        {
            var provider = new DiffProvider();

            var obj1 = new TestObject { IntArray = new int[] { 1, 2, 3, 4, 5 } };
            var obj2 = new TestObject { IntArray = new int[] { 2, 5, 4, 3, 1, 3 } };
            var diff = provider.ComputeDiff<TestObject>(obj1, obj2, ComparisonOptions.All | ComparisonOptions.AllowCollectionsToBeOutOfOrder);
            Assert.AreEqual(1, diff.Count);
        }

        [Test]
        public void ShouldDetect_ReadOnlyCollection_NoDifferences()
        {
            var provider = new DiffProvider();
            var list = new List<EqualsObject> {
                new EqualsObject(1, "Testing 1"),
                new EqualsObject(2, "Testing 2"),
            };
            var list2 = new List<EqualsObject> {
                new EqualsObject(1, "Testing 1"),
                new EqualsObject(2, "Testing 2"),
            };
            var obj1 = new ReadOnlyCollectionObject(list);
            var obj2 = new ReadOnlyCollectionObject(list2);
            var diff = provider.ComputeDiff<ReadOnlyCollectionObject>(obj1, obj2, ComparisonOptions.All | ComparisonOptions.IncludeList | ComparisonOptions.AllowCollectionsToBeOutOfOrder | ComparisonOptions.AllowEqualsOverride,
                    x => x.Collection);

            Assert.AreEqual(0, diff.Count);
        }

        [Test]
        public void ShouldDetect_ReadOnlyCollection_CustomEquals_AreDifferent()
        {
            var provider = new DiffProvider();
            var list = new List<EqualsObject> {
                new EqualsObject(1, "Testing 1"),
                new EqualsObject(2, "Testing 2"),
            };
            var list2 = new List<EqualsObject> {
                new EqualsObject(1, "Testing 1"),
                new EqualsObject(3, "This line is entirely different"),
            };
            var obj1 = new ReadOnlyCollectionObject(list);
            var obj2 = new ReadOnlyCollectionObject(list2);
            var diff = provider.ComputeDiff<ReadOnlyCollectionObject>(obj1, obj2, ComparisonOptions.All | ComparisonOptions.IncludeList | ComparisonOptions.AllowCollectionsToBeOutOfOrder | ComparisonOptions.AllowEqualsOverride,
                    x => x.Collection);

            Assert.Greater(diff.Count, 0);
        }

        [Test]
        public void ShouldDetect_ReadOnlyCollection_AreDifferent()
        {
            var provider = new DiffProvider();
            var list = new List<EqualsObject> {
                new EqualsObject(1, "Testing 1"),
                new EqualsObject(2, "Testing 2"),
            };
            var list2 = new List<EqualsObject> {
                new EqualsObject(1, "Testing 1"),
                new EqualsObject(3, "Testing 3"),
            };
            var obj1 = new ReadOnlyCollectionObject(list);
            var obj2 = new ReadOnlyCollectionObject(list2);
            var diff = provider.ComputeDiff<ReadOnlyCollectionObject>(obj1, obj2, ComparisonOptions.All | ComparisonOptions.IncludeList | ComparisonOptions.AllowEqualsOverride,
                    x => x.Collection);

            Assert.Greater(diff.Count, 0);
        }

        [Test]
        public void ShouldDetect_ReadOnlyCollection_OutOfOrder_AreDifferent()
        {
            var provider = new DiffProvider();
            var list = new List<EqualsObject> {
                new EqualsObject(1, "Testing 1"),
                new EqualsObject(2, "Testing 2"),
            };
            var list2 = new List<EqualsObject> {
                new EqualsObject(1, "Testing 1"),
                new EqualsObject(3, "Testing 3"),
            };
            var obj1 = new ReadOnlyCollectionObject(list);
            var obj2 = new ReadOnlyCollectionObject(list2);
            var diff = provider.ComputeDiff<ReadOnlyCollectionObject>(obj1, obj2, ComparisonOptions.All | ComparisonOptions.IncludeList | ComparisonOptions.AllowCollectionsToBeOutOfOrder | ComparisonOptions.AllowEqualsOverride,
                    x => x.Collection);

            Assert.Greater(diff.Count, 0);
        }
    }
}
