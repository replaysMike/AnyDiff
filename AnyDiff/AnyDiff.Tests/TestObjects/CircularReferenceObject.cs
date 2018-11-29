using System;
using System.Collections.Generic;
using System.Text;

namespace AnyDiff.Tests.TestObjects
{
    public class CircularReferenceObject
    {
        public int Id { get; }
        public CircularReferenceObject Parent { get; set; }
        public CircularReferenceObject(int id)
        {
            Id = id;
        }
    }
}
