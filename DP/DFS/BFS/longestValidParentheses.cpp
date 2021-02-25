#include <vector>
#include <string>
#include <math.h>
#include <stack>

using namespace std;

// 动态规划
class Solution
{
public:
    int longestValidParentheses(string s)
    {
        int ans = 0;
        int len = s.size();
        vector<int> dp(len, 0);
        for (int i = 1; i < len; ++i)
        {
            if (s[i] == ')')
            {
                if (s[i - 1] == '(')
                {
                    dp[i] = (i >= 2 ? dp[i - 2] : 0) + 2;
                }
                else if (i - dp[i - 1] - 1 >= 0 && s[i - dp[i - 1] - 1] == '(')
                    dp[i] = dp[i - 1] + 2 + ((i - dp[i - 1] - 2) >= 0 ? dp[i - dp[i - 1] - 2] : 0);
            }
            ans = max(ans, dp[i]);
        }
        return ans;
    }
};

// 栈
class Solution
{
public:
    int longestValidParentheses(string s)
    {
        int ans = 0;
        int len = s.size();
        stack<int> stk;
        stk.push(-1);
        for (int i = 0; i < len; ++i)
        {
            if (s[i] == '(')
            {
                stk.push(i);
            }
            else
            {
                stk.pop();
                if (stk.empty())
                {
                    // 最后一个没有被匹配的右括号的下标
                    stk.push(i);
                }
                else
                {
                    ans = max(ans, i - stk.top());
                }
            }
        }
        return ans;
    }
};

// left right指针
class Solution
{
public:
    int longestValidParentheses(string s)
    {
        int left = 0, right = 0, ans = 0;
        int len = s.size();
        for (int i = 0; i < len; ++i)
        {
            if (s[i] == '(')
                left++;
            else
                right++;
            if (left == right)
                ans = max(ans, 2 * right);
            else if (right > left)
                left = right = 0;
        }
        left = right = 0;
        for (int i = len - 1; i > -1; --i)
        {
            if (s[i] == '(')
                left++;
            else
                right++;
            if (left == right)
                ans = max(ans, 2 * left);
            else if (left > right)
                left = right = 0;
        }
        return ans;
    }
};