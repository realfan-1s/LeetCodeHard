#include <stdc++.h>

using namespace std;

// 中心扩散法
// 以s[i]为中心寻找回文子串，找到从start到end为回文串，list[end+1]=min(list[start]+1, list[end+1])
class Solution {
public:
    int minCut(string s) {
        int len = s.size();
        vector<int> dp(len + 1);
        for (int i = 0; i < len + 1; ++i)
            dp[i] = i - 1;

        for (int i = 0; i < len ; ++i){
            dp[i + 1] = min(dp[i + 1], dp[i] + 1);
            // 最后一个没必要再找
            if (i == len - 1)
                break;

            // 如果i为中心且回文子串是偶数个
            int start = i, end = i + 1;
            while (s[start] == s[end]){
                dp[end + 1] = min(dp[start] + 1, dp[end + 1]);
                if (start == 0 || end == len - 1)
                    break;
                --start;
                ++end;
            }

            // 如果i为中心且回文子串是奇数个
            start = i - 1, end = i + 1;
            if (start < 0)
                continue;
            while (s[start] == s[end]){
                dp[end + 1] = min(dp[start] + 1, dp[end + 1]);
                if (start == 0 || end == len - 1)
                    break;
                --start;
                ++end;

            }

            if (dp[len] == 0)
                return 0;
        }


        return dp[len];
    }
};