#include <stdc++.h>

using namespace std;

/*
1,2 -> [1,4)

1,2,4 -> [1,8)

1,2,4,8 -> [1,16)

1,2,4,8,16 -> [1,32)
*/
class Solution {
public:
    int minPatches(vector<int>& nums, int n) {
        int count = 0, index = 0;
        int len = nums.size();
        long curr = 1;

        while (curr <= n){
            // index > len - 1指的是数组内所有数字求和后仍<= n
            // nums[index] > curr 指的是在数组内有缺失的数
            if (index > len - 1 || nums[index] > curr){
                ++count;
                curr *= 2;
            }else{
                curr += nums[index++];
            }
        }

        return count;
    }
};