using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;

public class FastSortedList<T> : IList<T>
{
    private readonly IList<T> inner;
    private readonly IComparer<T> comparer;

    // TODO: Sort?
    public FastSortedList(IEnumerable<T> list, [CanBeNull] IComparer<T> comparer = null, bool sort = true)
    {
        var newList = new List<T>(list);
        inner = newList;
        this.comparer = comparer ?? Comparer<T>.Default;
        if (sort)
        {
            newList.Sort(comparer);
        }
    }

    public FastSortedList([CanBeNull] IComparer<T> comparer = null)
    {
        inner = new List<T>();
        this.comparer = comparer ?? Comparer<T>.Default;
    }

    public FastSortedList(int size, [CanBeNull] IComparer<T> comparer = null)
    {
        inner = new List<T>(size);
        this.comparer = comparer ?? Comparer<T>.Default;
    }

    private void InsertSorted(T item)
    {
        var (found, index) = inner.BinarySearch(item, comparer);

        if (index == -1 || found == null)
        {
            inner.Add(item);
            return;
        }

        var direction = comparer.Compare(item, found);

        // if item is in the same place, put in same place and move it up
        if (direction <= 0)
        {
            inner.Insert(index, item);
            return;
        }

        // if (direction < 0)
        // {
        //     // clamp to 0..
        //     inner.Insert(Math.Max(index - 1, 0), item);
        //     return;
        // }

        
        // place after found because it is greater
        inner.Insert(index + 1, item);
    }

    private bool RemoveSorted(T item)
    {
        var (found, index) = inner.BinarySearch(item, comparer);

        if (!Equals(found, item)) return false;

        inner.RemoveAt(index);
        return true;
    }

    private int? GetIndexSorted(T item)
    {
        var (found, index) = inner.BinarySearch(item, comparer);

        if (!Equals(found, item) || index == -1) return null;

        return index;
    }

    public IEnumerator<T> GetEnumerator() => inner.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public void Add(T item) => InsertSorted(item);

    public void Clear() => inner.Clear();

    public bool Contains(T item) => GetIndexSorted(item) != null;

    public void CopyTo(T[] array, int arrayIndex) => inner.CopyTo(array, arrayIndex);

    public bool Remove(T item) => RemoveSorted(item);


    public int Count => inner.Count;
    public bool IsReadOnly => inner.IsReadOnly;
    public int IndexOf(T item) => GetIndexSorted(item) ?? -1;

    // TODO: Redirect to sorted?
    public void Insert(int index, T item) => inner.Insert(index, item);

    public void RemoveAt(int index) => inner.RemoveAt(index);

    public T this[int index]
    {
        get => inner[index];
        set => inner[index] = value;
    }
}
