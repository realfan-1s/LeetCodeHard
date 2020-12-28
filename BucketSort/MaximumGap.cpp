#include <stdc++.h>

using namespace std;

class Solution {
public:
    int maximumGap(vector<int>& nums) {
        int length = nums.size();
        if (length < 2)
            return 0;
        int exp = 1;
        vector<int> buf(length);
        int maxVal = *max_element(nums.begin(), nums.end());

        while (maxVal >= exp)
        {
            vector<int> cnt(10);
            for (int i = 0; i < length; i++)
            {
                // 获得对应位数的值（自最高位开始）
                int digit = (nums[i] / exp) % 10;
                cnt[digit]++;
            }
            for (int i = 1; i < 10; i++)
                // 确定每个数值在buf中对应的位置
                cnt[i] += cnt[i - 1];
            for (int i = length - 1; i >= 0; i--)
            {
                int digit = (nums[i] / exp) % 10;
                buf[cnt[digit] - 1] = nums[i];
                cnt[digit]--;
            }
            copy(buf.begin(), buf.end(), nums.begin());
            exp *= 10;
        }
        int res = 0;
        for (auto i = 0; i < length - 1; i++)
            res = max(nums[i + 1] - nums[i], res);
        return res;
    }
};