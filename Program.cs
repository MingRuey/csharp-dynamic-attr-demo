using System.Reflection;
using csharp_dynamic_attr_demo;
using Microsoft.CSharp.RuntimeBinder;

class Model
{
    public int AppleCount { get; set; } = 0;
    public int BirdCount { get; set; } = 0;
    public int CatCount { get; set; } = 0;
}

static class Program
{
    public static void Main(string[] args)
    {
        var lstModelStrings = new List<string> { "Apple", "Bird", "Cat", "Dog" };
        var myModel = new Model();

        Console.WriteLine("Before:");
        Console.WriteLine(myModel.AppleCount);
        Console.WriteLine(myModel.BirdCount);
        Console.WriteLine(myModel.CatCount);

        foreach (var s in lstModelStrings)
        {
            // <<DANGEROUS>>
            // No compile time checking!
            // Avoid using this, but if we do, better to handled with try-catch
            try
            {
                myModel.GetType().InvokeMember(
                    s + "Count",
                    BindingFlags.Public | BindingFlags.SetProperty | BindingFlags.Instance,
                    Type.DefaultBinder, myModel, new object[] { 10 });
            }
            catch (MissingMethodException)
            {
                Console.WriteLine($"Property {s + "Count"} not found!");
            }
        }

        Console.WriteLine("After:");
        Console.WriteLine(myModel.AppleCount);
        Console.WriteLine(myModel.BirdCount);
        Console.WriteLine(myModel.CatCount);

        // WE SHOULD NOT DO THIS!!
        // Make a property with Dictionary, etc. is much better solution because it's simple
        dynamic myDynamicModel = DynamicExtend.ToDynamic(myModel) ??
                                   throw new Exception("Failed to create dynamic model");
        myDynamicModel.DogCount = 20;
        Console.WriteLine($"my_dynamic_model is not a type of Model, but {myDynamicModel.GetType()}");
        Console.WriteLine(myDynamicModel.DogCount);

        // Access non-existing property will still pass compiling!
        try
        {
            Console.WriteLine(myDynamicModel.FishCount);
        }
        catch (RuntimeBinderException)
        {
            Console.WriteLine("Property 'FishCount' doesn't exist!");
        }
    }
}