namespace System.Collections.Generic
{
    public static class Extensions
    {
        public static void AddIfNotNull(this List<string> list, string value, decimal? item)
        {
            if (item != null && item != 0)
            {
                list.Add(value);
            }
        }
        public static double DecimalRound(this decimal? value)
        {
            if (value != null)
            {
               return Math.Round((double)value, 2);
            }
            return 0;
        }
    }
}