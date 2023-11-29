using System.ComponentModel;
using System.Dynamic;

namespace csharp_dynamic_attr_demo;

public static class DynamicExtend
{
    public static dynamic? ToDynamic(this object value)
    {
        IDictionary<string, object> expando = new ExpandoObject();
        foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(value.GetType()))
            expando.Add(property.Name, property.GetValue(value) ?? "Not-Assigned");
        return expando as ExpandoObject;
    }
}