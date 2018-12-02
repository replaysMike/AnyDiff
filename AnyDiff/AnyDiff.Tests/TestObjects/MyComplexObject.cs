namespace AnyDiff.Tests.TestObjects
{
    public class MyComplexObject
    {
        public int Id { get; }
        public string Name { get; }
        public bool IsEnabled { get; }
        public Location Location { get; }

        public MyComplexObject(int id, string name, bool isEnabled)
        {
            Id = id;
            Name = name;
            IsEnabled = isEnabled;
        }

        public MyComplexObject(int id, string name, Location location)
        {
            Id = id;
            Name = name;
            Location = location;
        }
    }

    public class Location
    {
        public double Latitude { get; }
        public double Longitude { get; }
        public Location(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
