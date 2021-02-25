#include <vector>
#include <queue>

using namespace std;

// 滑动窗口,A[i]反转偶数次后还是A[i],翻转奇数次后是A[i]] ^ 1
class Solution
{
public:
    int minKBitFlips(vector<int> &A, int K)
    {
        int len = A.size();
        queue<int, vector<int>> q;
        int ans = 0;

        for (int i = 0; i < len; ++i)
        {
            if (!q.empty() && i >= K + q.front())
            {
                q.pop();
            }
            if (q.size() % 2 == A[i])
            {
                if (len - i < K)
                    return -1;
                q.push(i);
                ++ans;
            }
        }

        return ans;
    }
};

// 差分数组
class Solution
{
public:
    int minKBitFlips(vector<int> &A, int K)
    {
        int len = A.size();
        int ans = 0, cnt = 0;
        vector<int> diff(len + 1, 0);

        for (int i = 0; i < len; ++i){
            cnt += diff[i];
            if ((A[i] + cnt) % 2 == 0){
                if (i + K > len)
                    return -1;
                ++ans;
                ++cnt;
                diff[i + K]--;
            }
        }

        return ans;
    }
};

// 空间复杂度为O(1)的滑动窗口
class Solution
{
public:
    int minKBitFlips(vector<int> &A, int K)
    {
        int len = A.size();
        int ans = 0, cnt = 0;

        for (int i = 0; i < len; ++i){
            if (i >= K && A[i - K] > 1)
            {
                cnt ^= 1;
                A[i - K] -= 2;
            }
            if (A[i] == cnt){
                if (i + K > len)
                    return -1;
                cnt ^= 1;
                A[i] += 2;
                ++ans;
            }
        }

        return ans;
    }
};