using System;
using System.Collections.Generic;
using System.Text;

namespace AnyDiff.Tests.TestObjects
{
    public class ComplexObjectWithListChildren
    {
        public ICollection<ComplexChild> Children { get; set; } = new List<ComplexChild>();
        public int Id { get; set; }
        public string Name { get; set; }
        public BasicChild BasicChild { get; set; }

        public ComplexObjectWithListChildren(int id, string name, ICollection<ComplexChild> children, BasicChild basicChild)
        {
            Id = id;
            Name = name;
            Children = children;
            BasicChild = basicChild;
        }
    }

    public class BasicChild
    {
        public int BasicChildId { get; set; }
        public string BasicChildName { get; set; }
        public ICollection<BasicChild> Children { get; set; } = new List<BasicChild>();

        public BasicChild(int childId, string childName)
        {
            BasicChildId = childId;
            BasicChildName = childName;
        }
    }

    public class ComplexChild
    {
        public int ComplexChildId { get; set; }
        public string ChildName { get; set; }
        public BasicChild BasicChild { get; set; }

        public ComplexChild(int childId, string childName, BasicChild basicChild)
        {
            ComplexChildId = childId;
            ChildName = childName;
            BasicChild = basicChild;
        }
    }
}
