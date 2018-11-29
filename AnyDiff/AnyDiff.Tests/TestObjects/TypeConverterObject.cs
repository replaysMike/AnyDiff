using System.ComponentModel;

namespace AnyDiff.Tests.TestObjects
{
    public class TypeConverterObject
    {
        /// <summary>
        /// Convert a formatted string as a TimeSpan
        /// </summary>
        [TypeConverter(typeof(TimeSpanConverter))]
        public string StartTime { get; set; }

        public int Id { get; private set; }

        public TypeConverterObject(int id)
        {
            Id = id;
        }
    }
}
