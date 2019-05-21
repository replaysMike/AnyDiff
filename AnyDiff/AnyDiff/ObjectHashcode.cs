using System;

namespace AnyDiff
{
    /// <summary>
    /// Tracks an object reference by hashcode and type
    /// </summary>
    public struct ObjectHashcode
    {
        /// <summary>
        /// The object's hashcode
        /// </summary>
        public int Hashcode { get; set; }

        /// <summary>
        /// The object's type
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// Tracks an object reference by hashcode and type
        /// </summary>
        /// <param name="hashcode"></param>
        /// <param name="type"></param>
        public ObjectHashcode(int hashcode, Type type)
        {
            Hashcode = hashcode;
            Type = type;
        }

        public override int GetHashCode()
        {
            var computedHashcode = 23;
            computedHashcode = computedHashcode * 31 + Hashcode;
            computedHashcode = computedHashcode * 31 + Type.GetHashCode();
            return computedHashcode;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var typedObj = (ObjectHashcode)obj;
            return typedObj.Hashcode.Equals(Hashcode) && typedObj.Type.Equals(Type);
        }
    }
}
