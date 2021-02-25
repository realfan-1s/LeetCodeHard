#include <stdc++.h>
<<<<<<< HEAD
=======
#include <vector>
>>>>>>> 0ea2c7e... 寒假

using namespace std;

class Solution {
public:
    bool isMatch(string s, string p) {
        int len1 = s.size();
        int len2 = p.size();
        vector<vector<bool>> dp(len1 + 1, vector<bool>(len2 + 1, false));
        dp[0][0] = true;
        for (int i = 2; i < len2 + 1; i++){
            if (p[i - 1] == '*')
                dp[0][i] = dp[0][i - 2];
        }
        // 从左往右遍历，从右向左匹配
        for (int i = 1; i < len1 + 1; i++){
            for (int j = 1; j < len2 + 1; j++){
                if (s[i - 1] == p[j - 1] || p[j - 1] == '.')
                    dp[i][j] = dp[i - 1][j - 1];
                else if (p[j - 1] == '*')
                {
                    if (s[i - 1] == p[j - 2] || p[j - 2] == '.')
                    // 让p[j - 2]重复0次， 让p[j - 2]重复>=1次，让p[j- 2]重复>=2次
                        dp[i][j] = dp[i][j - 2] | dp[i - 1][j - 2] | dp[i - 1][j];
                    else
                        dp[i][j] = dp[i][j - 2];
                }
            }
        }

        return dp[len1][len2];
    }
};