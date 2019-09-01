using Assets.Script.Models;
using System.Collections.Generic;

namespace System.Collections.Generic
{
    public static class Extensions
    {
        public static void AddIfNotNull(this Dictionary<string, string> list, string key,string value, object item)
        {
            if (item != null) {
                list.Add(key,value);
            }
        }
    }
}
