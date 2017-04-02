using System;
using System.Collections.Generic;

namespace Maxxor.PCL.Extensions
{
    public static class ListExtensions
    {
        public static List<Type> GetTypes(this List<object> objects)
        {
            var types = new List<Type>();
            foreach (var obj in objects)
            {
                types.Add(obj.GetType());
            }
            return types;
        }
    }
}