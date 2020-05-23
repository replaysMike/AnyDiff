namespace AnyDiff.Tests.TestObjects
{
    public class Person
    {
        public int Id { get; set; }
        public Author Author { get; set; }
        public string Tagline { get; set; }
        public Genders Gender { get; set; }
    }

    public class Author
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }
    }

    public class Address
    {
        public string City { get; set; }
        public string Country { get; set; }
    }

    public enum Genders
    {
        Male,
        Female,
        Other
    }
}
