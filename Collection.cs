using System.Collections.Generic;


class Collection<T> : List<T>
{
    // Count property
    public new int Count { get { return base.Count; } }

    // Item property
    public new T this[int index] { get { return base[index]; } }

    // Add method
    public void Add(T item)
    {
        base.Add(item);
    }

    // Remove method
    public void Remove(T item)
    {
        base.Remove(item);
    }
}

