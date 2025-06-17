using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Voidless
{
public static class FuzzyMatcher
{
    public static int LevenshteinDistance(string a, string b)
    {
        int[,] matrix = new int[a.Length +1, b.Length + 1];

        for(int i = 0; i <= a.Length; i++) matrix[i, 0] = i;
        for(int j = 0; j <= b.Length; j++) matrix[0, j] = j;
    
        for(int i = 1; i <= a.Length; i++)
        {
            for(int j = 1; j <= b.Length; j++)
            {
                int cost = (a[i - 1] == b[j - 1]) ? 0 : 1;
                matrix[i, j] = Mathf.Min(
                    Mathf.Min(matrix[i - 1, j] + 1, matrix[i, j - 1] + 1),
                    matrix[i - 1, j - 1] + cost
                );
            }
        }

        return matrix[a.Length, b.Length];
    }

    public static string FindClosestMatch(string input, int maxDistance = 2, params string[] validWords)
    {
        string closestMatch = string.Empty;
        int minDistance = int.MaxValue;

        foreach(string word in validWords)
        {
            int distance = LevenshteinDistance(input.ToLower(), word.ToLower());
            if (distance < minDistance && distance <= maxDistance)
            {
                minDistance = distance;
                closestMatch = word;
            }
        }

        return closestMatch; // Returns null if no match is found within maxDistance
    }
}
}