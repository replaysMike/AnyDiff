using System.ComponentModel;

namespace AnyDiff.Tests.TestObjects
{
    public class CustomAttributeObject
    {
        public int Id { get; set; }

        [Description("This property will be ignored")]
        public string Name { get; set; }

        public CustomAttributeObject(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
