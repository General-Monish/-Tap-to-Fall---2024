using System.Collections.Generic;
using UnityEngine;

public class TripletSumZero : MonoBehaviour
{
    public bool IsSumThreeZero(int[] a)
    {
        for (int i = 0; i < a.Length - 2; i++)
        {
            List<int> seenNumbers = new List<int>();
            int currentSum = -a[i];
            for (int j = i + 1; j < a.Length; j++)
            {
                if (seenNumbers.Contains(currentSum - a[j]))
                    return true;
                seenNumbers.Add(a[j]);
            }
        }
        return false;
    }

    void Start()
    {
        int[] testArray = { -7, -3, 4, 6, 10, 15 };
        Debug.Log("Triplet Sum Zero: " + IsSumThreeZero(testArray)); // Output: true
    }
}
