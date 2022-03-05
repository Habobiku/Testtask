using System;
using System.Collections.Generic;
using System.Reflection;
using Amazon.DynamoDBv2.Model;
using TestTask.DataBase;

namespace TestTask.Extentions
{
    public static class Extension
    {
        public static T ToClass<T>(this Dictionary<string, AttributeValue> dict)
        {
            var type = typeof(T);
            var obj = Activator.CreateInstance(type);
            foreach (var kv in dict)
            {
                var property = type.GetProperty(kv.Key);
                if (property != null)
                {
                    if (!string.IsNullOrEmpty(kv.Value.S))
                    { 

                        property.SetValue(obj, kv.Value.S);

                    }
                    else if (!string.IsNullOrEmpty(kv.Value.N))
                    {
                        property.SetValue(obj, int.Parse(kv.Value.N));
                    }
                    else if (kv.Value.SS != null)
                    {
                        property.SetValue(obj, kv.Value.SS);
                    }


                }

            }
            return (T)obj;
        }
        public static Item Map(Dictionary<string, AttributeValue> res)
        {
            var result = res.ToClass<Item>();
            return result;
        }
    }
}
