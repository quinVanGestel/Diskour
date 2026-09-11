using UnityEngine;

public static class QTools
{
    /// <returns>Returns the array index of the item if it can be found in the array. Returns -1 otherwise.</returns>
    public static int FindIndex<T>(T item, T[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (item.Equals(array[i]))
                return i;
        }
        return -1;
    }
}
