using System;
using System.Collections.Generic;
public class Solution {
    Stack<string> stk = new Stack<string>();
    List<string> res = new List<string>();
    public IList<string> WordBreak(string s, IList<string> wordDict) {
        int length = s.Length;
        bool[] dp = new bool[length + 1];
        HashSet<string> wordSet = new HashSet<string>(wordDict);
        dp[0] = true;

        // 通过动态规划判断是否能否划分单词
        for (int i = 1; i <= length; i++)
            for (int j = 0; j < i; j++)
                if (dp[j] && wordSet.Contains(s.Substring(j, i - j)))
                {
                    dp[i] = true;
                    break;
                }

        // 通过回溯解法获得每个单词的排列
        if (dp[length])
            dfs(s, wordSet, dp, length);

        return res;
    }

    private void dfs(string s, HashSet<string> wordDict, bool[] dp, int len)
    {
        if (len == 0)
        {
            res.Add(String.Join(" ", stk));
            return;
        }

        // 自后向前枚举,i是左边界
        for (int i = len - 1; i >= 0; i--)
        {
            string suffix = s.Substring(i, len - i);
            if (wordDict.Contains(suffix) && dp[i])
            {
                stk.Push(suffix);
                dfs(s, wordDict, dp, i);
                stk.Pop();
            }
        }
    }
}