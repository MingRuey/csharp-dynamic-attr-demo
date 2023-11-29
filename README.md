# csharp-dynamic-attr-demo
Project for demonstrating C# dynamic properties.
Spoiler: the keyword is [Reflection](https://learn.microsoft.com/en-us/dotnet/csharp/advanced-topics/reflection-and-attributes/)

## Context

Given a list of string
```c#
var lstModelStrings = new List<string> { "Apple", "Bird", "Cat" };
```

Sometimes we would like to modify a class based on the string values:
```c#
class Model
{
    public int AppleCount { get; set; }
    public int BirdCount { get; set; }
    public int CatCount { get; set; }
}

var lstModelStrings = new List<string> { "Apple", "Bird", "Cat" };
var my_model = new Model();
foreach (var s in lstModelStrings)
{
    my_model.{s}Count = 10; // how to do this?
}
```

or add properties dynamically at runtime of a class:
```c#
var new_animal = "Dog"
my_model.{new_animal}Count = 5 // i.e., my_model.DogCount = 5, how to do this?
```
spoiler: it's not doable without changing the class type to dynamic object.

