using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListEqualier : MonoBehaviour
{
    public static bool UnorderedEqual<T>(ICollection<T> a, ICollection<T> b)
    {
        if (a.Count != b.Count)
        {
            return false;
        }

        Dictionary<T, int> dict = new Dictionary<T, int>();

        foreach (T element in a)
        {
            int c;
            if (dict.TryGetValue(element, out c))
            {
                dict[element] = c + 1;
            }
            else
            {
                dict.Add(element, 1);
            }
        }

        foreach (T element in b)
        {
            int c;
            if (dict.TryGetValue(element, out c))
            {
                if (c == 0)
                {
                    return false;
                }
                else
                {
                    dict[element] = c - 1;
                }
            }
            else
            {
                return false;
            }
        }

        foreach (int value in dict.Values)
        {
            if (value != 0)
            {
                return false;
            }
        }
        return true;
    }
}
