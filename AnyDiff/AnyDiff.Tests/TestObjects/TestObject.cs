using System;

namespace AnyDiff.Tests.TestObjects
{
    public class TestObject
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
    }
}
