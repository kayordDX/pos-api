namespace Kayord.Pos.Common.Printer;

public static class Util
{
    public static bool AddRange<T>(this List<T> list, params object[] items)
    {
        bool ignoredItems = false;
        foreach (var item in items)
        {
            if (item is T itemT)
            {
                list.Add(itemT);
            }
            else if (item is IEnumerable<T> arrayT)
            {
                list.AddRange(arrayT);
            }
            else
            {
                ignoredItems = true;
            }
        }

        return !ignoredItems;
    }
}