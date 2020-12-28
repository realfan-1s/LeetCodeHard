using System;
using System.ComponentModel;
using System.IO;
using System.Text.RegularExpressions;
// 二维数组,0 - 1背包dp
public class Solution1 {
    public int MaxProfitIII (int[] prices) {
        int length = prices.Length;
        int[, ] dp = new int[length, 5];
        for (int i = 2; i < length + 1; i++)
            for (int j = 1; j <= Math.Min (i + 1, 4); j++) {
                // j为奇数买入，j为偶数卖出
                if (j % 2 == 1)
                    dp[i, j] = Math.Max (dp[i - 1, j] + prices[i] - price[i - 1], dp[i - 1, j - 1]);
                else
                    dp[i, j] = Math.Max (dp[i - 1, j], dp[i - 1, j - 1] + prices[i] - prices[i - 1]);
            }

        return Math.Max (dp[length, 0], Math.Max (dp[length, 2], dp[length, 4]));
    }
}

// 一维滚动数组
public class Solution2 {
    public int MaxProfitIII (int[] prices) {
        int length = prices.Length;
        int[] dp = new int[5];
        for (int i = 1; i < length; i++)
            for (int j = Math.Min (i + 1, 4); j > 0; j--) {
                if (j % 2 == 1)
                    dp[j] = Math.Max (dp[j - 1], dp[j] + prices[i] - prices[i - 1]);
                else
                    dp[j] = Math.Max (dp[j], dp[j - 1] + prices[i] - prices[i - 1]);
            }

        return Math.Max (dp[0], Math.Max (dp[2], dp[4]));
    }
}

public class Solution4 {
    public int MaxProfitIV (int k, int[] prices) {
        int profit = 0;
        int length = prices.Length;
        int[] dp = new int[2 * k + 1];
        for (int i = 1; i < length; i++)
            for (int j = Math.Min (i + 1, 2 * k); j > 0; j++) {
                if (j % 2 == 1)
                    dp[j] = Math.Max (dp[j - 1], dp[j] + prices[i] - prices[i - 1]);
                else
                    dp[j] = Math.Max (dp[j] + prices[i] - prices[i - 1], dp[j - 1]);
            }

        for (int m = 0; m <= 2 * k; m += 2)
            profit = Math.Max (profit, dp[m]);

        return res;
    }
}