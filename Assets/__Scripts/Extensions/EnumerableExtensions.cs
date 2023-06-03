using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

public static class IEnumerableExtensions
{
    public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source,
        Func<TSource, TKey> keySelector)
    {
        var seenKeys = new HashSet<TKey>();
        foreach (var element in source)
        {
            if (seenKeys.Add(keySelector(element)))
                yield return element;
        }
    }

    public static IList<int> AllIndexOf(this string text, string str, bool standardizeUpperCase = true,
        StringComparison comparisonType = StringComparison.InvariantCultureIgnoreCase)
    {
        IList<int> allIndexOf = new List<int>();
        var newSource = standardizeUpperCase ? text.ToUpper() : text;
        var newStr = standardizeUpperCase ? str.ToUpper() : str;
        var index = newSource.IndexOf(newStr, comparisonType);
        while (index != -1)
        {
            allIndexOf.Add(index);
            index = newSource.IndexOf(newStr, index + newStr.Length, comparisonType);
        }

        return allIndexOf;
    }

    /// <summary>
    /// Finds an item with the closest index, or (null, -1) if no items
    /// </summary>
    /// <param name="list"></param>
    /// <param name="item"></param>
    /// <param name="comparer"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static (T item, int index) BinarySearch<T>(this IList<T> list, T item, [CanBeNull] IComparer<T> comparer = null)
    {
        T selectedItem = default;
        var index = -1;
        
        switch (list.Count)
        {
            case 0:
                return (selectedItem, index);
            case 1:
                return (list.First(), 0);
        }

        var left = 0;
        var right = list.Count - 1;
        comparer ??= Comparer<T>.Default;

        

        while (left < right - 1)
        {
            // // Check if x is present at mid
            // if (arr[m] == x)
            //     return m;
            //
            // // If x greater, ignore left half
            // if (x > arr[m])
            //     l = m + 1;
            //
            // // If x is smaller, ignore right half
            // else
            //     r = m - 1;
            var m = (left + right) / 2;
            index = m;
            
            selectedItem = list[m];

            var direction = comparer.Compare(item, selectedItem);

            if (direction == 0)
            {
                return (selectedItem, m);
            }

            // our item is greater than the middle
            // so go right
            if (direction > 0)
            {
                // making left every element to the right of middle
                left = m + 1;
            }
            
            // our item is lesser than the middle
            // so go left
            if (direction < 0)
            {
                // making right every element to the left of middle
                right = m - 1;
            }
        }

        return (selectedItem, index);
    }

    // TODO: Replace with Span in Unity 2021.2 
    // https://forum.unity.com/threads/does-unity-have-support-span-t.1167770/
    public static IEnumerable<T> GetViewBetween<T>(this IList<T> list, T begin, T end,
        [CanBeNull] IComparer<T> comparer = null)
    {
        var (_, beginIndex) = list.BinarySearch(begin, comparer);
        var (_, endIndex) = list.BinarySearch(end, comparer);

        if (beginIndex == -1) beginIndex = 0;
        if (endIndex == -1) endIndex = list.Count - 1;

        var newList = new List<T>(endIndex - beginIndex + 2);
        for (var i = beginIndex; i < endIndex + 1; i++)
        {
            newList.Add(list[i]);
        }

        return newList;
    }
}
