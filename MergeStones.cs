using System;
// 朴素DP
public class Solution {
    public int MergeStones(int[] stones, int K) {
        int length = stones.Length;
        if ((length - 1) % (K - 1) != 0)
            return -1;
        int[ , , ] dp = new int[length + 1, length + 1, K + 1];
        for (int i = 1; i <= length; i++)
            for (int j = i; j <= length; j++)
                for (int m = 2; m <= K; m++)
                {
                    dp[i, j, m] = int.MaxValue;
                }
        dp[1, length, 1] = 0;


        int[] stoneSum = new int[length + 1];
        for (int i = 0; i < length; i++)
            stoneSum[i + 1] = stoneSum[i] + stones[i];

        for (int len = 2; len <= length; len++)
            for (int i = 1; i + len - 1 <= length; i++)
            {
                int j = i + len - 1;
                for (int m = 2; m <= K; m++){
                    for (int n = i; n < j; n+=K-1)
                        dp[i, j, m] = Math.Min(dp[i, j, m], dp[i, n, 1] + dp[n + 1, j, m - 1]);
                    dp[i, j, 1] = dp[i, j, K] + stoneSum[j] - stoneSum[i - 1];
                }    
            }
        
        return dp[1, length, 1];
    }
}

// 改进DP
public class Solution2 {
    public int MergeStones(int[] stones, int K) {
        int length = stones.Length;
        if ((length - 1) % (K - 1) != 0)
            return -1;

        int[] sumStone = new int[length + 1];
        for (int i = 0; i < length; i++)
            sumStone[i + 1] = sumStone[i] + stones[i];

        int[ , ] dp = new int[length + 1, length + 1];
        // 枚举区间长度
        for (int len = K; len <= length; len++)
            for (int i = 1; i + len - 1 <= length; i++)
            {
                int j = i + len - 1;
                dp[i, j] = int.MaxValue;
                for (int p = i; p < j; p += K-1)
                    dp[i, j] = Math.Min(dp[i, j], dp[i, p] + dp[p + 1, j]);

                if ((j - i) % (K -1) == 0)
                    dp[i, j] += sumStone[j] - sumStone[i - 1];
            }
        
        return dp[1, length];
    }
}