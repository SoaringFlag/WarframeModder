using System.Collections.Generic;

namespace WarframeModder.Extensions
{
    public static class ListExtensions
    {
        public static void AddUnique<T>(this List<T> list, T item)
        {
            if (list.Contains(item))
            {
                return;
            }

            list.Add(item);
        }
    }
}
