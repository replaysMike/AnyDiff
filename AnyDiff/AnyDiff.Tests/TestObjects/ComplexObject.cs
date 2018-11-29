using System;
using System.Collections.Generic;

namespace AnyDiff.Tests.TestObjects
{
    public class ComplexObject : BaseObject
    {
        public readonly int PublicId;
        private int _idReference;
        private ICollection<int> _intCollection;

        public TestEnum TestEnum { get; set; }
        public string Name { get; }
        public DateTime Date { get; }
        public ComplexObject AnotherObject { get; set; }

        public ComplexObject(int id, int publicId, DateTime date, string name) : base(id)
        {
            PublicId = publicId;
            Name = name;
            _idReference = id;
            _intCollection = new List<int>();
        }

        public void AddInt(int value)
        {
            _intCollection.Add(value);
        }
    }

    public class BaseObject
    {
        public int Id { get; }
        public BaseObject(int id)
        {
            Id = id;
        }
    }

    public enum TestEnum
    {
        Value1 = 1,
        Value2,
        Value3,
        Value4,
        Value5
    }
}
