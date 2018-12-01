using System;
using System.Collections.Generic;
using System.Linq;

namespace AnyDiff.Tests.TestObjects
{
    public class TestObject : IEquatable<TestObject>
    {
        public bool BoolValue { get; set; }
        public bool? NullableBoolValue { get; set; }
        public byte ByteValue { get; set; }
        public byte? NullableByteValue { get; set; }
        public sbyte SByteValue { get; set; }
        public sbyte? NullableSByteValue { get; set; }
        public short ShortValue { get; set; }
        public short? NullableShortValue { get; set; }
        public ushort UShortValue { get; set; }
        public ushort? NullableUShortValue { get; set; }
        public int IntValue { get; set; }
        public int? NullableIntValue { get; set; }
        public uint UIntValue { get; set; }
        public uint? NullableUIntValue { get; set; }
        public long LongValue { get; set; }
        public long? NullableLongValue { get; set; }
        public ulong ULongValue { get; set; }
        public ulong? NullableULongValue { get; set; }
        public float FloatValue { get; set; }
        public float? NullableFloatValue { get; set; }
        public double DoubleValue { get; set; }
        public double? NullableDoubleValue { get; set; }
        public decimal DecimalValue { get; set; }
        public decimal? NullableDecimalValue { get; set; }
        public string StringValue { get; set; }
        public DateTime DateValue { get; set; }
        public DateTime? NullableDateValue { get; set; }
        public TimeSpan TimeSpanValue { get; set; }
        public TimeSpan? NullableTimeSpanValue { get; set; }
        public int[] IntArray { get; set; }
        public ICollection<int> IntCollection { get; set; }
        public IDictionary<int, string> IntDictionary { get; set; }
        public Guid GuidValue { get; set; }
        public Guid? NullableGuidValue { get; set; }

        public override bool Equals(object obj)
        {
            var basicObject = (TestObject)obj;
            return Equals(basicObject);
        }

        public bool Equals(TestObject other)
        {
            return IntValue == other.IntValue
                && (NullableIntValue == null && other.NullableIntValue == null) || NullableIntValue.Equals(other.NullableIntValue)
                && DecimalValue == other.DecimalValue
                && (NullableDecimalValue == null && other.NullableDecimalValue == null) || NullableDecimalValue.Equals(other.NullableDecimalValue)
                && (StringValue == null && other.StringValue == null) || StringValue.Equals(other.StringValue)
                && DateValue == other.DateValue
                && (NullableDateValue == null && other.NullableDateValue == null) || NullableDateValue.Equals(other.NullableDateValue)
                && TimeSpanValue == other.TimeSpanValue
                && (NullableDateValue == null && other.NullableDateValue == null) || NullableDateValue.Equals(other.NullableDateValue)
                && (IntArray == null && other.IntArray == null) || IntArray.SequenceEqual(other.IntArray)
                && (IntCollection == null && other.IntCollection == null) || IntCollection.SequenceEqual(other.IntCollection)
                && GuidValue == other.GuidValue
                && (NullableGuidValue == null && other.NullableGuidValue == null) || NullableGuidValue.Equals(other.NullableGuidValue)
                ;
        }
    }
}
