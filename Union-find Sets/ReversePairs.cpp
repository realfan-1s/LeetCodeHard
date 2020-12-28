#include <stdc++.h>

using namespace std;

// 归并排序,时间复杂度o(nlogn)， 空间复杂度O(n)
class Solution1 {
public:
    int reversePairs(vector<int>& nums) {
        int length = nums.size();
        if (length < 2)
            return 0;
        vector<int> copy(nums.begin(), nums.end());
        vector<int> tmp(length);
        return MergeSort(copy, 0, length - 1, tmp);
    }
private:
    // nums从[left, right]排序
    int MergeSort(vector<int>& nums, int left, int right, vector<int>& tmp){
        if (left == right)
            return 0;
        int mid = left + (right - left) / 2;
        int leftNums = MergeSort(nums, left, mid, tmp);
        int rightNums = MergeSort(nums, mid + 1, right, tmp);

        // if (nums[mid] <= nums[mid + 1]) 
            // return leftNums + rightNums;

        int crossNums = CountNum(nums, left, mid, right, tmp);
        return leftNums + rightNums + crossNums;
    }

    int CountNum(vector<int>& nums, int left, int mid, int right, vector<int>& tmp){
        for (int i = left; i < right + 1; i++)
            tmp[i] = nums[i];

        int i = left, j = mid + 1, ans = 0;
        for (int k = left; k < right + 1; k++){
            if (i == mid + 1)
                nums[k] = tmp[j++];
            else if (j == right + 1)
                nums[k] = tmp[i++];
            else if (tmp[i] <= tmp[j])
                nums[k] = tmp[i++];
            else{
                nums[k] = tmp[j++];
                ans += (mid - i + 1);
            }
        }

        return ans;
    }
};

// 离散树状数组，时间复杂度o(nlogn)， 空间复杂度O(n)
// 从后往前遍历序列a，前半部分未入桶，后半部分已入桶
// 离散化是在不改变数据相对大小的条件下，对数据进行相应的缩小
class BIT{
private:
    vector<int> tree;
    int n;
public:
    BIT(int n) : tree(n + 1), n(n) {}
    static constexpr int Lowbit(int x){
        return x & (-x);
    }
    void Update(int x, int d){
        while (x <= n){
            tree[x] += d;
            x += Lowbit(x);
        }
    }
    int Query(int x){
        int ans = 0;
        while (x != 0){
            ans += tree[x];
            x -= Lowbit(x);
        }
        return ans;
    }
};

class Solution2 {
public:
    int reversePairs(vector<int>& nums) {
        int len = nums.size();
        vector<int> copy = nums;
        sort(copy.begin(), copy.end());

        // 离散化
        for (int& num : nums)
            num = lower_bound(copy.begin(), copy.end(), num) - copy.begin() + 1;

        // 树状数组统计逆序对
        BIT bit(len);
        int ans = 0;
        for (int i = len -1; i > -1; i--){
            ans += bit.Query(nums[i] - 1);
            bit.Update(nums[i], 1);
        }
        return ans;
    }
};