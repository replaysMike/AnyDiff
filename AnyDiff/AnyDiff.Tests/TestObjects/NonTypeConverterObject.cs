namespace AnyDiff.Tests.TestObjects
{
    public class NonTypeConverterObject
    {
        /// <summary>
        /// Formatted string as a TimeSpan
        /// </summary>
        public string StartTime { get; set; }

        public int Id { get; private set; }

        public NonTypeConverterObject(int id)
        {
            Id = id;
        }
    }
}
