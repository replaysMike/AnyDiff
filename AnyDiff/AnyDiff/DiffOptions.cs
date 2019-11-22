using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AnyDiff
{
    public class DiffOptions
    {
        /// <summary>
        /// The list of attributes (or names of attributes) to use when ignoring fields/properties
        /// </summary>
        internal static readonly ICollection<object> _defaultIgnoreAttributes = new List<object> {
            typeof(IgnoreDataMemberAttribute),
            typeof(NonSerializedAttribute),
            "JsonIgnoreAttribute",
        };

        /// <summary>
        /// An list of custom attributes that will be treated as ignore attributes
        /// </summary>
        public ICollection<object> AttributeIgnoreList { get; set; } = _defaultIgnoreAttributes;

        public DiffOptions()
        {
        }

        /// <summary>
        /// Create Diff Options
        /// </summary>
        /// <param name="attributeIgnoreList">A list of custom attribute types or attribute names to ignore. Example: new List<object> { typeof(IgnoreDataMemberAttribute), "JsonIgnoreAttribute" }</param>
        public DiffOptions(ICollection<object> attributeIgnoreList)
        {
            AttributeIgnoreList = attributeIgnoreList;
        }

        /// <summary>
        /// Default DiffOptions
        /// </summary>
        public static DiffOptions Default
        {
            get {
                return new DiffOptions();
            }
        }
    }
}
