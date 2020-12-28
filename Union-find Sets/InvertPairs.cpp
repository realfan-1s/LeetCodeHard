#include <iostream>
#include <algorithm>
#include <vector>
#include <string>
#include <set>
#include <unordered_map>

using namespace std;
// 归并排序
class Solution1 {
public:
    int reversePairs(vector<int>& nums) {
        if (nums.size() < 2)
            return 0;
        return RescursiveSort(nums, 0, nums.size() - 1);
    }
private:
    int RescursiveSort(vector<int>& nums, int left, int right){
        if (left == right)
            return 0;
        else
        {
            int mid = left + (right - left) / 2;
            int n1 = RescursiveSort(nums, left, mid);
            int n2 = RescursiveSort(nums, mid + 1, right);
            int res = n1 + n2;

            int i = left, j = mid + 1;
            while (i <= mid)
            {
                while (j <= right && (long)nums[i] > (long)nums[j] * 2)
                    j++;
                res += (j - mid - 1);
                i++;
            }

            // 归并排序
            int p1 = left, p2 = mid + 1;
            int p = 0;
            vector<int> sorted(right - left + 1);
            while (p1 <= mid || p2 <= right){
                if (p1 > mid)
                    sorted[p++] = nums[p2++];
                else if(p2 > right)
                    sorted[p++] = nums[p1++];
                else
                {
                    if (nums[p1] < nums[p2])
                        sorted[p++] = nums[p1++];
                    else
                        sorted[p++] = nums[p2++];
                }
            }
            for (auto k = 0; k < sorted.size(); k++)
                nums[left + k] = sorted[k];
            return res;
        }
    }
};

// 树状数组
class BIT{
    private:
        vector<int> tree;
        int n;
    public:
        BIT(int n) : n(n), tree(n + 1) {}
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
        set<long> allNumbers;
        for (int i = 0; i < nums.size(); i++)
        {
            allNumbers.insert(nums[i]);
            allNumbers.insert((long)nums[i] * 2);
        }

        unordered_map<long, int> valuesDict;
        int index = 0;
        for(long item : allNumbers)
            valuesDict[item] = ++index;

        int res = 0;
        BIT bit(valuesDict.size());
        for (int k = 0; k < nums.size(); k++){
            int left = valuesDict[(long) nums[k] * 2];
            int right = valuesDict.size();
            res += bit.Query(right) - bit.Query(left);
            bit.Update(valuesDict[nums[k]], 1);
        }
        return res;
    }
};