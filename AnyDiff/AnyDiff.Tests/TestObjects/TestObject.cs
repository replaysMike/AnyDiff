using System;
using System.Linq;

namespace AnyDiff.Tests.TestObjects
{
    public class TestObject : IEquatable<TestObject>
    {
        public int IntValue { get; set; }
        public int? NullableIntValue { get; set; }
        public decimal DecimalValue { get; set; }
        public decimal? NullableDecimalValue { get; set; }
        public string StringValue { get; set; }
        public DateTime DateValue { get; set; }
        public DateTime? NullableDateValue { get; set; }
        public TimeSpan TimeSpanValue { get; set; }
        public TimeSpan? NullableTimeSpanValue { get; set; }
        public int[] Collection { get; set; }
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
                && (Collection == null && other.Collection == null) || Collection.SequenceEqual(other.Collection)
                && GuidValue == other.GuidValue
                && (NullableGuidValue == null && other.NullableGuidValue == null) || NullableGuidValue.Equals(other.NullableGuidValue)
                ;
        }
    }
}
