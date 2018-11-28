# AnyDiff

A CSharp library that allows you to diff two objects and get a list of the differences back.

## Description

AnyDiff works with complex objects of any type, and is great for performing changeset tracking, auditing, or anything else that might require comparing differences between complex objects. Great when combined with AnyClone, as it lets you take a snapshot of the current state of an object and then compare it in the future for changes to it.

It can even do this for objects that are of different types, though the results may vary depending on how different they are.

## Installation
Install AnyDiff from the Package Manager Console:
```
PM> Install-Package AnyDiff
```

## Usage

Comparing two objects with no differences:
```csharp
using AnyDiff;

var object1 = new MyComplexObject(1, "A string");
var object2 = new MyComplexObject(1, "A string");
var diff = AnyDiff.Diff(object1, object2);
Assert.AreEqual(diff.Count, 0);
```

Alternate extension syntax is also available (we will use this in further examples):
```csharp
using AnyDiff.Extensions;

var object1 = new MyComplexObject(1, "A string");
var object2 = new MyComplexObject(1, "A string");
var diff = object1.Diff(object2);
Assert.AreEqual(diff.Count, 0);
```

Comparing two objects with a single expected change:
```csharp
var object1 = new MyComplexObject(1, "A string");
var object2 = new MyComplexObject(1, "A different string");
var diff = object1.Diff(object2);
Assert.AreEqual(diff.Count, 1);
```

Comparing objects using custom Type Converters for proper delta detection:
```csharp
public class MyComplexObject
{
  public int Id { get; private set; }

  /// <summary>
  /// Convert a formatted string as a TimeSpan
  /// </summary>
  [TypeConverter(typeof(TimeSpanConverter))]
  public string StartTime { get; set; }

  public TypeConverterObject(int id, string startTime)
  {
    Id = id;
    StartTime = startTime;
  }
}

var object1 = new MyComplexObject(1, "04:00:00");
var object2 = new MyComplexObject(1, "04:05:00");
var diff = object1.Diff(object2);
Assert.AreEqual(diff.Count, 1);
Assert.AreEqual(TimeSpan.FromMinutes(5), diff.First().Delta); // difference of 5 minutes
```

### Ignoring Properties

Anydiff will ignore fields and properties decorated using attributes: `[IgnoreDataMember]`, `[NonSerialized]`, and `[JsonIgnore]`.
In addition, you can specify properties to ignore using expression syntax:

```csharp
var object1 = new MyComplexObject(1, "A string", true);
var object2 = new MyComplexObject(2, "A different string", true);
var diff = object1.Diff(object2, x => x.Id, x => x.Name);
Assert.AreEqual(diff.Count, 0);
```

### Analyzing results

Viewing the results of a diff:
```csharp
var diff = object1.Diff(object2);

foreach(var difference in diff)
{
  Console.Write($"Index: {difference.ArrayIndex}"); // when array elements differ in value
  Console.Write($"Delta: {difference.Delta}"); // when numbers, Dates, Timespans, strings differ in value
  Console.Write($"Left: {difference.LeftValue}"); // the left value being compared
  Console.Write($"Right: {difference.RightValue}"); // the right value being compared
  Console.Write($"Property name: {difference.Property}"); // the name of the field/property
  Console.Write($"Property type: {difference.PropertyType}"); // the type of the field/property
}

```

### Scenarios supported

- [x] Circular references
- [x] Using TypeConverters to understand data types
- [x] Deltas on strings, DateTimes, TimeSpans, numeric types
- [x] Comparing arrays, collections, dictionaries
- [x] Complex objects, deep type inspection
- [x] Entity Framework objects
- [x] IEquatable support

### Using with other libraries
Comparing the difference between the same object at different states, using [AnyClone](https://github.com/replaysMike/AnyClone)
```csharp
using AnyDiff.Extensions;
using AnyClone;

var object1 = new MyComplexObject(1, "A string");
var object1Snapshot = object1.Clone();

var diff = object1.Diff(object1Snapshot);
Assert.AreEqual(diff.Count, 0);

// change something anywhere in the object tree
object1.Name = "A different string";

diff = object1.Diff(object1Snapshot);
Assert.AreEqual(diff.Count, 1);
```
