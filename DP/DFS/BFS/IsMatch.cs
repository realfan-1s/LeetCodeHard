public class Solution {
    public bool IsMatch(string s, string p) {
        int s_length = s.Length;
        int p_length = p.Length;
        bool[ , ] dp = new bool[s_length + 1, p_length + 1];
        dp[0, 0] = true;
        
        for (int j = 1; j < p_length ; j++) 
        {
            if (p[j - 1] == '*' || p[j - 1] == '.') dp[0, j] = dp[0, j - 2];
        }


        for (int i = 1; i <= s_length; i++)
        // 匹配规律中第一个字符可以出现零次，出现一次的情况业已单独赋值，因此不必看
            for (int j = 1; j <= p_length + 1; j++)
            {
                if (s[i - 1] == p[j - 1] || p[j - 1] == '.')
                    dp[i, j] = dp[i - 1, j - 1];
                else if (p[j - 1] == '*')
                {
                    if (s[i - 1] == p[j - 2] || p[j - 2] == '.')
                        dp[i, j] = dp[i - 1, j - 2] | dp[i, j - 2] | dp[i - 1, j];
                    else
                        dp[i, j] = dp[i, j - 2];
                }

            }
        return dp[s_length, p_length];
    }
}