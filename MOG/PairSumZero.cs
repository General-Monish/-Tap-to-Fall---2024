using System.Collections.Generic;
using UnityEngine;

public class PairSumZero : MonoBehaviour
{
    public bool IsSumTwoZero(int[] a)
    {
        List<int> seenNumbers = new List<int>();
        foreach (int number in a)
        {
            if (seenNumbers.Contains(-number))
                return true;
            seenNumbers.Add(number);
        }
        return false;
    }

    void Start()
    {
        int[] testArray = { -7, -5, 4, 5, 6 };
        Debug.Log("Pair Sum Zero: " + IsSumTwoZero(testArray)); // Output: true
    }
}
