# AnyDiff

A CSharp library that allows you to diff two objects and get a list of the differences back.

## Description

AnyDiff works with complex objects of any type, and is great for performing changeset tracking, auditing, or anything else that might require comparing differences between complex objects. Great when combined with AnyClone, as it lets you take a snapshot of the current state of an object and then compare it in the future for changes to it.

It can even do this for objects that are of different types, though the results may vary depending on how different they are.

## Usage

Comparing two objects with no differences:
```
var object1 = new MyComplexObject(1, "A string");
var object2 = new MyComplexObject(1, "A string");
var diff = AnyDiff.Diff(object1, object2);
Assert.AreEqual(diff.Count, 0);
```

Comparing two objects with a single expected change:
```
var object1 = new MyComplexObject(1, "A string");
var object2 = new MyComplexObject(1, "A different string");
var diff = AnyDiff.Diff(object1, object2);
Assert.AreEqual(diff.Count, 1);
```

Viewing the results of a diff:
```
var diff = AnyDiff.Diff(object1, object2);
var diff = AnyDiff.Diff(object1, object2);

foreach(var difference in diff)
{
  Console.Write($"Index: {difference.ArrayIndex}"); // when array elements differ in value
  Console.Write($"Delta: {difference.Delta}"); // when numbers, strings differ in value
  Console.Write($"Left: {difference.LeftValue}"); // the left value being compared
  Console.Write($"Right: {difference.RightValue}"); // the right value being compared
  Console.Write($"Property name: {difference.Property}"); // the name of the field/property
  Console.Write($"Property type: {difference.PropertyType}"); // the type of the field/property
}

```

Comparing the difference between the same object at different states, using [AnyClone](https://github.com/replaysMike/AnyClone)
```
// using AnyClone;
var object1 = new MyComplexObject(1, "A string");
var object1Snapshot = object1.Clone();

var diff = AnyDiff.Diff(object1, object1Snapshot);
Assert.AreEqual(diff.Count, 0);

// change something anywhere in the object tree
object1.Name = "A different string";

diff = AnyDiff.Diff(object1, object1Snapshot);
Assert.AreEqual(diff.Count, 1);
```
