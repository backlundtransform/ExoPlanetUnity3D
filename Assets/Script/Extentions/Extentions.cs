using System.Collections.Generic;

namespace System.Collections.Generic
{
    public static class Extensions
    {
        public static void AddIfNotNull(this List<string> list,string value, object item)
        {
            if (item != null) {
                list.Add(value);
            }
        }
    }
}
