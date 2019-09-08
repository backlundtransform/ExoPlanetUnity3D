namespace System.Collections.Generic
{
    public static class Extensions
    {
        public static void AddIfNotNull(this List<string> list, string value, object item)
        {
            if (item != null && (double)item !=0.0)
            {
                list.Add(value);
            }
        }
    }
}