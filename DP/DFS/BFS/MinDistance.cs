using System;
public class Solution {
    public int MinDistance (string word1, string word2) {
        int length1 = word1.Length;
        int length2 = word2.Length;

        if (length1 * length2 == 0)
            return length1 + length2;

        int[, ] dp = new int[length1 + 1, length2 + 1];
        for (int i = 0; i < length1 + 1; i++)
            dp[i, 0] = i;
        for (int j = 0; j < length2 + 1; j++)
            dp[0, j] = j;

        for (int m = 1; m < length1 + 1; m++)
            for (int n = 1; n < length2 + 1; n++) {
                if (word1[m - 1] == word2[n - 1])
                    dp[m, n] = dp[m - 1, n - 1];
                else
                    dp[m, n] = Math.Min (Math.Min (dp[m, n - 1], dp[m - 1, n]), dp[m - 1, n - 1]) + 1;
            }

        return dp[length1, length2];

    }
}